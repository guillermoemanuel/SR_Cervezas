using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB;
using MeetUpCervezas.Service;

namespace MeetUpCervezas.Controllers
{
    public class RolController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        RolService rolService = new RolService();

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        /// <returns>Arreglo de Roles</returns>
        [HttpGet]
        public IEnumerable<Rol> Get()
        {
            return rolService.Get();
        }

        /// <summary>
        /// Obtener un rol filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Rol</returns>
        [HttpGet]
        public Rol Get(int id)
        {
            return rolService.Get(id);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
        [HttpPost]
        public IHttpActionResult InsertRol([FromBody] Rol rol)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (rolService.InsertRol(rol))
                    {
                        ModelState.AddModelError("", "Ya existe un rol con este nombre");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(rol);
                }

            }
            catch(Exception ex)
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
        public IHttpActionResult UpdateRol([FromBody] Rol rol)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!rolService.UpdateRol(rol))
                    {
                        ModelState.AddModelError("", "No se encontró rol para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(rol);
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
        public IHttpActionResult DeleteRol(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!rolService.DeleteRol(id))
                    {
                        ModelState.AddModelError("", "No se encontró rol para eliminar");

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