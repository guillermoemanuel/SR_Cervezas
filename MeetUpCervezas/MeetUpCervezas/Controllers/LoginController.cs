
using DB;
using MeetUpCervezas.Models;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace MeetUpCervezas.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();

        /// <summary>
        /// Verifica conexion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        /// <summary>
        /// muestra el usuario logueado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        /// <summary>
        /// permite autenticacion del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(string usuario, string password)
        {
            bool isCredentialValid = false;

            if (usuario == null || password == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var usuarioEncontrado = dbContext.Usuario.FirstOrDefault(u => u.Usuario1 == usuario && u.Password == password && u.Estado == 1);

            if (usuarioEncontrado != null)
                isCredentialValid = true;
            else
                isCredentialValid = false;

            if (isCredentialValid)
            {
                
                var token = TokenGenerator.GenerateTokenJwt(usuario);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }



}
