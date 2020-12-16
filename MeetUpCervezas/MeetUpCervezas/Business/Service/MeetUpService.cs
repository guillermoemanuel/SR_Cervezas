using DB;
using MeetUpCervezas.Business.enums;
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
        public Meetup Get(int? id, DateTime? fecha, DateTime? hora, string titulo, int? estado)
        {

            return dbContext.Meetup.FirstOrDefault(m => m.Id.ToString().Contains(id.ToString())
                 || m.Fecha.ToString().Contains(fecha.ToString())
                 || m.Hora.ToString().Contains(hora.ToString()) 
                 || m.Titulo.Contains(titulo)
                 || m.Estado.ToString().Contains(estado.ToString()));
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


        public bool InvitedAsistente(int idMeetUp, int idUsuario)
        {
          
            var asistenteCompare = dbContext.Asistentes.FirstOrDefault(a => a.IdMeetup == idMeetUp && a.IdUsuario == idUsuario);

            //si existe un detalle igual
            if (asistenteCompare != null)
                return true;
            else
            {
                Asistentes asistente = new Asistentes();
                asistente.IdMeetup = idMeetUp;
                asistente.IdUsuario = idUsuario;
                asistente.Estado = EEstadoMeetUp.Invitado.GetHashCode();

                dbContext.Asistentes.Add(asistente);
                dbContext.SaveChanges();
                return false;
            }

        }

        public bool UpdateAsistenteEstado(int idMeetUp, int idUsuario, int estado)
        {
            var asistenteCompare = meetUpCervezasEntities.Asistentes.FirstOrDefault(a => a.IdMeetup == idMeetUp && a.IdUsuario == idUsuario);

            //si existe un rol igual
            if (asistenteCompare == null)
                return false;
            else
            {
                Asistentes asistente = new Asistentes();
                asistente.IdMeetup = idMeetUp;
                asistente.IdUsuario = idUsuario;
                asistente.Estado = estado;

                dbContext.Entry(asistente).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }

        public List<Usuario> GetAsistentes(int idMeetUp)
        {
            int i = 0;
            List<int> asistentesId = new List<int>();

            var asistentesList = dbContext.Asistentes.Where(a => a.IdMeetup == idMeetUp).ToList();

            for(i=0; i<asistentesList.Count(); i++)
            {
                asistentesId.Add((int)asistentesList[i].IdUsuario);
            }


            return dbContext.Usuario.Where(u => asistentesId.Contains(u.Id)).ToList();

        }

        public List<Meetup> GetMeetUpsAsistentes(int idUsuario)
        {
            int i = 0;
            List<int> meetUpId = new List<int>();

            var asistentesList = dbContext.Asistentes.Where(a => a.IdUsuario == idUsuario).ToList();

            for (i = 0; i < asistentesList.Count(); i++)
            {
                meetUpId.Add((int)asistentesList[i].IdMeetup);
            }


            return dbContext.Meetup.Where(m => meetUpId.Contains(m.Id)).ToList();

        }

    }
}