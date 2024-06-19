using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.DTO.Employee_DTOs;
using Shipping.Models;
using Shipping.UnitOfWork;
using Swashbuckle.AspNetCore.Annotations;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork<Employee> _unit;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public EmployeesController(IUnitOfWork<Employee> unit, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region Get List Of Employees
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves a paginated list of employees.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the list of employees.", typeof(List<EmpDTO>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No employees found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<ActionResult<List<EmpDTO>>> GetEmployees(int page = 1, int pageSize = 9)
        {
            try
            {
                var employees = await _unit.Repository.GetAllAsync();
                if (!employees.Any())
                    return NotFound("لا يوجد موظفين");

                int totalCount = employees.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                employees = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                var employeesList = _mapper.Map<List<EmpDTO>>(employees);

                foreach (var empDto in employeesList)
                {
                    var employee = employees.FirstOrDefault(e => e.UserId == empDto.id);
                    var roles = await _userManager.GetRolesAsync(employee.User);
                    empDto.role = roles.FirstOrDefault();
                }

                return Ok(new { totalCount, totalPages, employeesList });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving employees: {ex.Message}");
            }
        }

        #endregion

        #region Get Specific Employee
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a specific employee by unique id.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the employee details.", typeof(EmpDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<ActionResult<EmpDTO>> GetEmployee(string id)
        {
            try
            {
                var employee = await _unit.EmployeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound($" لا يوجد موظف هذا الرقم");

                var employeeDto = _mapper.Map<EmpDTO>(employee);
                var roles = await _userManager.GetRolesAsync(employee.User);
                employeeDto.role = roles.FirstOrDefault();

                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving employee: {ex.Message}");
            }
        }
        #endregion

        #region Search Employees
        [HttpGet("search")]
        [SwaggerOperation(Summary = "Searches employees based on a query.", Description = "Searches for employees that match the given query.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmpDTO>>> SearchEmployeesAsync(string query)
        {
            try
            {
                var employees = _unit.EmployeeRepository.Search(query);
                if (employees == null || !employees.Any())
                    return NotFound("لا يوجد موظفين يتوافقوا مع بحثك");

                var employeesList = _mapper.Map<List<EmpDTO>>(employees);

                foreach (var empDto in employeesList)
                {
                    var employee = employees.FirstOrDefault(e => e.UserId == empDto.id);
                    var roles = await _userManager.GetRolesAsync(employee.User);
                    empDto.role = roles.FirstOrDefault();
                }

                return Ok(employeesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error searching employees: {ex.Message}");
            }
        }
        #endregion

        #region Update Employee
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates the data of an employee.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Employee updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Employee ID mismatch.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> PutEmployee(string id, EmpDTO employeeDto)
        {
            try
            {
                if (id != employeeDto.id)
                    return BadRequest("الرقم الخاص بالموظف غير متطابق");

                var existingEmployee = await _unit.EmployeeRepository.GetEmployeeByIdAsync(id);
                if (existingEmployee == null)
                    return NotFound($"لا يوجد موظف يحمل هذا الرقم");

                var employee = _mapper.Map<EmpDTO>(existingEmployee);
                var roles = await _userManager.GetRolesAsync(existingEmployee.User);
                employee.role = roles.FirstOrDefault();

                await _unit.EmployeeRepository.Update(employeeDto,_userManager);
                await _unit.Repository.SaveAsync();

                return Ok(new { Status = 201, Msg = "تم تعديل البيانات بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating employee: {ex.Message}");
            }
        }
        #endregion

        #region Add Employee
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new employee.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Employee created successfully.", typeof(EmpDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<ActionResult<Employee>> PostEmployee(EmpDTO employee)
        {
            try
            {
                await _unit.EmployeeRepository.Add(employee,_userManager);
                await _unit.Repository.SaveAsync();
                return Created("", new { Status = 200, employee, Msg = "تم اضافة الموظف بنجاح" });
                //return CreatedAtAction(nameof(GetEmployee), new { id = employee.id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating employee: {ex.Message}");
            }
        }
        #endregion

        #region Update Status
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Updates the status of an existing employee.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Employee status updated successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error.")]
        public async Task<IActionResult> DeleteEmployee(string id, bool status)
        {
            try
            {
                var employee = await _unit.EmployeeRepository.GetEmployeeByIdAsync(id);
                if (employee == null)
                    return NotFound($"لا يوجد موظف يحمل هذا الرقم");

                _unit.EmployeeRepository.UpdateStatus(employee, status);
                _unit.SaveChanges();

                return Ok(new { Status = 201, Msg = "تم تعديل الحالة بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting employee: {ex.Message}");
            }
        }
        #endregion
    }
}
