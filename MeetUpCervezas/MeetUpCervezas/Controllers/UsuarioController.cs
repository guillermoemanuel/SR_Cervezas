using DB;
using MeetUpCervezas.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MeetUpCervezas.Controllers
{
    public class UsuarioController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        UsuarioService usuarioService = new UsuarioService();

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        /// <returns>Arreglo de Roles</returns>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return usuarioService.Get();
        }

        /// <summary>
        /// Obtener un rol filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Rol</returns>
        [HttpGet]
        public Usuario Get(int id)
        {
            return usuarioService.Get(id);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
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
        /// Modificar Rol
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> rol a modificar</param>
        /// <returns>Rol Modificado</returns>
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
        /// Eliminar Rol por id
        /// </summary>
        /// <param name="id"> id rol a eliminar</param>
        /// <returns>Rol Eliminado</returns>

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
    }
}
