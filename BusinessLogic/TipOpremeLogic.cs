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
    public class TipOpremeLogic : ITipOpremeLogic
    {
        public TipOpremeLogic(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Task<List<string>?> Add(TipOpreme entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipOpreme>> GetAll()
        {
            return await UnitOfWork.TipOpremeRepository.GetAll();
        }

        public async Task<IEnumerable<TipOpreme>> GetAllSubtypes(int subtype)
        {
            return await UnitOfWork.TipOpremeRepository.GetAllSubtypes(subtype);
        }

        public async Task<TipOpreme> GetById(object id)
        {
            return await UnitOfWork.TipOpremeRepository.GetById(id);
        }

        public Task<List<string>?> Update(TipOpreme entity)
        {
            throw new NotImplementedException();
        }
    }
}
