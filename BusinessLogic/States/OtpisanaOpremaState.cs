using BusinessLogic.States.Abstracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class OtpisanaOpremaState : OpremaStateBase
    {
        public override string Otpisi(Oprema oprema)
        {
            return "Otpisana oprema se ne moze otpisati";
        }

        public override string Rashoduj(Oprema oprema)
        {
            return "Otpisana oprema se ne moze rashodovati";
        }

        public override string Razduzi(Oprema oprema)
        {
            return "Otpisana oprema se ne moze razduziti";
        }

        public override string Zaduzi(Oprema oprema)
        {
            return "Otpisana oprema se ne moze zaduziti";
        }
    }
}
