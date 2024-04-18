using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.ZaposleniRepo
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        private readonly OpremaContext context;

        public ZaposleniRepository(OpremaContext context)
        {
            this.context = context;
        }
        public Task Add(Zaposleni entity)
        {
            //await context.Zaposleni.AddAsync(entity);
            //context.Entry(entity.Kabinet).State = EntityState.Unchanged;
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            //context.Zaposleni.Remove(await context.Zaposleni.FindAsync(id));
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zaposleni>> GetAll()
        {
            return await context.Zaposleni.Include(x => x.Kabinet).ToListAsync();
        }

        public async Task<Zaposleni> GetById(object id)
        {
            return await context.Zaposleni.Include(x => x.Kabinet).FirstOrDefaultAsync(y => y.Email == id.ToString())
                ?? throw new ArgumentNullException(nameof(Zaposleni), "Zaposleni sa trazenim kljucem ne postoji!");
        }

        public Task Update(Zaposleni entity)
        {
            //context.Zaposleni.Update(entity);

            //return Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
