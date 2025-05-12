using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tuya_SA.Services;
using Tuya_SA.Domain;



namespace Tuya_SA.Presentation.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    // GET /api/customers - Obtener todos los clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> ListarTodos()
    {
        var customers = await _customerRepository.ListarTodos();
        return Ok(customers);
    }

    // GET /api/customers/{id} - Obtener un cliente por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> ObtenerXId(int id)
    {
        var customer = await _customerRepository.ObtenerXId(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    // POST /api/customers - Crear un nuevo cliente
    [HttpPost]
    public async Task<ActionResult> Guardar([FromBody] Customer customer)
    {
        if (customer == null)
        {
            return BadRequest();
        }
        else
        {
            var success = await _customerRepository.Guardar(customer);

            if (!success)
            {
                return StatusCode(500, "Error al guardar el cliente");
            }
            else
            {
                return CreatedAtAction(nameof(ObtenerXId), new { id = customer.Id }, customer);
            }
        }      
    }

    // PUT /api/customers/{id} - Actualizar un cliente existente
    [HttpPut("{id}")]
    public async Task<ActionResult> Actualizar(int id, [FromBody] Customer customer)
    {
        if (customer == null) return BadRequest("El objeto cliente es inválido.");
        if (id != customer.Id) return BadRequest("El ID("+id+") proporcionado no coincide con el ID del cliente.");

        var success = await _customerRepository.Actualizar(customer);
        if (!success) return Problem("Error al actualizar el cliente");

        return Ok("Actualización exitosa!");
    }

    // DELETE /api/customers/{id} - Borrar un cliente
    [HttpDelete("{id}")]
    public async Task<ActionResult> Borrar(int id)
    {
        var success = await _customerRepository.Borrar(id);
        if (!success) { 
            return NotFound(); 
        }
        else
        {
            return Ok("Cliente Eliminado Exitosamente!");
        }
    }
}