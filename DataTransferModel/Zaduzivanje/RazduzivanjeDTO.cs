using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Zaduzivanje
{
    public class RazduzivanjeDTO
    {
        public int ZaduzivanjeId { get; set; }
        public string Email { get; set; }
        public Guid SerijskiBroj { get; set; }
        public DateTime DatumZaduzivanja { get; set; }
        public DateTime DatumRazduzivanja { get; set; }
        public int BrojKomada { get; set; }
        public int KabinetId { get; set; }
    }
}
