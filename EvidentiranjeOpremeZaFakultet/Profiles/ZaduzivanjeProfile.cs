using AutoMapper;
using DataTransferModel.Zaduzivanje;
using Domain;

namespace EvidentiranjeOpremeZaFakultet.Profiles
{
    public class ZaduzivanjeProfile : Profile
    {
        public ZaduzivanjeProfile()
        {
            CreateMap<Zaduzivanje, GetZaduzivanjeDTO>();
            CreateMap<CreateZaduzivanjeDTO, Zaduzivanje>();
            CreateMap<UpdateZaduzivanjeDTO, Zaduzivanje>();
            CreateMap<Zaduzivanje, GroupPerZaposleniDTO>();
            CreateMap<Zaduzivanje, GroupPerKabinetDTO>();
            CreateMap<RazduzivanjeDTO, Zaduzivanje>();
        }
    }
}
