using AutoMapper;
using DataTransferModel.Oprema;
using Domain;

namespace EvidentiranjeOpremeZaFakultet.Profiles
{
    public class OpremaProfile : Profile
    {
        public OpremaProfile()
        {
            CreateMap<Oprema, GetOpremaDTO>();
            CreateMap<CreateOpremaDTO, Oprema>();
            CreateMap<UpdateOpremaDTO, Oprema>();
            CreateMap<RashodujOpremaDTO, Oprema>();
            CreateMap<OtpisiOpremaDTO, Oprema>();
        }
    }
}
