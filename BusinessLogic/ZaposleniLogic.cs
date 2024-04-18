using BusinessLogic.Interfaces;
using DataAccess.EntityFramework.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ZaposleniLogic : IZaposleniLogic
    {
        public ZaposleniLogic(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Task<List<string>> Add(Zaposleni entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Zaposleni>> GetAll()
        {
            return await UnitOfWork.ZaposleniRepository.GetAll();
        }

        public async Task<Zaposleni> GetById(object id)
        {
            return await UnitOfWork.ZaposleniRepository.GetById(id);
        }

        public Task<List<string>> Update(Zaposleni entity)
        {
            throw new NotImplementedException();
        }
    }
}
