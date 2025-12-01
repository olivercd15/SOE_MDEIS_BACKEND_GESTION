using SOE_MDEIS_BACKEND_GESTION.Models;
using Xunit;

public class EmpleadoTests
{
    [Fact]
    public void Empleado_DebeTenerCargo()
    {
        var empleado = new Empleado
        {
            TipoDocumento = "CI",
            NumeroDocumento = "654321",
            Nombres = "David",
            Apellidos = "Fernandez",
            Cargo = "Administrador"
        };

        Assert.Equal("Administrador", empleado.Cargo);
        Assert.NotNull(empleado.Nombres);
        Assert.NotNull(empleado.Apellidos);
    }
}
