using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Tuya_SA.Models;
using Tuya_SA.Controllers;
using Tuya_SA.Aplication;

public class OrderControllerTests
{
    [Fact]
    public async Task CreateOrder_DeberiaGuardarOrden_CuandoClienteExiste()
    {
        // Configurar DbContext en memoria
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new AppDbContext(options))
        {
            var mockOrderService = new Mock<OrderService>(context); // Simular OrderService
            var controller = new OrderController(mockOrderService.Object, context); // Pasar ambos argumentos

            var customer = new Customer { Id = 1, Name = "Test", Email = "test@example.com" };
            context.Customers.Add(customer);
            await context.SaveChangesAsync(); // Guardar cliente en BD de prueba

            var order = new Order { CustomerId = 1, TotalAmount = 200 };

            var result = await controller.CreateOrder(order);

            var savedOrder = await context.Orders.FirstOrDefaultAsync(o => o.CustomerId == 1);

            Assert.NotNull(savedOrder); // Validar que la orden se guardó correctamente
            Assert.Equal(200, savedOrder.TotalAmount); // Validar que el monto se guardó correctamente
        }
    }
}