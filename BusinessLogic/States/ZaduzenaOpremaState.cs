using BusinessLogic.States.Abstracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class ZaduzenaOpremaState : OpremaStateBase
    {
        public override string Otpisi(Oprema oprema)
        {
            return "Zaduzena oprema se ne moze optisati";
        }

        public override string Rashoduj(Oprema oprema)
        {
            return "Zaduzena oprema se ne moze rashodovati";
        }

        public override string? Razduzi(Oprema oprema)
        {
            oprema.Stanje = Status.RazduzenoStanje();
            oprema.StanjeId = Status.RazduzenoStanje().StatusId;

            return null;
        }

        public override string Zaduzi(Oprema oprema)
        {
            return "Zaduzena oprema se ne moze zaduziti";
        }
    }
}
