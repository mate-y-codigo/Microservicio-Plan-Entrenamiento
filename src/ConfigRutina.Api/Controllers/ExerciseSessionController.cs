using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExerciseSessionResponse),200)]
        [ProducesResponseType(typeof(ApiError),404)]
        [ProducesResponseType(typeof(ApiError),400)]
        [ProducesResponseType(typeof(ApiError),409)]
        public async Task<IActionResult> GetExerciseSessionsByTrainingSession(string id){

            try
            {
                return new JsonResult(_exerciseSessionService.GetExcerciseSessionById(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }

            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { message = ex.Message });
            }
            catch (ConflictException ex) {

                return Conflict(new ApiError { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }
        }

    }
    
}

