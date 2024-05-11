using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Kabinet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KabinetId { get; set; }
        public string Naziv { get; set; }
        public List<Zaposleni> ListaZaposlenih { get; set; }
        [JsonIgnore]
        public List<Zaduzivanje> ListaZaduzivanja { get; set; }
    }
}
