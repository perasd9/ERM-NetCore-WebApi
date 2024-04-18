using BusinessLogic.Interfaces;
using BusinessLogic.States.Abstracts;
using BusinessLogic.Validation;
using DataAccess.EntityFramework.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class OpremaLogic : IOpremaLogic
    {
        public OpremaLogic(IUnitOfWork unitOfWork, OpremaValidator opremaValidator)
        {
            UnitOfWork = unitOfWork;
            OpremaValidator = opremaValidator;
        }

        public IUnitOfWork UnitOfWork { get; }
        public OpremaValidator OpremaValidator { get; }

        public async Task<List<string>?> Add(Oprema entity)
        {
            var validationResult = await OpremaValidator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            var listaOpreme = await UnitOfWork.OpremaRepository.GetAll();

            foreach(var oprema in listaOpreme)
                if(oprema.Naziv == entity.Naziv && oprema.InventarskiBroj == entity.InventarskiBroj
                    && oprema.TipOpremeId == entity.TipOpremeId)
                {
                    entity.SerijskiBroj = oprema.SerijskiBroj;
                    entity.Kolicina += oprema.Kolicina;

                    if(oprema.StanjeId != Status.ZaduzenoStanje().StatusId && oprema.StanjeId != Status.RazduzenoStanje().StatusId)
                    {
                        var state = Activator.CreateInstance(Type.GetType(oprema.Stanje.GetImeStanja())) as OpremaStateBase;

                        var stateResult = state?.Razduzi(oprema);

                        if (stateResult != null) return new List<string>() { stateResult };
                    }else if(oprema.StanjeId == Status.ZaduzenoStanje().StatusId)
                    {
                        new ZaduzenaOpremaState().Razduzi(entity);
                    }

                    await UnitOfWork.OpremaRepository.Update(entity);
                    await UnitOfWork.SaveChanges();

                    return null;
                }
            entity.StanjeId = Status.RazduzenoStanje().StatusId;

            await UnitOfWork.OpremaRepository.Add(entity);
            await UnitOfWork.SaveChanges();

            return null;
        }

        public async Task Delete(object id)
        {
            await UnitOfWork.OpremaRepository.Delete(id);
            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Oprema>> GetAll()
        {
            return await UnitOfWork.OpremaRepository.GetAll();
        }

        public async Task<Oprema> GetById(object id)
        {
            return await UnitOfWork.OpremaRepository.GetById(id);
        }

        public async Task<List<Oprema>> GetSortedPerKolicina(bool asc)
        {
            return await UnitOfWork.OpremaRepository.GetSortedPerKolicina(asc);
        }

        public async Task<List<Oprema>> GetSortedPerNaziv()
        {
            return await UnitOfWork.OpremaRepository.GetSortedPerNaziv();
        }

        public async Task<List<string>?> Otpisi(Oprema entity)
        {
            var oprema = await UnitOfWork.OpremaRepository.GetById(entity.SerijskiBroj);

            List<Zaduzivanje> listaZaduzivanja = (List<Zaduzivanje>)await UnitOfWork.ZaduzivanjeRepository.GetAll();

            if(listaZaduzivanja.FirstOrDefault(zad => (zad.DatumRazduzivanja == null && zad.SerijskiBroj == oprema.SerijskiBroj)) != null)
                return new List<string>() { "Deo opreme je vec zaduzeno i nalazi se u nekom kabinetu" };

            var state = Activator.CreateInstance(Type.GetType(oprema.Stanje.GetImeStanja())) as OpremaStateBase;

            var stateResult = state?.Otpisi(oprema);

            if (stateResult != null) return new List<string>() { stateResult };

            await UnitOfWork.OpremaRepository.ChangeState(oprema);
            await UnitOfWork.SaveChanges();

            return null;
        }

        public async Task<List<string>?> Rashoduj(Oprema entity)
        {
            var oprema = await UnitOfWork.OpremaRepository.GetById(entity.SerijskiBroj);

            List<Zaduzivanje> listaZaduzivanja = (List<Zaduzivanje>)await UnitOfWork.ZaduzivanjeRepository.GetAll();

            if (listaZaduzivanja.FirstOrDefault(zad => zad?.DatumRazduzivanja == null && zad.SerijskiBroj == oprema.SerijskiBroj) != null)
                return new List<string>() { "Deo opreme je vec zaduzeno i nalazi se u nekom kabinetu" };

            var state = Activator.CreateInstance(Type.GetType(oprema.Stanje.GetImeStanja())) as OpremaStateBase;

            var stateResult = state.Rashoduj(oprema);

            if (stateResult != null) return new List<string>() { stateResult };

            await UnitOfWork.OpremaRepository.ChangeState(oprema);
            await UnitOfWork.SaveChanges();

            return null;
        }

        public async Task<List<string>?> Update(Oprema entity)
        {
            var validationResult = await OpremaValidator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            await UnitOfWork.OpremaRepository.Update(entity);
            await UnitOfWork.SaveChanges();

            return null;
        }
    }
}
