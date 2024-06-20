using AutoMapper;
using Shipping.DTO.CityDTO;
using Shipping.DTO.GovernmentDTO;
using Shipping.DTO.OrderDTO;
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

            #region City 
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.shippingPrice, opt => opt.MapFrom(src => src.ShippingPrice))
                .ForMember(dest => dest.pickUpPrice, opt => opt.MapFrom(src => src.PickUpPrice))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.governmentId, opt => opt.MapFrom(src => src.GovernmentId))
                .ForMember(dest => dest.governmentName, opt => opt.MapFrom(src => src.Government.Name))

                ;
            CreateMap<CityDTO, City>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
               .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.shippingPrice))
               .ForMember(dest => dest.PickUpPrice, opt => opt.MapFrom(src => src.pickUpPrice))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
               .ForMember(dest => dest.GovernmentId, opt => opt.MapFrom(src => src.governmentId))
     

               ;
            #endregion


            #region Government 
            CreateMap<Government, GovernmentDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap()
                ;
            #endregion

            #region Order
            CreateMap<Order, OrderDTO>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SerialNumber))
         .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName))
         .ForMember(dest => dest.ClientPhoneNumber1, opt => opt.MapFrom(src => src.ClientPhoneNumber1))
         .ForMember(dest => dest.ClientPhoneNumber2, opt => opt.MapFrom(src => src.ClientPhoneNumber2))
         .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.ClientEmail))
         .ForMember(dest => dest.OrderCost, opt => opt.MapFrom(src => src.OrderCost))
         .ForMember(dest => dest.TotalWeight, opt => opt.MapFrom(src => src.TotalWeight))
         .ForMember(dest => dest.IsVillage, opt => opt.MapFrom(src => src.IsVillage))
         .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.City.Government.Name))
         .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
         .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
         .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.Date))
         .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.StreetName))
         .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
         .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
         .ForMember(dest => dest.ShippingCost, opt => opt.MapFrom(src => src.ShippingCost))
         .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
         .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.orderProducts))
         .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
         .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.DeliveryId))
         .ForMember(dest => dest.MerchantId, opt => opt.MapFrom(src => src.MerchantId))
         .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
         .ForMember(dest => dest.ShippingType, opt => opt.MapFrom(src => src.ShippingType))
         .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
         .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
         .ReverseMap()
            ;
            #endregion


        }
    }
}
