using DB;
using MeetUpCervezas.Business.enums;
using MeetUpCervezas.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class PedidoService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<Pedido> Get()
        {
            return meetUpCervezasEntities.Pedido.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public Pedido Get(int id)
        {

            return dbContext.Pedido.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertPedido(int idProveedor, int idMeetUp, float temp)
        {            
                                          
            var pedidoCompare = dbContext.Pedido.FirstOrDefault(p => p.IdMeetup == idMeetUp);

            //si existe un nombre igual
            if (pedidoCompare != null)
                return true;
            else
            {
                var asistentes = dbContext.Asistentes.Where(a => a.IdMeetup == idMeetUp).ToList();

                Pedido pedido = new PedidoBuscador().BuscarPedido(temp, asistentes.Count());
                pedido.IdProveedor = idProveedor;
                pedido.IdMeetup = idMeetUp;
                pedido.Estado = (int)EEstado.Activo;

                dbContext.Pedido.Add(pedido);
                dbContext.SaveChanges();
                return false;
            }

        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdatePedido(Pedido pedido)
        {
            var pedidoCompare = meetUpCervezasEntities.Pedido.FirstOrDefault(u => u.Id == pedido.Id);

            //si existe un rol igual
            if (pedidoCompare == null)
                return false;
            else
            {
                dbContext.Entry(pedido).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeletePedido(int id)
        {
            var pedidoCompare = dbContext.Pedido.FirstOrDefault(u => u.Id == id);

            //si existe un rol igual
            if (pedidoCompare == null)
                return false;
            else
            {
                dbContext.Pedido.Remove(pedidoCompare);
                dbContext.SaveChanges();
                return true;
            }
        }

    }
}