using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOpremaLogic : ILogic<Oprema>
    {
        public Task<List<Oprema>> GetSortedPerKolicina(bool asc);
        public Task<List<Oprema>> GetSortedPerNaziv();
        public Task<List<string>?> Rashoduj(Oprema entity);
        public Task<List<string>?> Otpisi(Oprema entity);
    }
}
