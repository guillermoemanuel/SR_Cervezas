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
    public class PedidoController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        PedidoService pedidoService = new PedidoService();

        /// <summary>
        /// Obtener todos los Pedidos
        /// </summary>
        /// <returns>Arreglo de Pedidos</returns>
        [HttpGet]
        public IEnumerable<Pedido> Get()
        {
            return pedidoService.Get();
        }

        /// <summary>
        /// Obtener un Pedido filtrado por Id
        /// </summary>
        /// <param name="id"> id pedido a obtener</param>
        /// <returns>Pedido</returns>
        [HttpGet]
        public Pedido Get(int id)
        {
            return pedidoService.Get(id);
        }

        /// <summary>
        /// Inserta un Pedido
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Pedido insertado</returns>
        [HttpPost]
        [Route("api/Pedido")]
        public IHttpActionResult RealizarPedido(int IdProveedor, int idMeetUp)
        {
            TemperaturaController temperaturaController = new TemperaturaController();
            Meetup meetup = new Meetup();

            meetup = dbContext.Meetup.FirstOrDefault(m => m.Id == idMeetUp);

            var temp = temperaturaController.Get(meetup.Lugar, "AR").app_temp;
           
                try
            {

                if (ModelState.IsValid)
                {
                    if (pedidoService.InsertPedido(IdProveedor, idMeetUp, temp))
                    {
                        ModelState.AddModelError("", "Ya existe un pedido para la meetUp seleccionada");

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
        /// Modificar Pedido
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> Pedido a modificar</param>
        /// <returns>Pedido Modificado</returns>
        [HttpPut]
        public IHttpActionResult UpdatePedido([FromBody] Pedido pedido)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!pedidoService.UpdatePedido(pedido))
                    {
                        ModelState.AddModelError("", "No se encontró Pedido para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(pedido);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar Pedido por id
        /// </summary>
        /// <param name="id"> id rol a eliminar</param>
        /// <returns>Pedido Eliminado</returns>

        [HttpDelete]
        public IHttpActionResult DeletePedido(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!pedidoService.DeletePedido(id))
                    {
                        ModelState.AddModelError("", "No se encontró Pedido para eliminar");

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
