using AutoMapper;
using DataTransferModel.Kabinet;
using DataTransferModel.Zaduzivanje;
using Domain;

namespace EvidentiranjeOpremeZaFakultet.Profiles
{
    public class KabinetProfile : Profile
    {
        public KabinetProfile()
        {
            CreateMap<Kabinet, GetKabinetDTO>();
        }
    }
}
