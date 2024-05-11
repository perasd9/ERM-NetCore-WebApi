using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IZaduzivanjeLogic : ILogic<Zaduzivanje>
    {
        //koriscena metoda za rad pod transakcijom i ubacivanje liste entiteta
        //i omoguceno menjanje 2 table u jednom prolazu
        public Task<List<string>?> AddRange(List<Zaduzivanje> entities);
        //filtriranje po zaposlenima
        public Task<List<Zaduzivanje>> GetPerZaposleni();
        //filtriranje po kabinetima
        public Task<List<Zaduzivanje>> GetPerKabinet();
        //razduzivanje
        public Task<List<string>?> Razduzi(Zaduzivanje entity);
        public Task<PaginatedListZaduzivanja?> GetPaginatedList(int pageIndex, int pageSize);
        public Task<List<Zaduzivanje>> GetPerOneZaposleni(string email);
        public Task<List<Zaduzivanje>> GetPerOneOprema(Guid serijskiBroj);

    }
}
