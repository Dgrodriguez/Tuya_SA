using System;
using Microsoft.EntityFrameworkCore;
using Tuya_SA.Domain;

namespace Tuya_SA.Aplication
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrder(Customer customer, decimal totalAmount)
        {
            if (customer == null || totalAmount <= 0)
            {
                return false; // Validación de negocio
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                TotalAmount = totalAmount, 
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null || order.Status == "Completado")
            {
                return false; // No cancelar órdenes completadas
            }
            else
            {
                order.Status = "Cancelado";
                return await _context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<Order>> ListOrders()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
