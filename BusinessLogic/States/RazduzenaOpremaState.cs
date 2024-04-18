using BusinessLogic.States.Abstracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class RazduzenaOpremaState : OpremaStateBase
    {
        public override string? Otpisi(Oprema oprema)
        {
            oprema.Stanje = Status.OtpisanoStanje();
            oprema.StanjeId = Status.OtpisanoStanje().StatusId;

            return null;
        }

        public override string? Rashoduj(Oprema oprema)
        {
            oprema.Stanje = Status.RashodovanoStanje();
            oprema.StanjeId = Status.RashodovanoStanje().StatusId;

            return null;
        }

        public override string Razduzi(Oprema oprema)
        {
            return "Razduzena oprema se ne moze razduziti";
        }

        public override string? Zaduzi(Oprema oprema)
        {
            oprema.Stanje = Status.ZaduzenoStanje();
            oprema.StanjeId = Status.ZaduzenoStanje().StatusId;

            return null;
        }
    }
}
