using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOE_MDEIS_BACKEND_GESTION.DTOs;
using SOE_MDEIS_BACKEND_GESTION.Services.Interfaces;

namespace SOE_MDEIS_BACKEND_GESTION.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<EmpleadoDto>>>> GetAll()
        {
            var empleados = await _empleadoService.GetAllAsync();
            var response = ApiResponse<IEnumerable<EmpleadoDto>>.SuccessResponse(empleados, "Listado de empleados obtenido correctamente");
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<EmpleadoDto>>> GetById(int id)
        {
            var empleado = await _empleadoService.GetByIdAsync(id);
            if (empleado is null)
            {
                var error = ApiResponse<EmpleadoDto>.ErrorResponse(
                    "Empleado no encontrado",
                    $"No se encontró un empleado con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<EmpleadoDto>.SuccessResponse(empleado, "Empleado obtenido correctamente");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmpleadoDto>>> Create([FromBody] EmpleadoCreateDto dto)
        {
            var created = await _empleadoService.CreateAsync(dto);

            var response = ApiResponse<EmpleadoDto>.SuccessResponse(
                created,
                "Empleado creado correctamente",
                StatusCodes.Status201Created
            );

            return CreatedAtAction(nameof(GetById), new { id = created.EmpleadoId }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Update(int id, [FromBody] EmpleadoUpdateDto dto)
        {
            var updated = await _empleadoService.UpdateAsync(id, dto);
            if (!updated)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo actualizar el empleado",
                    $"No existe un empleado con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Empleado actualizado correctamente");
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var deleted = await _empleadoService.DeleteAsync(id);
            if (!deleted)
            {
                var error = ApiResponse<object>.ErrorResponse(
                    "No se pudo eliminar el empleado",
                    $"No existe un empleado con el id {id}",
                    StatusCodes.Status404NotFound
                );
                return NotFound(error);
            }

            var response = ApiResponse<object>.SuccessResponse(null, "Empleado eliminado correctamente");
            return Ok(response);
        }
    }
}
