using System.Web.Http;
using WebActivatorEx;
using MeetUpCervezas;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MeetUpCervezas
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "MeetUpCervezas");

                        c.ApiKey("Token")
            .Description("Filling bearer token here")
            .Name("Authorization")
            .In("header");


                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
