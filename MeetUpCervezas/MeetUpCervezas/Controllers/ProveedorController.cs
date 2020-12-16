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
    public class ProveedorController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        ProveedorService proveedorService = new ProveedorService();

        /// <summary>
        /// Obtener todos los Proveedor
        /// </summary>
        /// <returns>Arreglo de Proveedor</returns>
        [HttpGet]
        public IEnumerable<Proveedor> Get()
        {
            return proveedorService.Get();
        }

        /// <summary>
        /// Obtener un Proveedor filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Proveedor</returns>
        [HttpGet]
        public Proveedor Get(int id)
        {
            return proveedorService.Get(id);
        }

        /// <summary>
        /// Inserta un Porveedor
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Proveedor insertado</returns>
        [HttpPost]
        public IHttpActionResult InsertProveedor([FromBody] Proveedor proveedor)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (proveedorService.InsertProveedor(proveedor))
                    {
                        ModelState.AddModelError("", "Ya existe un proveedor con este nombre");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(proveedor);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Modificar Proveedor
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> Proveedor a modificar</param>
        /// <returns>Proveedor Modificado</returns>
        [HttpPut]
        public IHttpActionResult UpdateProveedor([FromBody] Proveedor proveedor)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!proveedorService.UpdateProveedor(proveedor))
                    {
                        ModelState.AddModelError("", "No se encontró Proveedor para actualizar");

                    }

                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok(proveedor);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar Proveedor por id
        /// </summary>
        /// <param name="id"> id rol a eliminar</param>
        /// <returns>Proveedor Eliminado</returns>

        [HttpDelete]
        public IHttpActionResult DeleteProveedor(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!proveedorService.DeleteProveedor(id))
                    {
                        ModelState.AddModelError("", "No se encontró Proveedor para eliminar");

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
