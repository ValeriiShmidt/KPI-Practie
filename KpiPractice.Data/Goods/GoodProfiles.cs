using AutoMapper;

namespace KpiPractice.Data.Goods
{
    public class GoodDaoProfile : Profile
    {
        public GoodDaoProfile()
        {
            CreateMap<Good, Core.Goods.Goods>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum))
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.ShopId))
                .ReverseMap();
        }
    }
}