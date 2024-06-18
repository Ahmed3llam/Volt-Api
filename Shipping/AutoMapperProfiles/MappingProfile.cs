using AutoMapper;
using Shipping.Models;

namespace Shipping.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region from Product to ProductDTO
            //CreateMap<Product, ProductDTO>()
            //.ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
            //.ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(dest => dest.img, opt => opt.MapFrom(src => src.Img))
            //.ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
            //.ForMember(dest => dest.stock, opt => opt.MapFrom(src => src.Stock))
            //.ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.category_name, opt => opt.MapFrom(src => src.Category.Title))
            //;
            //// Mapping from ProductDTO to Product (reverse direction)
            //CreateMap<ProductDTO, Product>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
            //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
            //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
            //.ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.img))
            //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
            //.ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.stock))
            //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.categoryId))
            //;

            #endregion



        }
    }
}
