using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MeetUpCervezas.Business.Service;
using MeetUpCervezas.Models;


namespace MeetUpCervezas.Controllers
{
    [Authorize]
    public class TemperaturaController : ApiController
    {
        TemperaturaService temperaturaService = new TemperaturaService();

        /// <summary>
        /// Devuelve la temperatura de la ciudad seleccionada 
        /// </summary>
        /// <param name="city">ciudad nombre</param>
        /// <param name="country">pais  2 caracteres AR</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Temperatura")]
        public Temperatura Get(string city, string country)
        {
            string key = "9ce5dea30a4d48f293ee4268d53c11e9";
            //consulta a weaterbit para temperatura de mendoza https://api.weatherbit.io/v2.0/current?city=Mendoza&country=AR&key=9ce5dea30a4d48f293ee4268d53c11e9

            string url = "https://api.weatherbit.io/v2.0/current?city="+city+"&country="+country+"&key="+key;

            return temperaturaService.Get(url);
          
        }
    }
}
