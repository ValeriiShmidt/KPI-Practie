using System;
using AutoMapper;

namespace KpiPractice.Orchestrators.Shops
{
    public class ShopOrchProfile : Profile
    {
        public ShopOrchProfile()
        {
            CreateMap<Shop, Core.Shops.Shop>()
                .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address))
                .ReverseMap();;
        }

    }
    // public class OrchBankProfile : Profile
    // {
    //     public OrchBankProfile()
    //     {
    //         CreateMap<Bank, Core.Banks.Bank>()
    //             .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
    //             .ForMember(dest => dest.Head, memberOptions: opt => opt.MapFrom(src => src.Head))
    //             .ForMember(dest => dest.Count, memberOptions: opt => opt.MapFrom(src => src.Count))
    //             .ForMember(dest => dest.Location, memberOptions: opt => opt.MapFrom(src => src.Location))
    //             .ReverseMap();;
    //     }
    //
    // }
}
