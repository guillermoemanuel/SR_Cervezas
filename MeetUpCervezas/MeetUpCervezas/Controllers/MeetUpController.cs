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
    public class MeetUpController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        MeetUpService meetUpService = new MeetUpService();

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        /// <returns>Arreglo de Roles</returns>
        [HttpGet]
        public IEnumerable<Meetup> Get()
        {
            return meetUpService.Get();
        }

        /// <summary>
        /// Obtener un rol filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Rol</returns>
        [HttpGet]
        public Meetup Get(int id)
        {
            return meetUpService.Get(id);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
        [HttpPost]
        public IHttpActionResult InsertMeetup([FromBody] Meetup meetUp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (meetUpService.InsertMeetup(meetUp))
                    {
                        ModelState.AddModelError("", "Ya existe un meetUp con este nombre");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(meetUp);
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
        public IHttpActionResult UpdateMeetup([FromBody] Meetup meetUp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!meetUpService.UpdateMeetup(meetUp))
                    {
                        ModelState.AddModelError("", "No se encontró Meetup para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(meetUp);
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
        public IHttpActionResult DeleteMeetup(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!meetUpService.DeleteMeetup(id))
                    {
                        ModelState.AddModelError("", "No se encontró Meetup para eliminar");

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
