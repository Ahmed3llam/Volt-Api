using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.DTO.Employee_DTOs;
using Shipping.Models;

namespace Shipping.AutoMapperProfiles
{
    public class MappingEmployee : Profile
    {
        public MappingEmployee()
        {
            CreateMap<Employee, EmpDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.branchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.password, opt => opt.Ignore())
                //.AfterMap(async (src, dest, context) =>
                //{
                //    if (context.Items.TryGetValue("UserManager", out var userManagerObj) && userManagerObj is UserManager<AppUser> userManager)
                //    {
                //        var roles = await userManager.GetRolesAsync(src.User);
                //        dest.role = roles.FirstOrDefault();
                //    }
                //})
                .ReverseMap();
        }
    }
}
