using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MeetUpCervezas.Business.Service
{
    public class MailService
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        
        public MailService()
        {

        }

        public Mail SetMail()
        {

            Mail mail = new Mail();

            var mailBuscar = dbContext.Mail.FirstOrDefault();

           
                mail.mailFrom = mailBuscar.mailFrom;
                mail.password = mailBuscar.password;
                mail.host = mailBuscar.host;
                mail.port = mailBuscar.port;
                         
            return mail;
        }

        /// <summary>
        /// Envio de Mails de confirmación 
        /// </summary>
        /// <param name="mail">objetos con datos del remitente</param>
        /// <param name="mailTo">destinatario</param>
        /// <param name="subject">asunto del mail</param>
        /// <param name="body">cuerpo del mail</param>
        /// <returns>valor 0 no enviado 1 enviado</returns>
        public int SendMail(string subject, string mailTo, string body)
        {
            var mail = SetMail();
            
                try
                {
                    //objeto de tipo mail message y parametrizacion
                    MailMessage mailMessage = new MailMessage(mail.mailFrom, mailTo, subject, body);

                    //parametros
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = mail.host;
                    smtpClient.EnableSsl = false;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Port = (int) mail.port;

                    //credenciales
                    smtpClient.Credentials = new NetworkCredential(mail.mailFrom, mail.password);

                    //envio de mail
                    smtpClient.Send(mailMessage);
                    smtpClient.Dispose();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return 0;
                }

                return 1;
        }
                 

    }
}