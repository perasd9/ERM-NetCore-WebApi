using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.OpremaRepo
{
    public class OpremaRepository : IOpremaRepository
    {
        private readonly OpremaContext context;

        public OpremaRepository(OpremaContext context)
        {
            this.context = context;
        }
        public async Task Add(Oprema entity)
        {
            await context.Oprema.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            var oprema = await context.Oprema.FindAsync((Guid)id) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");
            
            context.Oprema.Remove(oprema);
        }

        public async Task<IEnumerable<Oprema>> GetAll()
        {
            return await context.Oprema.Include(x => x.TipOpreme).Include(x => x.Stanje).AsNoTracking().ToListAsync();
        }

        public async Task<Oprema> GetById(object id)
        {
            return await context.Oprema.Include(x => x.TipOpreme).Include(x => x.Stanje).AsNoTracking().FirstAsync(y => y.SerijskiBroj == (Guid)id);
        }

        public async Task<List<Oprema>> GetSortedPerKolicina(bool asc)
        {
            return asc ? await context.Oprema.Include(x => x.TipOpreme).Include(x => x.Stanje).AsNoTracking()
                .OrderBy(x => x.Kolicina).ToListAsync() :
                await context.Oprema.Include(x => x.TipOpreme).Include(x => x.Stanje).AsNoTracking()
                .OrderByDescending(x => x.Kolicina).ToListAsync();
        }

        public async Task<List<Oprema>> GetSortedPerNaziv()
        {
            return await context.Oprema.Include(x => x.TipOpreme).Include(x => x.Stanje).AsNoTracking()
                .OrderBy(x => x.Naziv).ToListAsync();
        }

        public async Task Update(Oprema entity)
        {
            var opremaTemp = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            opremaTemp.Naziv = entity.Naziv;
            opremaTemp.InventarskiBroj = entity.InventarskiBroj;
            opremaTemp.TipOpremeId = entity.TipOpremeId;
            opremaTemp.Kolicina = entity.Kolicina;
            opremaTemp.StanjeId = entity.StanjeId;

            context.Entry(opremaTemp).State = EntityState.Modified;
        }
        public async Task ChangeState(Oprema entity)
        {
            var opremaTemp = await context.Oprema.FindAsync(entity.SerijskiBroj) ?? throw new ArgumentNullException(nameof(Oprema), "Oprema sa trazenim kljucem ne postoji!");

            opremaTemp.Stanje = entity.Stanje;
            context.Entry(opremaTemp.Stanje).State = EntityState.Unchanged;
        }

    }
}
