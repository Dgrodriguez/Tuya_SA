using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tuya_SA.Repositories;
using Tuya_SA.Domain;
using Tuya_SA.Aplication;


namespace Tuya_SA.Presentation.Controllers
    ;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly AppDbContext _context; 

    public OrderController(OrderService orderService, AppDbContext context) 
    {
        _orderService = orderService;
        _context = context;
    }

    // GET /api/orders - Obtener todas las órdenes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _orderService.ListOrders();
        return Ok(orders);
    }

    // POST /api/orders - Crear una nueva orden
    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] Order order)
    {
        if (order == null || order.TotalAmount <= 0) return BadRequest("Datos inválidos");

        // Validar que el cliente exista antes de crear la orden
        var customer = await _context.Customers.FindAsync(order.CustomerId);
        if (customer == null)
        {
            return BadRequest("Cliente no encontrado, no se puede crear la orden.");
        }
        else
        {
            // Asignar el cliente manualmente a la orden
            order.Customer = customer;
        }

        // Guardar la orden en la base de datos
        _context.Orders.Add(order);
        var success = await _context.SaveChangesAsync() > 0;

        if (!success) return StatusCode(500, "Error al crear la orden");

        return StatusCode(201, $"Orden creada exitosamente con ID {order.Id}");
    }

    // PUT /api/orders - Cancelar una orden
    [HttpPut]
    public async Task<ActionResult> CancelOrder([FromBody] int orderId)
    {
        if (orderId <= 0) return BadRequest("ID inválido");

        var success = await _orderService.CancelOrder(orderId);
        if (!success) return BadRequest("La orden no puede cancelarse");

        return Ok($"Orden con ID {orderId} ha sido cancelada exitosamente.");
    }
}