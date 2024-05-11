using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.ZaduzivanjeRepo
{
    public class ZaduzivanjeRepository : IZaduzivanjeRepository
    {
        private readonly OpremaContext context;

        public ZaduzivanjeRepository(OpremaContext context)
        {
            this.context = context;
        }
        public async Task Add(Zaduzivanje entity)
        {
            //kada se dodaje kolicina u opremi se smanjuje
            await context.Zaduzivanje.AddAsync(entity);
            var item = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            item.Kolicina -= entity.BrojKomada;
            item.StanjeId = entity.Oprema.StanjeId;

            context.Entry(entity.Oprema).State = EntityState.Modified;
            context.Entry(item.TipOpreme).State = EntityState.Unchanged;
            context.Entry(item.Stanje).State = EntityState.Unchanged;
        }

        public async Task Delete(object id)
        {
            //kada se brise record kolicina u opremi se povecava
            var entity = await GetById(id) ?? throw new ArgumentNullException(nameof(Zaduzivanje), "Zaduzivanje sa trazenim kljucem ne postoji!");

            context.Zaduzivanje.Remove(entity);
            context.Entry(entity.Zaposleni).State = EntityState.Unchanged;
            var item = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            item.Kolicina += entity.BrojKomada;
            context.Entry(entity.Oprema).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Zaduzivanje>> GetAll()
        {
            return await context.Zaduzivanje.Include(x => x.Zaposleni).Include(y => y.Oprema).AsNoTracking().Include(k => k.Kabinet).ToListAsync();
        }

        public async Task<Zaduzivanje> GetById(object id)
        {
            return await context.Zaduzivanje.Include(x => x.Zaposleni).Include(y => y.Oprema).FirstAsync(z => z.ZaduzivanjeId == (int)id);
        }

        public async Task<PaginatedListZaduzivanja> GetPaginatedList(int pageIndex, int pageSize)
        {
            var zaduzivanja = await context.Zaduzivanje.Include(x => x.Zaposleni).
                Include(y => y.Oprema).AsNoTracking().Include(k => k.Kabinet).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedListZaduzivanja()
            {
                TotalPages = (int)Math.Ceiling(await context.Zaduzivanje.CountAsync() / (double) pageSize),
                Items = zaduzivanja,
                PageIndex = pageIndex,
            };
        }

        public async Task<List<Zaduzivanje>> GetPerKabinet()
        {

            var lista = context.Zaduzivanje.Include(zad => zad.Oprema).Include(zad => zad.Kabinet)
            .AsEnumerable().GroupBy(zad => new { zad.Kabinet, zad.Oprema }).Select(grupa => new Zaduzivanje
            {
                Kabinet = grupa.Key.Kabinet,
                Oprema = grupa.Key.Oprema,
                BrojKomada = grupa.Sum(z => z.BrojKomada),
            }).OrderBy(kab => kab.Kabinet.Naziv).ToList();

            //await context.Zaduzivanje.GroupBy(z => new { z.Zaposleni }).Include(x => x.Zaposleni).Include(y => y.Oprema).Include(k => k.Kabinet).ToListAsync();
            return lista;
        }

        public async Task<List<Zaduzivanje>> GetPerOneOprema(Guid serijskiBroj)
        {
            return await context.Zaduzivanje.Include(x => x.Zaposleni).Include(y => y.Oprema).Where(x => x.SerijskiBroj == serijskiBroj)
                .AsNoTracking().Include(k => k.Kabinet).ToListAsync();
        }

        public async Task<List<Zaduzivanje>> GetPerOneZaposleni(string email)
        {
            return await context.Zaduzivanje.Include(x => x.Zaposleni).Include(y => y.Oprema).Where(x => x.Email == email)
                .AsNoTracking().Include(k => k.Kabinet).ToListAsync();
        }

        public async Task<List<Zaduzivanje>> GetPerZaposleni()
        {
            #region
            //var grupisanaLista = lista.GroupBy(z => new { z.Zaposleni, z.Oprema })
            //              .Select(grupa => new
            //              {
            //                  Zaposleni = grupa.Key.Zaposleni,
            //                  Oprema = grupa.Key.Oprema,
            //                  Kolicina = grupa.Sum(z => z.BrojKomada)
            //              }).OrderBy(x => x.Zaposleni.Email);


            //foreach (var grupa in grupisanaLista.GroupBy(gr => gr.Zaposleni))
            //{
            //    Console.WriteLine($"Zaposleni: {grupa.Key.Ime} {grupa.Key.Prezime}");

            //    foreach (var stavkaOpreme in grupa)
            //    {
            //        Console.WriteLine($"  Oprema: {stavkaOpreme.Oprema.Naziv}, Količina: {stavkaOpreme.Kolicina}");
            //    }

            //    Console.WriteLine();
            //}
            #endregion

            //korisceno asEnumerable da bi se izbeglo kllijentsko prevodjenje upita
            var lista = context.Zaduzivanje.Include(zad => zad.Zaposleni).Include(zad => zad.Oprema).Include(zad => zad.Kabinet)
                .AsEnumerable().
                        GroupBy(zad => new { zad.Zaposleni, zad.Oprema, zad.Kabinet}).Select(grupa => new Zaduzivanje
                            {
                                Zaposleni = grupa.Key.Zaposleni,
                                Oprema = grupa.Key.Oprema,
                                Kabinet = grupa.Key.Kabinet,
                                BrojKomada = grupa.Sum(z => z.BrojKomada),
                            }).OrderBy(zap => zap.Zaposleni.Ime).ToList();

            //await context.Zaduzivanje.GroupBy(z => new { z.Zaposleni }).Include(x => x.Zaposleni).Include(y => y.Oprema).Include(k => k.Kabinet).ToListAsync();
            return lista;
        }

        public async Task Update(Zaduzivanje entity)
        {
            //na update se takodje menja i kolicina ukupno u opremi
            var zaduzivanjeTemp = await GetById(entity.ZaduzivanjeId) ?? throw new ArgumentNullException(nameof(Zaduzivanje), "Zaduzivanje sa trazenim kljucem ne postoji!");

            zaduzivanjeTemp.DatumRazduzivanja = entity.DatumRazduzivanja != null ? entity.DatumRazduzivanja : null;

            var item = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            item.Kolicina += zaduzivanjeTemp.BrojKomada;
            item.Kolicina -= entity.BrojKomada;
            zaduzivanjeTemp.BrojKomada = entity.BrojKomada;
            item.StanjeId = entity.Oprema.StanjeId;
        }

        public async Task UpdateRazduzivanje(Zaduzivanje entity)
        {
            var zaduzivanjeTemp = await GetById(entity.ZaduzivanjeId) ?? throw new ArgumentNullException(nameof(Zaduzivanje), "Zaduzivanje sa trazenim kljucem ne postoji!");

            zaduzivanjeTemp.DatumRazduzivanja = entity.DatumRazduzivanja;

            var item = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            context.Entry(entity.Oprema.Stanje).State = EntityState.Unchanged;

            item.Kolicina += zaduzivanjeTemp.BrojKomada;
            item.StanjeId = entity.Oprema.StanjeId;
        }
    }
}
