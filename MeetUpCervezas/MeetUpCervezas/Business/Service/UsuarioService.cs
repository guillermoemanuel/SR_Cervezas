using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class UsuarioService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<Usuario> Get()
        {
            return meetUpCervezasEntities.Usuario.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public Usuario Get(int id)
        {

            return dbContext.Usuario.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertUsuario(Usuario usuario)
        {

            var usuarioCompare = dbContext.Usuario.FirstOrDefault(u => u.Nombre == usuario.Nombre);

            //si existe un nombre igual
            if (usuarioCompare != null)
                return true;
            else
            {
                dbContext.Usuario.Add(usuario);
                dbContext.SaveChanges();
                return false;
            }

        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdateUsuario(Usuario usuario)
        {
            var usuarioCompare = meetUpCervezasEntities.Usuario.FirstOrDefault(u => u.Id == usuario.Id);

            //si existe un rol igual
            if (usuarioCompare == null)
                return false;
            else
            {
                dbContext.Entry(usuario).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeleteUsuario(int id)
        {
            var usuarioCompare = dbContext.Usuario.FirstOrDefault(u => u.Id == id);

            //si existe un rol igual
            if (usuarioCompare == null)
                return false;
            else
            {
                dbContext.Usuario.Remove(usuarioCompare);
                dbContext.SaveChanges();
                return true;
            }
        }

    }
}