using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferModel.Tip_opreme
{
    public class GetTipOpremeDTO
    {
        public int TipOpremeId { get; set; }
        public string Naziv { get; set; }
        public int? NadtipId { get; set; }

    }
}
