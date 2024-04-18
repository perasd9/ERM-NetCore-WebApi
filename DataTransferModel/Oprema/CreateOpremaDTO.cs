using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Oprema
{
    public class CreateOpremaDTO
    {
        public string Naziv { get; set; }
        public string InventarskiBroj { get; set; }
        public int Kolicina { get; set; }
        public int TipOpremeId { get; set; }
    }
}
