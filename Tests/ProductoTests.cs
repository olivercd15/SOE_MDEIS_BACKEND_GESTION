using SOE_MDEIS_BACKEND_GESTION.Models;
using Xunit;

public class ProductoTests
{
    [Fact]
    public void Producto_DebeSerValidoCuandoTieneCamposCorrectos()
    {
        var p = new Producto
        {
            Codigo = "P001",
            Nombre = "Laptop",
            PrecioUnitario = 1500,
            StockActual = 10
        };

        Assert.NotNull(p);
        Assert.Equal("P001", p.Codigo);
        Assert.Equal("Laptop", p.Nombre);
        Assert.True(p.PrecioUnitario > 0);
    }
}
