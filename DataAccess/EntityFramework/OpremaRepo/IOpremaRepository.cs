using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.OpremaRepo
{
    public interface IOpremaRepository : IRepository<Oprema>
    {
        public Task<List<Oprema>> GetSortedPerKolicina(bool asc);
        public Task<List<Oprema>> GetSortedPerNaziv();
        public Task ChangeState(Oprema entity);
    }
}
