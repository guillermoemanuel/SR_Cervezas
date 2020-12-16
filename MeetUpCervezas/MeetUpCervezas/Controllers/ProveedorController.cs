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
    public class ProveedorController : ApiController
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        ProveedorService proveedorService = new ProveedorService();

        /// <summary>
        /// Obtener todos los Roles
        /// </summary>
        /// <returns>Arreglo de Roles</returns>
        [HttpGet]
        public IEnumerable<Proveedor> Get()
        {
            return proveedorService.Get();
        }

        /// <summary>
        /// Obtener un rol filtrado por Id
        /// </summary>
        /// <param name="id"> id rol a obtener</param>
        /// <returns>Rol</returns>
        [HttpGet]
        public Proveedor Get(int id)
        {
            return proveedorService.Get(id);
        }

        /// <summary>
        /// Inserta un Rol
        /// </summary>
        /// <param name="rol">rol a insertar</param>
        /// <returns>Rol insertado</returns>
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
        /// Modificar Rol
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="rol"> rol a modificar</param>
        /// <returns>Rol Modificado</returns>
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
        /// Eliminar Rol por id
        /// </summary>
        /// <param name="id"> id rol a eliminar</param>
        /// <returns>Rol Eliminado</returns>

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
