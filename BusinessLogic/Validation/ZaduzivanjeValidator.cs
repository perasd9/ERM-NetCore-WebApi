using DataTransferModel.Zaduzivanje;
using Domain;
using FluentValidation;

namespace BusinessLogic.Validation
{
    public class ZaduzivanjeValidator : AbstractValidator<Zaduzivanje>
    {
        public ZaduzivanjeValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Zaposleni je obavezan");
            RuleFor(x => x.SerijskiBroj).NotNull().NotEmpty().WithMessage("Oprema je obavezna");
            RuleFor(x => x.BrojKomada).GreaterThan(0).WithMessage("Broj komada mora biti veci od nule i ne sme sadrzati slova").Must(BeInteger).WithMessage("Broj komada mora biti veci od nule i ne sme sadrzati slova");
            RuleFor(x => x.DatumZaduzivanja).NotNull().LessThan(DateTime.Now).WithMessage("Datum ne moze biti zabelezen u buducnosti");
        }
        private bool BeInteger(int value)
        {
            return value.ToString().All(char.IsDigit);
        }
    }
}
