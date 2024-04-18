using DataTransferModel.Kabinet;
using DataTransferModel.Oprema;
using DataTransferModel.Zaposleni;

namespace DataTransferModel.Zaduzivanje
{
    public class GetZaduzivanjeDTO
    {
        public int ZaduzivanjeId { get; set; }
        public GetZaposleniDTO Zaposleni { get; set; }
        public GetOpremaDTO Oprema { get; set; }
        public DateTime DatumZaduzivanja { get; set; }
        public DateTime? DatumRazduzivanja { get; set; }
        public int BrojKomada { get; set; }
        public GetKabinetDTO Kabinet { get; set; }

    }
}
