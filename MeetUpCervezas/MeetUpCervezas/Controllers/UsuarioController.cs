using DB;
using MeetUpCervezas.Business.Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MeetUpCervezas.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        UsuarioService usuarioService = new UsuarioService();

        /// <summary>
        /// Obtener todos los Usuario
        /// </summary>
        /// <returns>Arreglo de Usuario</returns>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return usuarioService.Get();
        }

        /// <summary>
        /// Obtener un Usuario filtrado por Id
        /// </summary>
        /// <param name="id"> id Usuario a obtener</param>
        /// <returns>Usuario</returns>
        [HttpGet]
        public Usuario Get(int id)
        {
            return usuarioService.Get(id);
        }

        /// <summary>
        /// Inserta un Usuario
        /// </summary>
        /// <param name="rol">usuario a insertar</param>
        /// <returns>Usuario insertado</returns>
        [HttpPost]
        public IHttpActionResult InsertUsuario([FromBody] Usuario usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (usuarioService.InsertUsuario(usuario))
                    {
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(usuario);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Modificar Usuario
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> usuario a modificar</param>
        /// <returns>Usuario Modificado</returns>
        [HttpPut]
        public IHttpActionResult UpdateUsuario([FromBody] Usuario usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!usuarioService.UpdateUsuario(usuario))
                    {
                        ModelState.AddModelError("", "No se encontró Usuario para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(usuario);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar Usuario por id
        /// </summary>
        /// <param name="id"> id Usuario a eliminar</param>
        /// <returns>Usuario Eliminado</returns>

        [HttpDelete]
        public IHttpActionResult DeleteUsuario(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!usuarioService.DeleteUsuario(id))
                    {
                        ModelState.AddModelError("", "No se encontró Usuario para eliminar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(id);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// valida token de login
        /// </summary>
        internal class ValidarTokenHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                   CancellationToken cancellationToken)
            {
                HttpStatusCode statusCode;
                string token;

                if (!TryRetrieveToken(request, out token))
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    return base.SendAsync(request, cancellationToken);
                }

                try
                {
                    var claveSecreta = ConfigurationManager.AppSettings["ClaveSecreta"];
                    var issuerToken = ConfigurationManager.AppSettings["Issuer"];
                    var audienceToken = ConfigurationManager.AppSettings["Audience"];

                    var securityKey = new SymmetricSecurityKey(
                        System.Text.Encoding.Default.GetBytes(claveSecreta));

                    SecurityToken securityToken;
                    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = audienceToken,
                        ValidIssuer = issuerToken,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        // DELEGADO PERSONALIZADO PERA COMPROBAR
                        // LA CADUCIDAD EL TOKEN.
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey
                    };

                    // COMPRUEBA LA VALIDEZ DEL TOKEN
                    Thread.CurrentPrincipal = tokenHandler.ValidateToken(token,
                                                                         validationParameters,
                                                                         out securityToken);
                    HttpContext.Current.User = tokenHandler.ValidateToken(token,
                                                                          validationParameters,
                                                                          out securityToken);

                    return base.SendAsync(request, cancellationToken);
                }
                catch (SecurityTokenValidationException ex)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                catch (Exception ex)
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }

                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                            new HttpResponseMessage(statusCode) { });
            }

            // RECUPERA EL TOKEN DE LA PETICIÓN
            private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
            {
                token = null;
                IEnumerable<string> authzHeaders;
                if (request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                                                  authzHeaders.Count() > 1)
                {
                    return false;
                }
                var bearerToken = authzHeaders.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ?
                        bearerToken.Substring(7) : bearerToken;
                return true;
            }

            // COMPRUEBA LA CADUCIDAD DEL TOKEN
            public bool LifetimeValidator(DateTime? notBefore,
                                          DateTime? expires,
                                          SecurityToken securityToken,
                                          TokenValidationParameters validationParameters)
            {
                var valid = false;

                if ((expires.HasValue && DateTime.UtcNow < expires)
                    && (notBefore.HasValue && DateTime.UtcNow > notBefore))
                { valid = true; }

                return valid;
            }
        }
    }
}
