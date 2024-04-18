using DataTransferModel.Tip_opreme;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Oprema
{
    public class GetOpremaDTO
    {
        public Guid SerijskiBroj { get; set; }
        public string Naziv { get; set; }
        public string InventarskiBroj { get; set; }
        public int Kolicina { get; set; }
        public Status Stanje { get; set; }
        public GetTipOpremeDTO TipOpreme { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
