using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseSessionController : ControllerBase
    {
        private readonly IExerciseSessionService _exerciseSessionService;

        public ExerciseSessionController(IExerciseSessionService exerciseSessionService)
        {
            _exerciseSessionService = exerciseSessionService;
        }

        /// <summary>
        /// Obtener sesión de ejercicio detallado por id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExerciseSessionResponse),200)]
        [ProducesResponseType(typeof(ApiError),404)]
        public async Task<IActionResult> GetExerciseSessionById([FromRoute] Guid id){

            try
            {
                var result = await _exerciseSessionService.GetExerciseSessionById(id);
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }

            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }
        }

    }
    
}

