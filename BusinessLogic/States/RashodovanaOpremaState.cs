using BusinessLogic.States.Abstracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class RashodovanaOpremaState : OpremaStateBase
    {
        public override string Otpisi(Oprema oprema)
        {
            return "Rashodovana oprema se ne moze otpisati";
        }

        public override string Rashoduj(Oprema oprema)
        {
            return "Rashodovana oprema se ne moze rashodovati";
        }

        public override string Razduzi(Oprema oprema)
        {
            return "Rashodovana oprema se ne moze razduziti";
        }

        public override string Zaduzi(Oprema oprema)
        {
            return "Rashodovana oprema se ne moze zaduziti";
        }
    }
}
