using Microsoft.AspNetCore.Identity;
using Shipping.DTO.Employee_DTOs;
using Shipping.Models;

namespace Shipping.Repository.Employee_Repository
{
    public interface IEmployeeRepository
    {
        Task Add(EmpDTO newEmp, UserManager<AppUser> _userManager);
        Task<Employee> GetEmployeeByIdAsync(string id);
        List<Employee> Search(string query);
        Task Update(EmpDTO NewData, UserManager<AppUser> _userManager);
        void UpdateStatus(Employee employee, bool status);
    }
}
