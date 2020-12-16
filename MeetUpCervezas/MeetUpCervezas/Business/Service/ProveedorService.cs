using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class ProveedorService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<Proveedor> Get()
        {
            return meetUpCervezasEntities.Proveedor.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public Proveedor Get(int id)
        {

            return dbContext.Proveedor.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertProveedor(Proveedor proveedor)
        {

            var proveedorCompare = dbContext.Proveedor.FirstOrDefault(u => u.Proveedor1 == proveedor.Proveedor1);

            //si existe un nombre igual
            if (proveedorCompare != null)
                return true;
            else
            {
                dbContext.Proveedor.Add(proveedor);
                dbContext.SaveChanges();
                return false;
            }

        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdateProveedor(Proveedor proveedor)
        {
            var proveedorCompare = meetUpCervezasEntities.Proveedor.FirstOrDefault(u => u.Id == proveedor.Id);

            //si existe un rol igual
            if (proveedorCompare == null)
                return false;
            else
            {
                dbContext.Entry(proveedor).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeleteProveedor(int id)
        {
            var proveedorCompare = dbContext.Proveedor.FirstOrDefault(u => u.Id == id);

            //si existe un rol igual
            if (proveedorCompare == null)
                return false;
            else
            {
                dbContext.Proveedor.Remove(proveedorCompare);
                dbContext.SaveChanges();
                return true;
            }
        }

    }

}