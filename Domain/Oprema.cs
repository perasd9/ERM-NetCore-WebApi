using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Oprema
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SerijskiBroj { get; set; }
        public string Naziv { get; set; }
        public string InventarskiBroj { get; set; }
        public int Kolicina { get; set; }
        public int TipOpremeId { get; set; }
        public TipOpreme TipOpreme { get; set; }
        public int StanjeId { get; set; } = 1;
        public Status Stanje { get; set; }
        public List<Zaduzivanje> ListaZaduzivanja { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
