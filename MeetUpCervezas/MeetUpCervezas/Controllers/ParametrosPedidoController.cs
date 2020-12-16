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
    public class ParametrosPedidoController : ApiController
    {

        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        ParametrosPedidosService parametrosPedidoService = new ParametrosPedidosService();

        /// <summary>
        /// Obtener todos los Parametros
        /// </summary>
        /// <returns>Arreglo de parametros</returns>
        [HttpGet]
        public IEnumerable<ParametrosPedido> Get()
        {
            return parametrosPedidoService.Get();
        }

        /// <summary>
        /// Obtener un parametro filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>parametro</returns>
        [HttpGet]
        public ParametrosPedido Get(int id)
        {
            return parametrosPedidoService.Get(id);
        }

        /// <summary>
        /// Inserta un parametro
        /// </summary>
        /// <param name="parametro">parametro a insertar</param>
        /// <returns>Parametro insertado</returns>
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
        /// Modificar Parametro
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> parametro a modificar</param>
        /// <returns>Parametro Modificado</returns>
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
        /// Eliminar Parametro por id
        /// </summary>
        /// <param name="id"> id parametro a eliminar</param>
        /// <returns>Parametro Eliminado</returns>

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
