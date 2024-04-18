using DataTransferModel.Kabinet;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Zaposleni
{
    public class GetZaposleniDTO
    {
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Katedra { get; set; }
        public GetKabinetDTO Kabinet { get; set; }
    }
}
