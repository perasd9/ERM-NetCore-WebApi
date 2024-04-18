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
    public class KabinetLogic : IKabinetLogic
    {
        public KabinetLogic(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Task<List<string>?> Add(Kabinet entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Kabinet>> GetAll()
        {
            return await UnitOfWork.KabinetRepository.GetAll();
        }

        public async Task<Kabinet> GetById(object id)
        {
            return await UnitOfWork.KabinetRepository.GetById(id);
        }

        public Task<List<string>?> Update(Kabinet entity)
        {
            throw new NotImplementedException();
        }
    }
}
