using AutoMapper;
using Shipping.DTO.Employee_DTOs;
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

            #region map Employee - EmpDTO
            CreateMap<Employee, EmpDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.password, opt => opt.Ignore())
                .ReverseMap();
            #endregion

        }
    }
}
