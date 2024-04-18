using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Stanje { get; set; }

        public static Status ZaduzenoStanje()
        {
            return new Status { StatusId = 2 };
        }
        public static Status RazduzenoStanje()
        {
            return new Status { StatusId = 1 };
        }
        public static Status OtpisanoStanje()
        {
            return new Status { StatusId = 3 };
        }
        public static Status RashodovanoStanje()
        {
            return new Status { StatusId = 4 };
        }
        public string GetImeStanja()
        {
            return "BusinessLogic."+Stanje + "OpremaState, BusinessLogic";
        }
    }
}
