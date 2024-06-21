using AutoMapper;
using Shipping.DTO.OrderDTO;
using Shipping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipping.Repository.OrderRepo
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task<List<OrderDTO>> GetOrdersByStatusAsync(string orderStatus);
        Task<OrderDTO> AddOrderAsync(OrderDTO orderDTO, string userId);
        Task<OrderDTO> EditOrderAsync(int id, OrderDTO orderDTO);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task UpdateOrderDeliveryAsync(int orderId, int deliveryId);
        Task DeleteOrderAsync(int orderId);
        Task<List<string>> GenerateTableAsync(OrdersPlusDeliveriesDTO ordersPlusDeliveriesDTO);
    }
}
