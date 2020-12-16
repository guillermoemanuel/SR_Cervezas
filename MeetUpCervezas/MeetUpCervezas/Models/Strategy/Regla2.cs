using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Models.Strategy
{
    public class Regla2 : IPedidoInterface
    {
        private MeetUpCervezasEntities dbContext = new MeetUpCervezasEntities();

        public Pedido buscarPedido(int participantes)
        {
            decimal totalPacks = 0;
            decimal packs = 0;

            var parametros = dbContext.ParametrosPedido.FirstOrDefault(p => p.Nombre == "Regla2");
            packs = (decimal)((parametros.LitrosXPersona * participantes) / parametros.unidadesXPack);

            totalPacks = Math.Ceiling(packs);

            Pedido pedido = new Pedido();
            pedido.cantidadPacks = (int)totalPacks;
            return pedido;
        }
    }
}