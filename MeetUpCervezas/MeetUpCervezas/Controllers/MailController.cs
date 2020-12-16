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
    public class MailController : ApiController
    {
        MailService mailService = new MailService();

        /// <summary>
        /// emvia notificaciones
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="mailTo"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet]
        public int SendMail(string subject, string mailTo, string body)
        {
            return mailService.SendMail(subject, mailTo, body);
        }
    }
}
