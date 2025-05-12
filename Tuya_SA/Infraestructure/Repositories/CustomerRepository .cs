using Microsoft.EntityFrameworkCore;
using Tuya_SA.Domain;

namespace Tuya_SA.Infraestructure.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> ListarTodos()
        {
            var clientes = await _context.Customers.ToListAsync();

            if (clientes == null || !clientes.Any())
            {
                Console.WriteLine("No hay datos de Customers que mostrar.");
                return new List<Customer>(); 
            }

            return clientes;
        }

        public async Task<Customer?> ObtenerXId(int id)
        {
            if (id != 0)
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Guardar(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _context.Customers.Add(customer);
                    return await _context.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar en BD: " + ex.Message);

                // 🔹 Mostrar detalles más profundos del error
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                return false;


            }
        }

        public async Task<bool> Actualizar(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _context.Customers.Update(customer);
                    return await _context.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al Actualizar: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Borrar(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }
                else
                {
                    _context.Customers.Remove(customer);
                    return await _context.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al Borrar: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Dictionary<string, object>>> ConsultaPersonalizada(string sql)
        {
            var resultado = new List<Dictionary<string, object>>();

            try
            {
                // Validar que la consulta sea un SELECT
                sql = sql.Trim().ToUpper();
                if (!sql.StartsWith("SELECT"))
                {
                    Console.WriteLine("Consulta no permitida: Solo se admiten SELECT.");
                    return resultado; // Retorna lista vacía
                }

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }

                                resultado.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar SQL personalizado: {ex.Message}");
            }

            return resultado;
        }
    }
}
