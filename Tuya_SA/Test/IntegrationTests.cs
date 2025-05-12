using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Tuya_SA;
using Tuya_SA.Domain;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomers_DeberiaRetornarListaDeClientes()
    {
        var response = await _client.GetAsync("/api/customers");
        response.EnsureSuccessStatusCode(); // Verificar que la API responde correctamente

        var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
        Assert.NotNull(customers); // Validar que se devuelve una lista
        Assert.NotEmpty(customers); // Asegurarse de que hay clientes en la respuesta
    }
}