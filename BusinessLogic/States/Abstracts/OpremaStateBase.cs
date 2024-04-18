using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.States.Abstracts
{
    public abstract class OpremaStateBase
    {
        public abstract string? Otpisi(Oprema oprema);
        public abstract string? Rashoduj(Oprema oprema);
        public abstract string? Zaduzi(Oprema oprema);
        public abstract string? Razduzi(Oprema oprema);
    }
}
