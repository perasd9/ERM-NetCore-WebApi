using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.KabinetRepo
{
    public class KabinetRepository : IKabinetRepository
    {
        private readonly OpremaContext context;

        public KabinetRepository(OpremaContext context)
        {
            this.context = context;
        }
        public Task Add(Kabinet entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Kabinet>> GetAll()
        {
            return await context.Kabineti.ToListAsync();
        }

        public async Task<Kabinet> GetById(object id)
        {
            return await context.Kabineti.FindAsync((int)id) ?? throw new ArgumentNullException(nameof(Kabinet), "Kabinet sa trazenim kljucem ne postoji!");
        }

        public Task Update(Kabinet entity)
        {
            throw new NotImplementedException();
        }
    }
}
