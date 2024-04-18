using AutoMapper;
using DataTransferModel.Tip_opreme;
using Domain;

namespace EvidentiranjeOpremeZaFakultet.Profiles
{
    public class TipOpremeProfile : Profile
    {
        public TipOpremeProfile()
        {
            CreateMap<TipOpreme, GetTipOpremeDTO>();
        }
    }
}
