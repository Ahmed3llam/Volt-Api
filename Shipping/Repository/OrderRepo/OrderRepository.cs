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

        public OrderRepository(ShippingContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _myContext.Orders
                    .Include(o => o.City).ThenInclude(c => c.Government)
                    .Where(o => !o.IsDeleted)
                    .ToListAsync();

                return orders.Select(order => MapToOrderDTO(order)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في جلب جميع الطلبات.", ex);
            }
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _myContext.Orders
                    .Include(o => o.City).ThenInclude(c => c.Government)
                    .FirstOrDefaultAsync(o => o.SerialNumber == id);

                if (order == null)
                    throw new Exception("الطلب غير موجود.");

                return MapToOrderDTO(order);
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في جلب الطلب حسب الرقم.", ex);
            }
        }

        public async Task<List<OrderDTO>> GetOrdersByStatusAsync(string orderStatus)
        {
            try
            {
                var orders = await _myContext.Orders
                    .Include(o => o.City).ThenInclude(c => c.Government)
                    .Where(o => o.OrderStatus == orderStatus && !o.IsDeleted)
                    .ToListAsync();

                return orders.Select(order => MapToOrderDTO(order)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في جلب الطلبات حسب الحالة.", ex);
            }
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

                if (city == null)
                    throw new Exception("المدينة غير موجودة.");
                if (merchantId == 0)
                    throw new Exception("التاجر غير موجود.");

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
                    throw new Exception("الفرع غير موجود.");

                order.BranchId = branch.Id;

                _myContext.Orders.Add(order);
                await _myContext.SaveChangesAsync();

                orderDTO.Id = order.SerialNumber; // Update DTO with generated ID

                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في إضافة الطلب.", ex);
            }
        }

        public async Task<OrderDTO> EditOrderAsync(int id, OrderDTO orderDTO)
        {
            try
            {
                var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == id);
                if (order == null)
                    throw new Exception("الطلب غير موجود.");

                var city = await _myContext.Cities.FirstOrDefaultAsync(c => c.Name == orderDTO.CityName);
                if (city == null)
                    throw new Exception("المدينة غير موجودة.");

                order.CityId = city.Id;
                order.IsVillage = orderDTO.IsVillage;
                order.ClientEmail = orderDTO.ClientEmail;
                order.ClientName = orderDTO.ClientName;
                order.ClientPhoneNumber1 = orderDTO.ClientPhoneNumber1;
                order.ClientPhoneNumber2 = orderDTO.ClientPhoneNumber2;
                order.Notes = orderDTO.Notes;
                order.OrderCost = orderDTO.OrderCost;
                order.PaymentType = orderDTO.PaymentType;
                order.ShippingType = orderDTO.ShippingType;
                order.StreetName = orderDTO.StreetName;
                order.TotalWeight = orderDTO.TotalWeight;
                order.Type = orderDTO.Type;
                order.GovernmentId = city.GovernmentId;
                order.ShippingCost = orderDTO.ShippingCost;

                var branch = await _myContext.Branches.FirstOrDefaultAsync(b => b.Name == orderDTO.BranchName);
                if (branch == null)
                    throw new Exception("الفرع غير موجود.");

                order.BranchId = branch.Id;

                order.orderProducts.Clear();
                order.orderProducts = orderDTO.OrderProducts.Select(op => new OrderProduct
                {
                    ProductName = op.ProductName,
                    ProductQuantity = op.ProductQuantity,
                    Weight = op.Weight
                }).ToList();

                await _myContext.SaveChangesAsync();

                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في تعديل الطلب.", ex);
            }
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            try
            {
                var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
                if (order == null)
                    throw new Exception("الطلب غير موجود.");

                order.OrderStatus = status;
                await _myContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في تحديث حالة الطلب.", ex);
            }
        }

        public async Task UpdateOrderDeliveryAsync(int orderId, int deliveryId)
        {
            try
            {
                var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
                if (order == null)
                    throw new Exception("الطلب غير موجود.");

                order.OrderStatus = "قيد_الانتظار";
                order.DeliveryId = deliveryId;
                await _myContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في تحديث تسليم الطلب.", ex);
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _myContext.Orders.FirstOrDefaultAsync(o => o.SerialNumber == orderId);
                if (order == null)
                    throw new Exception("الطلب غير موجود.");

                order.IsDeleted = true;
                await _myContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في حذف الطلب.", ex);
            }
        }

        public async Task<List<string>> GenerateTableAsync(OrdersPlusDeliveriesDTO ordersPlusDeliveriesDTO)
        {
            try
            {
                // Implement table generation logic using ordersPlusDeliveriesDTO
                var table = new List<string>
                {
                    "Header1, Header2, Header3", // Example headers
                    "Data1, Data2, Data3", // Example data
                    "Data4, Data5, Data6" // More example data
                };
                return await Task.FromResult(table);
            }
            catch (Exception ex)
            {
                throw new Exception("خطأ في إنشاء الجدول.", ex);
            }
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
