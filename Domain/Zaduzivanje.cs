using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Zaduzivanje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZaduzivanjeId { get; set; }
        public string Email { get; set; }
        public Zaposleni Zaposleni { get; set; }
        public Guid SerijskiBroj { get; set; }
        public Oprema Oprema { get; set; }
        public DateTime DatumZaduzivanja { get; set; }
        public DateTime? DatumRazduzivanja { get; set; }
        public int BrojKomada { get; set; }
        public int KabinetId { get; set; }
        public Kabinet Kabinet { get; set; }

    }
}
