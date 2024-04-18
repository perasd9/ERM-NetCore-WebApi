using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TipOpreme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipOpremeId { get; set; }
        public string Naziv { get; set; }
        [ForeignKey("Nadtip")]
        public int? NadtipId { get; set; }
        public TipOpreme? Nadtip { get; set; }
        public List<Oprema> ListaOpreme { get; set; }
        public List<TipOpreme> ListaPodTipova { get; set; }
    }
}
