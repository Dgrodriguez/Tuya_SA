using System.Collections.Generic;

namespace Tuya_SA.Domain
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListarTodos();
        Task<Customer?> ObtenerXId(int id);
        Task<bool> Guardar(Customer customer);
        Task<bool> Actualizar(Customer customer);
        Task<bool> Borrar(int id);
        Task<List<Dictionary<string, object>>> ConsultaPersonalizada(string sql);
    }
}
