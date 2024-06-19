using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.DTO.OrderDTO;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShippingContext _myContext;
        private readonly IGovernmentRepository _governmentRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWeightSettingRepository _weightSettingRepository;

        public OrderRepository(ShippingContext myContext,IGovernmentRepository governmentRepository , ICityRepository cityRepository,   UserManager<ApplicationUser> userManager, IWeightSettingRepository weightSettingRepository)
        {
            _myContext = myContext;
            _cityRepository = cityRepository;
            _governmentRepository = governmentRepository;
            _userManager = userManager;
            _weightSettingRepository = weightSettingRepository;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _myContext.Orders
                .Include(o => o.City).ThenInclude(c => c.Government)
                .Where(o => !o.IsDeleted)
                .ToListAsync();

            return orders.Select(order => MapToOrderDTO(order)).ToList();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _myContext.Orders
                .Include(o => o.City).ThenInclude(c => c.Government)
                .FirstOrDefaultAsync(o => o.SerialNumber == id);

            if (order == null)
                return null;

            return MapToOrderDTO(order);
        }

        public async Task<List<OrderDTO>> GetOrdersByStatusAsync(string orderStatus)
        {
            var orders = await _myContext.Orders
                .Include(o => o.City).ThenInclude(c => c.Government)
                .Where(o => o.OrderStatus == orderStatus && !o.IsDeleted)
                .ToListAsync();

            return orders.Select(order => MapToOrderDTO(order)).ToList();
        }

        public async Task<OrderDTO> AddOrderAsync(OrderDTO orderDTO, string userId)
        {
            try
            {
                var city = await _myContext.Cities.FirstOrDefaultAsync(c => c.Name == orderDTO.CityName);
                var merchantId = await _myContext.Merchants
                    .Where(m => m.UserId == userId)
                    .Select(m => m.Id)
                    .FirstOrDefaultAsync();

                if (city == null || merchantId == 0)
                    return null; // Handle error scenario

                var order = new Order
                {
                    MerchantId = merchantId,
                    CityId = city.Id,
                    IsVillage = orderDTO.IsVillage,
                    ClientEmail = orderDTO.ClientEmail,
                    ClientName = orderDTO.ClientName,
                    ClientPhoneNumber1 = orderDTO.ClientPhoneNumber1,
                    ClientPhoneNumber2 = orderDTO.ClientPhoneNumber2,
                    Notes = orderDTO.Notes,
                    OrderCost = orderDTO.OrderCost,
                    PaymentType = orderDTO.PaymentType,
                    ShippingType = orderDTO.ShippingType,
                    StreetName = orderDTO.StreetName,
                    TotalWeight = orderDTO.TotalWeight,
                    Type = orderDTO.Type,
                    GovernmentId = city.GovernmentId,
                    ShippingCost = orderDTO.ShippingCost,
                    orderProducts = orderDTO.OrderProducts.Select(op => new OrderProduct
                    {
                        ProductName = op.ProductName,
                        ProductQuantity = op.ProductQuantity,
                        Weight = op.Weight
                    }).ToList()
                };

                var branch = await _myContext.Branches.FirstOrDefaultAsync(b => b.Name == orderDTO.BranchName);
                if (branch == null)
                    return null; // Handle error scenario

                order.BranchId = branch.Id;

                _myContext.Orders.Add(order);
                await _myContext.SaveChangesAsync();

                orderDTO.Id = order.SerialNumber; // Update DTO with generated ID

                return orderDTO;
            }
            catch (Exception ex)
            {
                // Handle exception and log if needed
                throw ex;
            }
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                await _myContext.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderDeliveryAsync(int orderId, int deliveryId)
        {
            var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
            if (order != null)
            {
                order.OrderStatus = "قيد_الانتظار";
                order.DeliveryId = deliveryId;
                await _myContext.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
            if (order != null)
            {
                order.IsDeleted = true;
                await _myContext.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GenerateTableAsync(OrdersPlusDeliveriesDTO ordersPlusDeliveriesDTO)
        {
            // Implement table generation logic using ordersPlusDeliveriesDTO
            // This is an example placeholder method
            return new List<string>();
        }

        private OrderDTO MapToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                Id = order.SerialNumber,
                ClientName = order.ClientName,
                ClientEmail = order.ClientEmail,
                ClientPhoneNumber1 = order.ClientPhoneNumber1,
                ClientPhoneNumber2 = order.ClientPhoneNumber2,
                Notes = order.Notes,
                OrderCost = order.OrderCost,
                PaymentType = order.PaymentType,
                ShippingType = order.ShippingType,
                StreetName = order.StreetName,
                TotalWeight = order.TotalWeight,
                Type = order.Type,
                IsVillage = order.IsVillage,
                GovernmentName = order.City.Government.Name,
                CityName = order.City.Name,
                OrderDate = order.Date,
                OrderStatus = order.OrderStatus,
                DeliveryId = order.DeliveryId,
                BranchId = order.BranchId,
                ShippingCost = order.ShippingCost,
                TotalCost = order.TotalCost,
                MerchantId = order.MerchantId
            };
        }
    }
}
