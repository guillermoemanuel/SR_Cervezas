using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Service
{
    public class RolService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<Rol> Get()
        {
            return meetUpCervezasEntities.Rol.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public Rol Get(int id)
        {

            return dbContext.Rol.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertRol(Rol rol)
        {
         
                var rolCompare = dbContext.Rol.FirstOrDefault(r => r.Nombre == rol.Nombre);

                //si existe un nombre igual
                if (rolCompare != null)
                    return true;
                else
                {
                    dbContext.Rol.Add(rol);
                    dbContext.SaveChanges();
                    return false;
                }
                              
        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdateRol(Rol rol)
        {
            var rolCompare = meetUpCervezasEntities.Rol.FirstOrDefault(r=> r.Id == rol.Id);

            //si existe un rol igual
            if (rolCompare == null)
                return false;
            else
            {
                dbContext.Entry(rol).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeleteRol(int id)
        {
            var rolCompare = dbContext.Rol.FirstOrDefault(r => r.Id == id);

            //si existe un rol igual
            if (rolCompare == null)
                return false;
            else
            {
                dbContext.Rol.Remove(rolCompare);
                dbContext.SaveChanges();
                return true;
            }
        }


    }
}