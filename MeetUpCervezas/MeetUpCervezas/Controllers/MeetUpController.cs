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
    [Authorize]
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
        public Meetup Get(int? id, DateTime? fecha, DateTime? hora, string titulo, int? estado)
        {
            return meetUpService.Get(id,fecha,hora,titulo,estado);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
        [HttpPost]
        [Route("CrearMeetUp")]
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

        /// <summary>
        /// Devuelve el listado de Usuarios asistentes a una MeetUp
        /// </summary>
        /// <param name="idMeetUp"> id de la meetUp</param>
        /// <returns>List de Usuarios</returns>
        [HttpGet]
        [Route("Asistente")]
        public List<Usuario> Get(int idMeetUp)
        {
            return meetUpService.GetAsistentes(idMeetUp);
        }

        /// <summary>
        /// Devuelve un listado de MeetUp relacionadas a un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>List MeetUp</returns>
        [HttpGet]
        [Route("MeetUpAsistente")]
        public List<Meetup> GetMeetUp(int idUsuario)
        {
            return meetUpService.GetMeetUpsAsistentes(idUsuario);
        }


        /// <summary>
        /// Asocia un usuario a una MeetUp permitiendo realizar invitaciones e inscripciones 
        /// </summary>
        /// <param name="idMeetUp"> id de la MeetUp</param>
        /// <param name="idUsuario"> id del usuario></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Asistente")]
        public IHttpActionResult InvitedAsistente(int idMeetUp, int idUsuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (meetUpService.InvitedAsistente(idMeetUp, idUsuario))
                    {
                        ModelState.AddModelError("", "EL usuario ya fue invitado");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Permite modificar el estado de un usuario con respecto a una MeetUp permitiendo de esta forma poder realizar check-in
        /// </summary>
        /// <param name="idMeetUp"></param>
        /// <param name="idUsuario"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Asistente")]
        public IHttpActionResult UpdateAsistenteEstado(int idMeetUp, int idUsuario, int estado)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!meetUpService.UpdateAsistenteEstado(idMeetUp,idUsuario,estado))
                    {
                        ModelState.AddModelError("", "No se encontró Asistente para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
