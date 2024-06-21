using AutoMapper;
using Shipping.DTO;
using Shipping.DTO.BranchDTOs;
using Shipping.DTO.DeliveryDTOs;
using Shipping.DTO.Employee_DTOs;
using Shipping.DTO.MerchantDTOs;
using Shipping.Models;
using AutoMapper;
using CloudinaryDotNet.Core;
using Shipping.DTO.BranchDTOs;
using Shipping.DTO.MerchantDTOs;
using Shipping.DTO.MerchantDTOs;
using Shipping.Models;
using Shipping.DTO.CityDTO;
using Shipping.DTO.GovernmentDTO;
using Shipping.DTO.OrderDTO;
using Shipping.DTO.AccountDTOs;

namespace Shipping.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region user
            CreateMap<AppUser, UserDTO>()
                .ForMember(dest => dest.userID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));
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

            #region from WeightSetting to WeightSettingDTO
            CreateMap<WeightSetting, WeightSettingDTO>()
            .ForMember(dest => dest.standaredWeight, opt => opt.MapFrom(src => src.StandaredWeight))
            .ForMember(dest => dest.addition_Cost, opt => opt.MapFrom(src => src.Addition_Cost)).ReverseMap()
            ;

            #endregion

            #region from UserRole to UserRoleDTO
            CreateMap<UserRole, UserRoleDTO>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date)).ReverseMap()
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
                    .ReverseMap();
 
            CreateMap<OrderProduct, OrderProductDTO>().ReverseMap();


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

        

        #endregion

            #region Map Merchant - MerchantDTO
        CreateMap<Merchant, MerchantDTO>()
                     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                     .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                     .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.PasswordHash))
                     .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                     .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                     .ForMember(dest => dest.PickUpSpecialCost, opt => opt.MapFrom(src => src.PickUpSpecialCost))
                     .ForMember(dest => dest.RefusedOrderPercent, opt => opt.MapFrom(src => src.RefusedOrderPercent))
                     .ReverseMap();
            #endregion

            #region Branch Mapper
            CreateMap<Branch, BranchDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Government.Name)) // تعيين اسم المحافظة
                    ;
            CreateMap<BranchDTO, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            #endregion


        }
    }
}
