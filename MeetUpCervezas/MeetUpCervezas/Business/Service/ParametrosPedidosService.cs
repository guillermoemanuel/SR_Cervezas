using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class ParametrosPedidosService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<ParametrosPedido> Get()
        {
            return meetUpCervezasEntities.ParametrosPedido.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public ParametrosPedido Get(int id)
        {

            return dbContext.ParametrosPedido.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertParametrosPedido(ParametrosPedido parametrosPedido)
        {

            var parametrosPedidoCompare = dbContext.ParametrosPedido.FirstOrDefault(p => p.Nombre == parametrosPedido.Nombre);

            //si existe un nombre igual
            if (parametrosPedidoCompare != null)
                return true;
            else
            {
                dbContext.ParametrosPedido.Add(parametrosPedido);
                dbContext.SaveChanges();
                return false;
            }

        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdateParametrosPedido(ParametrosPedido parametrosPedido)
        {
            var parametrosPedidoCompare = meetUpCervezasEntities.ParametrosPedido.FirstOrDefault(p => p.Id == parametrosPedido.Id);

            //si existe un rol igual
            if (parametrosPedidoCompare == null)
                return false;
            else
            {
                dbContext.Entry(parametrosPedido).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeleteParametrosPedido(int id)
        {
            var parametrosPedidoCompare = dbContext.ParametrosPedido.FirstOrDefault(p => p.Id == id);

            //si existe un rol igual
            if (parametrosPedidoCompare == null)
                return false;
            else
            {
                dbContext.ParametrosPedido.Remove(parametrosPedidoCompare);
                dbContext.SaveChanges();
                return true;
            }
        }
    }
}