using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.DTO.OrderDTO;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGovernmentRepository _governmentRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IbranchRepository _branchRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ShippingContext _myContext;

        public OrderController(IGovernmentRepository governmentRepository, ICityRepository cityRepository, ShippingContext myContext,
            IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IbranchRepository branchRepository,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _governmentRepository = governmentRepository;
            _cityRepository = cityRepository;
            _myContext = myContext;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _branchRepository = branchRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("Index")]
        //[Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Index()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost("ChangeDelivery")]
        //[Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> ChangeDelivery(int id, int deliveryId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _orderRepository.UpdateOrderDeliveryAsync(id, deliveryId);
            return NoContent();
        }

        [HttpPost("GetOrdersDependonStatus")]
        //[Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersDependonStatus(string? status = null)
        {
            List<OrderDTO> orders = status == null ? await _orderRepository.GetAllOrdersAsync() : await _orderRepository.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPost("ChangeStatus")]
        //[Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _orderRepository.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpPost("Add")]
        //[Authorize(Permissions.Orders.Create)]
        public async Task<IActionResult> Add(OrderDTO orderDTO)
        {
            if (orderDTO.OrderProducts == null || orderDTO.OrderProducts.Count == 0)
            {
                ModelState.AddModelError("", "يجب عليك اضافه منتاجات");
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            var addedOrder = await _orderRepository.AddOrderAsync(orderDTO, user.Id);
            if (addedOrder == null)
            {
                return BadRequest("Failed to add order");
            }

            return CreatedAtAction(nameof(Index), new { id = addedOrder.Id }, addedOrder);
        }

        [HttpGet("GetCitiesByGovernment")]
        //[Authorize(Permissions.Orders.Create)]
        public IActionResult GetCitiesByGovernment(int governmentId)
        {
            var cities = _cityRepository.GetAllByGovernmentId(governmentId);
            return Ok(cities);
        }

        [HttpGet("GetBranchesByGovernment")]
        //[Authorize(Permissions.Orders.Create)]
        public IActionResult GetBranchesByGovernment(string government)
        {
            var branches = _branchRepository.GetBranchesByGovernmentName(government);
            return Ok(branches);
        }


        [HttpGet("SearchByClientName")]
        //[Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> SearchByClientName(string query)
        {
            var orders = string.IsNullOrWhiteSpace(query) ? await _orderRepository.GetAllOrdersAsync() :
                (await _orderRepository.GetAllOrdersAsync()).Where(i => i.ClientName.ToUpper().Contains(query.ToUpper())).ToList();

            return Ok(orders);
        }

        [HttpGet("SearchByDeliveryName")]
        //[Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> SearchByDeliveryName(string query)
        {
            var orders = new List<OrderDTO>();
            var deliveries = await _deliveryRepository.GetAll(null);

            if (string.IsNullOrWhiteSpace(query))
            {
                orders = await _orderRepository.GetAllOrdersAsync();
            }
            else
            {
                var filteredDeliveries = deliveries.Where(d => d.DeliverName.ToUpper().Contains(query.ToUpper())).ToList();
                foreach (var delivery in filteredDeliveries)
                {
                    var filteredOrders = (await _orderRepository.GetAllOrdersAsync()).Where(o => o.DeliveryId == delivery.OrignalIdOnlyInDeliveryTable).ToList();
                    orders.AddRange(filteredOrders);
                }
            }
            return Ok(orders);
        }

        [HttpGet("Edit")]
        //[Authorize(Permissions.Orders.Edit)]
        public async Task<ActionResult<OrderDTO>> Edit(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("Edit")]
        //[Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> Edit(int id, OrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound();

                // Update order details here
                // Note: The code to update the order details is missing in your provided code.

                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Delete")]
        //[Authorize(Permissions.Orders.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("OrderCount")]
        [Authorize]
        public async Task<IActionResult> OrderCount()
        {
            var roleName = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepository.GetAllOrdersAsync();

            if (roleName == "Admin" || roleName == "الموظفين")
                return Ok(orders);
            else if (roleName == "التجار")
            {
                var merchantId = _myContext.Merchants.Where(m => m.UserId == userId).Select(m => m.Id).FirstOrDefault();
                var filteredOrders = orders.Where(o => o.MerchantId == merchantId).ToList();
                return Ok(filteredOrders);
            }
            else if (roleName == "المناديب")
            {
                var deliveryId = _myContext.Deliveries.Where(d => d.UserId == userId).Select(d => d.Id).FirstOrDefault();
                var filteredOrders = orders.Where(o => o.DeliveryId == deliveryId).ToList();
                return Ok(filteredOrders);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("IndexAfterFilter")]
        //[Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> IndexAfterFilter(string query)
        {
            var roleName = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepository.GetAllOrdersAsync();

            var filteredOrders = orders.Where(o => o.OrderStatus == query).ToList();

            if (roleName == "Admin" || roleName == "الموظفين")
                return Ok(filteredOrders);
            else if (roleName == "التجار")
            {
                var merchantId = _myContext.Merchants.Where(m => m.UserId == userId).Select(m => m.Id).FirstOrDefault();
                return Ok(filteredOrders.Where(o => o.MerchantId == merchantId).ToList());
            }
            else if (roleName == "المناديب")
            {
                var deliveryId = _myContext.Deliveries.Where(d => d.UserId == userId).Select(d => d.Id).FirstOrDefault();
                return Ok(filteredOrders.Where(o => o.DeliveryId == deliveryId).ToList());
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("OrderReceipt")]
        public async Task<ActionResult<OrderDTO>> OrderReceipt(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
