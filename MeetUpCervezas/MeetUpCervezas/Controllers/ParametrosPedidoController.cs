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
    public class ParametrosPedidoController : ApiController
    {

        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        ParametrosPedidosService parametrosPedidoService = new ParametrosPedidosService();

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        /// <returns>Arreglo de Roles</returns>
        [HttpGet]
        public IEnumerable<ParametrosPedido> Get()
        {
            return parametrosPedidoService.Get();
        }

        /// <summary>
        /// Obtener un rol filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Rol</returns>
        [HttpGet]
        public ParametrosPedido Get(int id)
        {
            return parametrosPedidoService.Get(id);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
        [HttpPost]
        public IHttpActionResult InsertParametrosPedido([FromBody] ParametrosPedido parametrosPedido)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (parametrosPedidoService.InsertParametrosPedido(parametrosPedido))
                    {
                        ModelState.AddModelError("", "Ya existe un parametrosPedido con este nombre");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(parametrosPedido);
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
        public IHttpActionResult UpdateParametrosPedido([FromBody] ParametrosPedido parametrosPedido)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!parametrosPedidoService.UpdateParametrosPedido(parametrosPedido))
                    {
                        ModelState.AddModelError("", "No se encontró ParametrosPedido para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(parametrosPedido);
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
        public IHttpActionResult DeleteParametrosPedido(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!parametrosPedidoService.DeleteParametrosPedido(id))
                    {
                        ModelState.AddModelError("", "No se encontró ParametrosPedido para eliminar");

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
