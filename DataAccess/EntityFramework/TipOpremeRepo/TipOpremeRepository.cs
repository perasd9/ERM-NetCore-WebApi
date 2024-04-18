using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.TipOpremeRepo
{
    public class TipOpremeRepository : ITipOpremeRepository
    {
        //implementiramo interfejs za sve domenske objekte u slucaju da u buducnosti zelimo da implementiramo operacije za sve objekte
        private readonly OpremaContext context;

        public TipOpremeRepository(OpremaContext context)
        {
            this.context = context;
        }
        public Task Add(TipOpreme entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipOpreme>> GetAll()
        {
            return await context.TipOpreme.Where(to => to.NadtipId != null).ToListAsync();
        }

        public async Task<TipOpreme> GetById(object id)
        {
            return await context.TipOpreme.FindAsync((int)id) ?? throw new ArgumentNullException(nameof(TipOpreme), "Tip opreme sa trazenim kljucem ne postoji!");
        }

        public Task Update(TipOpreme entity)
        {
            throw new NotImplementedException();
        }
    }
}
