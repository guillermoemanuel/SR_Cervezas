using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetUpCervezas.Business.enums
{
    public enum EEstadoMeetUp : short
    {
        NoInvitado = 0,
        Invitado,
        Cancelado,
        Confirmado,
        Rechazado,
        Presenciado
    }
}