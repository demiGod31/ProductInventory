using AutoMapper;
using ProductInventory.Application.DTOs;
using ProductInventory.Domain.Entities;

namespace ProductInventory.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CreatedBy))
            .ReverseMap()
            .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
