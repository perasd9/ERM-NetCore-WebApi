using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Zaduzivanje
{
    public class UpdateZaduzivanjeDTO
    {
        public int ZaduzivanjeId { get; set; }
        public string Email { get; set; }
        public Guid SerijskiBroj { get; set; }
        public int BrojKomada { get; set; }
    }
}
