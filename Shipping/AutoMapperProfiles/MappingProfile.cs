using AutoMapper;
using Shipping.DTO;
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


            #region from WeightSetting to WeightSettingDTO
            CreateMap<WeightSetting, WeightSettingDTO>()
            .ForMember(dest => dest.standaredWeight, opt => opt.MapFrom(src => src.StandaredWeight))
            .ForMember(dest => dest.addition_Cost, opt => opt.MapFrom(src => src.Addition_Cost))
            ;
            // Mapping from WeightSettingDTO to WeightSetting (reverse direction)
            CreateMap<WeightSettingDTO, WeightSetting>()
            .ForMember(dest => dest.StandaredWeight, opt => opt.MapFrom(src => src.standaredWeight))
            .ForMember(dest => dest.Addition_Cost, opt => opt.MapFrom(src => src.addition_Cost))
            ;

            #endregion

            #region from UserRole to UserRoleDTO
            CreateMap<UserRole, UserRoleDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date)).ReverseMap()
            ;
       
            #endregion


            #region from WeightSetting to WeightSettingDTO
            CreateMap<WeightSetting, WeightSettingDTO>()
            .ForMember(dest => dest.standaredWeight, opt => opt.MapFrom(src => src.StandaredWeight))
            .ForMember(dest => dest.addition_Cost, opt => opt.MapFrom(src => src.Addition_Cost))
            ;
            // Mapping from WeightSettingDTO to WeightSetting (reverse direction)
            CreateMap<WeightSettingDTO, WeightSetting>()
            .ForMember(dest => dest.StandaredWeight, opt => opt.MapFrom(src => src.standaredWeight))
            .ForMember(dest => dest.Addition_Cost, opt => opt.MapFrom(src => src.addition_Cost))
            ;

            #endregion

            #region from UserRole to UserRoleDTO
            CreateMap<UserRole, UserRoleDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date)).ReverseMap()
            ;
       
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
                        #region From Delivery To DeliveryDTO

            CreateMap<Delivery, DeliveryDTO>()
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Government, opt => opt.MapFrom(src => src.Governement))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.DiscountType))
                .ForMember(dest => dest.CompanyPercentage, opt => opt.MapFrom(src => src.CompanyPercent))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id))
                .ReverseMap();

        }

        #endregion

    }
 }

        

