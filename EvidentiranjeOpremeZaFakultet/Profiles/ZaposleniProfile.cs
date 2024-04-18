using AutoMapper;
using DataTransferModel.Zaposleni;
using Domain;

namespace EvidentiranjeOpremeZaFakultet.Profiles
{
    public class ZaposleniProfile : Profile
    {
        public ZaposleniProfile()
        {
            CreateMap<Zaposleni, GetZaposleniDTO>();
        }
    }
}
