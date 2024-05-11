using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.ZaduzivanjeRepo
{
    public interface IZaduzivanjeRepository : IRepository<Zaduzivanje>
    {
        public Task<List<Zaduzivanje>> GetPerZaposleni();
        public Task<List<Zaduzivanje>> GetPerKabinet();
        public Task UpdateRazduzivanje(Zaduzivanje entity);
        public Task<PaginatedListZaduzivanja> GetPaginatedList(int pageIndex, int pageSize);
        public Task<List<Zaduzivanje>> GetPerOneZaposleni(string email);
        public Task<List<Zaduzivanje>> GetPerOneOprema(Guid serijskiBroj);
    }
}
