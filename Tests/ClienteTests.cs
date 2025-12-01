using SOE_MDEIS_BACKEND_GESTION.Models;
using Xunit;

public class ClienteTests
{
    [Fact]
    public void Cliente_DebeTenerDocumento()
    {
        var cliente = new Cliente
        {
            TipoDocumento = "CI",
            NumeroDocumento = "123456",
            Nombres = "Oliver",
            Apellidos = "Carranza"
        };

        Assert.NotEmpty(cliente.NumeroDocumento);
        Assert.Equal("CI", cliente.TipoDocumento);
    }
}
