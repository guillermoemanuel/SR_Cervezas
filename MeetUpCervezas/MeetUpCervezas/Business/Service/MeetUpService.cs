using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class MeetUpService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        private MeetUpCervezasEntities meetUpCervezasEntities = new MeetUpCervezasEntities();


        /// <summary>
        ///  lista de Roles
        /// </summary>
        /// <returns>Lista de Roles</returns>
        public List<Meetup> Get()
        {
            return meetUpCervezasEntities.Meetup.ToList();
        }

        /// <summary>
        /// Obtener rol por id
        /// </summary>
        /// <param name="id"> id rol a buscar</param>
        /// <returns>Rol</returns>
        public Meetup Get(int id)
        {

            return dbContext.Meetup.FirstOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Inserta Rol validadando que no exista
        /// </summary>
        /// <param name="rol"> rol a insertar</param>
        /// <returns>bool</returns>
        public bool InsertMeetup(Meetup meetUp)
        {

            var meetUpCompare = dbContext.Meetup.FirstOrDefault(m => m.Titulo == meetUp.Titulo);

            //si existe un nombre igual
            if (meetUpCompare != null)
                return true;
            else
            {
                dbContext.Meetup.Add(meetUp);
                dbContext.SaveChanges();
                return false;
            }

        }

        /// <summary>
        /// Modificar Rol y valida que el rol a modificar exista
        /// </summary>
        /// <param name="rol">rol a modificar</param>
        /// <returns>bool</returns>
        public bool UpdateMeetup(Meetup meetUp)
        {
            var meetUpCompare = meetUpCervezasEntities.Meetup.FirstOrDefault(m => m.Id == meetUp.Id);

            //si existe un rol igual
            if (meetUpCompare == null)
                return false;
            else
            {
                dbContext.Entry(meetUp).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// Eliminar Rol por Id
        /// </summary>
        /// <param name="id">id del rol a eliminar</param>
        /// <returns>bool</returns>
        public bool DeleteMeetup(int id)
        {
            var meetUpCompare = dbContext.Meetup.FirstOrDefault(m => m.Id == id);

            //si existe un rol igual
            if (meetUpCompare == null)
                return false;
            else
            {
                dbContext.Meetup.Remove(meetUpCompare);
                dbContext.SaveChanges();
                return true;
            }
        }

    }
}