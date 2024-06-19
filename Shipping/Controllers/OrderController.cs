using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.DTO.OrderDTO;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;
using Shipping.UnitOfWork;
using Swashbuckle.AspNetCore.Annotations;
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
        private readonly IUnitOfWork<Order> _unit;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly ShippingContext _myContext;


        public OrderController(ShippingContext myContext,
            IUnitOfWork<Order> unit,
            UserManager<AppUser> userManager,
            RoleManager<UserRole> roleManager
)
        {
            _unit = unit;
            _myContext = myContext;
            _userManager = userManager;

        }

        #region Get Orders

        [HttpGet("Index")]
        [SwaggerOperation(Summary = "Retrieves all orders.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of orders.")]
     // [Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Index()
        {
            var orders = await _unit.OrderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost("GetOrdersDependonStatus")]
        [SwaggerOperation(Summary = "Retrieves orders based on status.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of orders based on status.")]
     // [Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersDependonStatus(string? status = null)
        {
            List<OrderDTO> orders = status == null ? await _unit.OrderRepository.GetAllOrdersAsync() : await _unit.OrderRepository.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpGet("SearchByClientName")]
        [SwaggerOperation(Summary = "Searches orders by client name.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of orders that match the client name.")]
    //  [Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> SearchByClientName(string query)
        {
            var orders = string.IsNullOrWhiteSpace(query) ? await _unit.OrderRepository.GetAllOrdersAsync() :
                (await _unit.OrderRepository.GetAllOrdersAsync()).Where(i => i.ClientName.ToUpper().Contains(query.ToUpper())).ToList();

            return Ok(orders);
        }

      //  [HttpGet("SearchByDeliveryName")]
      //  [SwaggerOperation(Summary = "Searches orders by delivery name.")]
      //  [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of orders that match the delivery name.")]
      ////[Authorize(Permissions.Orders.View)]
      //  public async Task<ActionResult<IEnumerable<OrderDTO>>> SearchByDeliveryName(string query)
      //  {
      //      var orders = new List<OrderDTO>();
      //      var deliveries = await _deliveryRepository.GetAll(null);

      //      if (string.IsNullOrWhiteSpace(query))
      //      {
      //          orders = await _unit.OrderRepository.GetAllOrdersAsync();
      //      }
      //      else
      //      {
      //          var filteredDeliveries = deliveries.Where(d => d.DeliverName.ToUpper().Contains(query.ToUpper())).ToList();
      //          foreach (var delivery in filteredDeliveries)
      //          {
      //              var filteredOrders = (await _unit.OrderRepository.GetAllOrdersAsync()).Where(o => o.DeliveryId == delivery.OrignalIdOnlyInDeliveryTable).ToList();
      //              orders.AddRange(filteredOrders);
      //          }
      //      }
      //      return Ok(orders);
      //  }

        [HttpGet("Edit")]
        [SwaggerOperation(Summary = "Gets the details of a specific order for editing.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the details of the order.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
      //  [Authorize(Permissions.Orders.Edit)]
        public async Task<ActionResult<OrderDTO>> Edit(int id)
        {
            var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("OrderReceipt")]
        [SwaggerOperation(Summary = "Retrieves the receipt of a specific order.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the receipt of the order.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
     //   [Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<OrderDTO>> OrderReceipt(int id)
        {
            var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        #endregion

        #region Change Order Details

        [HttpPost("ChangeDelivery")]
        [SwaggerOperation(Summary = "Changes the delivery of a specific order.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Delivery changed successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
      //  [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> ChangeDelivery(int id, int deliveryId)
        {
            var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _unit.OrderRepository.UpdateOrderDeliveryAsync(id, deliveryId);
            return NoContent();
        }

        [HttpPost("ChangeStatus")]
        [SwaggerOperation(Summary = "Changes the status of a specific order.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Status changed successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
     //   [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _unit.OrderRepository.UpdateOrderStatusAsync(id, status);
            return NoContent();
        }

        [HttpPost("Edit")]
        [SwaggerOperation(Summary = "Edits a specific order.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Order edited successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid order data.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
      //  [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> Edit(int id, OrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound();

                // Update order details here
                // Note: The code to update the order details is missing in your provided code.

                return NoContent();
            }
            return BadRequest(ModelState);
        }

        #endregion

        #region Add and Delete Orders

        [HttpPost("Add")]
        [SwaggerOperation(Summary = "Adds a new order.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Order created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to add order.")]
      //  [Authorize(Permissions.Orders.Create)]
        public async Task<IActionResult> Add(OrderDTO orderDTO)
        {
            if (orderDTO.OrderProducts == null || orderDTO.OrderProducts.Count == 0)
            {
                ModelState.AddModelError("", "يجب عليك اضافه منتاجات");
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            var addedOrder = await _unit.OrderRepository.AddOrderAsync(orderDTO, user.Id);
            if (addedOrder == null)
            {
                return BadRequest("Failed to add order");
            }

            return CreatedAtAction(nameof(Index), new { id = addedOrder.Id }, addedOrder);
        }

        [HttpDelete("Delete")]
        [SwaggerOperation(Summary = "Deletes a specific order.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Order deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found.")]
    //    [Authorize(Permissions.Orders.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unit.OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            await _unit.OrderRepository.DeleteOrderAsync(id);
            return NoContent();
        }

        #endregion

        #region Additional Endpoints

        [HttpGet("GetCitiesByGovernment")]
        [SwaggerOperation(Summary = "Retrieves cities based on government ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of cities.")]
     //   [Authorize(Permissions.Orders.Create)]
        public IActionResult GetCitiesByGovernment(int governmentId)
        {
            var cities = _unit.CityRepository.GetAllByGovernmentId(governmentId);
            return Ok(cities);
        }

     //   [HttpGet("GetBranchesByGovernment")]
     //   [SwaggerOperation(Summary = "Retrieves branches based on government name.")]
     //   [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of branches.")]
     ////   [Authorize(Permissions.Orders.Create)]
     //   public IActionResult GetBranchesByGovernment(string government)
     //   {
     //       var branches = _branchRepository.GetBranchesByGovernmentName(government);
     //       return Ok(branches);
     //   }

        [HttpGet("OrderCount")]
        [SwaggerOperation(Summary = "Retrieves the count of orders based on user role.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of orders.")]
        [Authorize]
        public async Task<IActionResult> OrderCount()
        {
            var roleName = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _unit.OrderRepository.GetAllOrdersAsync();

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
        [SwaggerOperation(Summary = "Retrieves orders based on status and user role.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of filtered orders.")]
     //   [Authorize(Permissions.Orders.View)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> IndexAfterFilter(string query)
        {
            var roleName = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _unit.OrderRepository.GetAllOrdersAsync();

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

        #endregion
    }
}
