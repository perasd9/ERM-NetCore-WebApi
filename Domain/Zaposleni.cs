using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Zaposleni
    {
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Katedra { get; set; }
        public Kabinet Kabinet { get; set; }
        public int KabinetId { get; set; }
        public List<Zaduzivanje> ListaZaduzivanja { get; set; }
    }
}
