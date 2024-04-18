using DataTransferModel.Oprema;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validation
{
    public class OpremaValidator : AbstractValidator<Oprema>
    {
        public OpremaValidator() {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Kolicina).GreaterThan(0).WithMessage("Kolicina ne sme biti manja od nule").Must(BeInteger).WithMessage("Kolicina sme sadrzai samo brojeve");
            RuleFor(x => x.Naziv).MinimumLength(3).WithMessage("Naziv mora sadrzati najmanje 3 karaktera");
            RuleFor(x => x.Naziv).NotEmpty().WithMessage("Naziv ne sme biti prazan");
            RuleFor(x => x.InventarskiBroj).NotEmpty().WithMessage("Inventarski broj ne sme biti prazan");
            RuleFor(x => x.TipOpremeId).GreaterThan(0).WithMessage("Tip opreme je obavezan");
        }
        private bool BeInteger(int value)
        {
            return value.ToString().All(char.IsDigit);
        }
    }
}
