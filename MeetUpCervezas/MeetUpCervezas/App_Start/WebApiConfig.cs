﻿using MeetUpCervezas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using static MeetUpCervezas.Controllers.LoginController;
using static MeetUpCervezas.Controllers.UsuarioController;

namespace MeetUpCervezas
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // AÑADE EL HANDLER DE VALIDACIÓN DE TOKENS
            config.MessageHandlers.Add(new TokenValidationHandler());
        }
    }
}
