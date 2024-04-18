using DataTransferModel.Kabinet;
using DataTransferModel.Oprema;
using DataTransferModel.Zaposleni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Zaduzivanje
{
    public class GroupPerKabinetDTO
    {
        public GetOpremaDTO Oprema { get; set; }
        public int BrojKomada { get; set; }
        public GetKabinetDTO Kabinet { get; set; }
    }
}
