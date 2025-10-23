using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.Interfaces.Excercise;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExcerciseCommandService _exerciseCommandService;
        private readonly IExcerciseQueryService _excerciseQueryService;

        public ExerciseController(IExcerciseQueryService excerciseQueryService, IExcerciseCommandService exerciseCommandService)
        {
            _excerciseQueryService = excerciseQueryService;
            _exerciseCommandService = exerciseCommandService;
        }

        /// <summary>
        /// Crear nuevo ejercicio.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ExerciseResponse), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 409)]
        public async Task<IActionResult> Create(ExerciseCreateRequest request)
        {
            try
            {
                return new JsonResult(await _exerciseCommandService.Create(request)) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (ConflictException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }

        /// <summary>
        /// Borrar nuevo ejercicio.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExerciseResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return new JsonResult(await _exerciseCommandService.Delete(id)) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }

        /// <summary>
        /// Actualizar ejercicio existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ExerciseResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 409)]
        public async Task<IActionResult> Update(string id, [FromBody] ExerciseUpdateRequest updateRequest)
        {
            try
            {
                return new JsonResult(await _exerciseCommandService.Update(id, updateRequest)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (ConflictException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }

        /// <summary>
        /// Obtener categorias de ejercicios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ExerciseResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? mainMuscle, [FromQuery] string? muscleGroup, [FromQuery] int category, [FromQuery] bool onlyActive = true)
        {
            try
            {
                return new JsonResult(await _excerciseQueryService.Search(name!, mainMuscle!, muscleGroup!, category, onlyActive)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }

        /// <summary>
        /// Obtener categorias de ejercicios.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExerciseResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> SearchById(string id)
        {
            try
            {
                return new JsonResult(await _excerciseQueryService.SearchById(id)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }
    }
}
