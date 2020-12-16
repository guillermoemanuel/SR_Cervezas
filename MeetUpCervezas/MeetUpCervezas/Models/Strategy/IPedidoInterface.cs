using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpCervezas.Models.Strategy
{
    public interface IPedidoInterface
    {
        Pedido buscarPedido(int participantes);     
               
    }
}
