using BusinessLogic.Interfaces;
using BusinessLogic.States.Abstracts;
using BusinessLogic.Validation;
using DataAccess.EntityFramework.UnitOfWork;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ZaduzivanjeLogic : IZaduzivanjeLogic
    {
        public ZaduzivanjeLogic(IUnitOfWork unitOfWork, ZaduzivanjeValidator zaduzivanjeValidator)
        {
            UnitOfWork = unitOfWork;
            ZaduzivanjeValidator = zaduzivanjeValidator;
        }

        public IUnitOfWork UnitOfWork { get; }
        public ZaduzivanjeValidator ZaduzivanjeValidator { get; }

        public async Task<List<string>?> Add(Zaduzivanje entity)
        {
            var validationResult = ZaduzivanjeValidator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }
            await UnitOfWork.ZaduzivanjeRepository.Add(entity);

            return null;
        }

        public async Task<List<string>?> AddRange(List<Zaduzivanje> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    var validationResult = ZaduzivanjeValidator.Validate(entity);
                    if (!validationResult.IsValid)
                    {
                        return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
                    }
                    var listaZaduzivanja = await UnitOfWork.ZaduzivanjeRepository.GetAll();
                    var oprema = await UnitOfWork.OpremaRepository.GetById(entity.SerijskiBroj);

                    //provera da li je broj komada za zaduzivanje moguce zapravo i zaduziti
                    if (oprema.Kolicina < entity.BrojKomada)
                        return new List<string>() { "Broj komada je veci od kolicine" };


                    var state = Activator.CreateInstance(Type.GetType(oprema.Stanje.GetImeStanja())) as OpremaStateBase;

                    var stateResult = state?.Zaduzi(oprema);

                    if (stateResult != null) return new List<string>() { stateResult };


                    new ZaduzenaOpremaState().Razduzi(oprema);

                    if (oprema.Kolicina == entity.BrojKomada)
                    {
                        new RazduzenaOpremaState().Zaduzi(oprema);
                    }

                    entity.Oprema = oprema;

                    //provera ako vec menjamo nesto sto je vec zaduzeno samo cemo
                    //nadodati na to a ne da zapravo pamtimo jos jedan record zaduzivanja
                    var zaduzenje = listaZaduzivanja
                        .FirstOrDefault(zad => entity.Email == zad.Email && entity.SerijskiBroj == zad.SerijskiBroj && zad.DatumRazduzivanja == null);
                    
                    if(zaduzenje != null)
                    {
                        entity.BrojKomada += zaduzenje.BrojKomada;
                        entity.ZaduzivanjeId = zaduzenje.ZaduzivanjeId;
                        await Update(entity);
                        await UnitOfWork.SaveChanges();
                        continue;
                    }

                    //ako nista od ovoga iznad nije bio slucaj idemo i klasicno dodavanje
                    //ako nema poruka za greske vratice se null a ne prazna lista poruka
                    await Add(entity);
                    await UnitOfWork.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task Delete(object id)
        {
            await UnitOfWork.ZaduzivanjeRepository.Delete(id);
            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Zaduzivanje>> GetAll()
        {
            return await UnitOfWork.ZaduzivanjeRepository.GetAll();
        }

        public async Task<Zaduzivanje> GetById(object id)
        {
            return await UnitOfWork.ZaduzivanjeRepository.GetById(id);
        }

        public async Task<List<Zaduzivanje>> GetPerKabinet()
        {
            return await UnitOfWork.ZaduzivanjeRepository.GetPerKabinet();
        }

        public async Task<List<Zaduzivanje>> GetPerZaposleni()
        {
            return await UnitOfWork.ZaduzivanjeRepository.GetPerZaposleni();
        }

        public async Task<List<string>?> Update(Zaduzivanje entity)
        {
            var validationResult = await ZaduzivanjeValidator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            await UnitOfWork.ZaduzivanjeRepository.Update(entity);
            await UnitOfWork.SaveChanges();

            return null;
        }
        public async Task<List<string>?> Razduzi(Zaduzivanje entity)
        {
            var validationResult = await ZaduzivanjeValidator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Select(err => err.ErrorMessage).ToList();
            }

            entity.Oprema = await UnitOfWork.OpremaRepository.GetById(entity.SerijskiBroj);

            List<Zaduzivanje> listaZaduzivanja = (List<Zaduzivanje>)await UnitOfWork.ZaduzivanjeRepository.GetAll();

            if(listaZaduzivanja.FirstOrDefault(zad => zad.DatumRazduzivanja != null && entity.ZaduzivanjeId == zad.ZaduzivanjeId) != null)
                return new List<string>() { "Ova oprema je vec razduzena" };


            if (entity.Oprema.StanjeId != Status.RazduzenoStanje().StatusId)
            {
                var state = Activator.CreateInstance(Type.GetType(entity.Oprema.Stanje.GetImeStanja())) as OpremaStateBase;

                var stateResult = state.Razduzi(entity.Oprema);
                if (stateResult != null) return new List<string>() { stateResult };
            }

            await UnitOfWork.ZaduzivanjeRepository.UpdateRazduzivanje(entity);
            await UnitOfWork.SaveChanges();

            return null;
        }
    }
}
