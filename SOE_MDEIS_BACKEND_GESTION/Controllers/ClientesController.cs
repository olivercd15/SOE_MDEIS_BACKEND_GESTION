using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOE_MDEIS_BACKEND_GESTION.DTOs;
using SOE_MDEIS_BACKEND_GESTION.Services.Interfaces;

namespace SOE_MDEIS_BACKEND_GESTION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ClienteDto>>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            var response = ApiResponse<IEnumerable<ClienteDto>>.SuccessResponse(clientes, "Listado de clientes obtenido correctamente");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<ClienteDto>>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente is null)
            {
                var error = ApiResponse<ClienteDto>.ErrorResponse(
                    "Cliente no encontrado",
                    $"No se encontró un cliente con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<ClienteDto>.SuccessResponse(cliente, "Cliente obtenido correctamente");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ClienteDto>>> Create([FromBody] ClienteCreateDto dto)
        {
            var created = await _clienteService.CreateAsync(dto);

            var response = ApiResponse<ClienteDto>.SuccessResponse(
                created,
                "Cliente creado correctamente",
                StatusCodes.Status201Created
            );

            return CreatedAtAction(nameof(GetById), new { id = created.ClienteId }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] ClienteUpdateDto dto)
        {
            var updated = await _clienteService.UpdateAsync(id, dto);
            if (!updated)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo actualizar el cliente",
                    $"No existe un cliente con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Cliente actualizado correctamente");
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var deleted = await _clienteService.DeleteAsync(id);
            if (!deleted)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo eliminar el cliente",
                    $"No existe un cliente con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Cliente eliminado correctamente");
            return Ok(response);
        }
    }
}
