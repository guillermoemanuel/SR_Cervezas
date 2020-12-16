using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Models.Strategy
{
    public class PedidoBuscador
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();
        public Pedido BuscarPedido(float temp, int participantes)
        {
            var regla1 = dbContext.ParametrosPedido.FirstOrDefault(p=> p.Nombre=="Regla1");
            var regla2 = dbContext.ParametrosPedido.FirstOrDefault(p => p.Nombre == "Regla2");
            var regla3 = dbContext.ParametrosPedido.FirstOrDefault(p => p.Nombre == "Regla3");

            IPedidoInterface iPedidoStratategy = null;

            if(temp >= regla1.TemperaturaMinima && temp <= regla1.TemperaturaMaxima)
            {
                iPedidoStratategy = new Regla1();
            }
            else if (temp < regla2.TemperaturaMaxima)
            {
                iPedidoStratategy = new Regla2();
            }
            else if (temp > regla3.TemperaturaMinima)
            {
                iPedidoStratategy = new Regla3();
            }
            else
            {
                throw new Exception();
            }

            return iPedidoStratategy.buscarPedido(participantes);
        }
    }
}