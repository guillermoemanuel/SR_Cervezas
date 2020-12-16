using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MeetUpCervezas.Models;
using Newtonsoft.Json;

namespace MeetUpCervezas.Business.Service
{
    public class TemperaturaService
    {
        Temperatura temperatura = new Temperatura();

        public Temperatura Get(string url)
        {
            
            //Se configura la petición.
            HttpWebRequest peticion;
            peticion = WebRequest.Create(url) as HttpWebRequest;
            peticion.ContentType = "application/json; charset=utf-8";
            peticion.Method = "GET";

            // Respuesta
            try
            {
                HttpWebResponse respuesta = peticion.GetResponse() as HttpWebResponse;

                //Si la peticion fue correcta
                if ((int)respuesta.StatusCode == 200)
                {
                    var reader = new StreamReader(respuesta.GetResponseStream());
                    string s = reader.ReadToEnd();
                    dynamic DOJson = JsonConvert.DeserializeObject(s);
                   
                  
                    //objeto temperatura
                    temperatura.app_temp = DOJson.data[0].app_temp;
                   
                }
                else
                {
                    //mensaje de error
                    Console.WriteLine("error armado de objeto");
                }
            }
            catch (Exception ex)
            {
                //muestro el error
                Console.WriteLine(ex.Message);
            }
            return temperatura;
        }
    }
      
}