using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOE_MDEIS_BACKEND_GESTION.DTOs;
using SOE_MDEIS_BACKEND_GESTION.Services.Interfaces;

namespace SOE_MDEIS_BACKEND_GESTION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductoDto>>>> GetAll()
        {
            var productos = await _productoService.GetAllAsync();
            var response = ApiResponse<IEnumerable<ProductoDto>>.SuccessResponse(productos, "Listado de productos obtenido correctamente");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<ProductoDto>>> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto is null)
            {
                var error = ApiResponse<ProductoDto>.ErrorResponse(
                    "Producto no encontrado",
                    $"No se encontró un producto con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<ProductoDto>.SuccessResponse(producto, "Producto obtenido correctamente");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductoDto>>> Create([FromBody] ProductoCreateDto dto)
        {
            var created = await _productoService.CreateAsync(dto);

            var response = ApiResponse<ProductoDto>.SuccessResponse(
                created,
                "Producto creado correctamente",
                StatusCodes.Status201Created
            );

            return CreatedAtAction(nameof(GetById), new { id = created.ProductoId }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] ProductoUpdateDto dto)
        {
            var updated = await _productoService.UpdateAsync(id, dto);
            if (!updated)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo actualizar el producto",
                    $"No existe un producto con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Producto actualizado correctamente");
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var deleted = await _productoService.DeleteAsync(id);
            if (!deleted)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo eliminar el producto",
                    $"No existe un producto con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Producto eliminado correctamente");
            return Ok(response);
        }
    }
}
