using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.TrainingSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSessionController : ControllerBase
    {
        private readonly ITrainingSessionService _trainingSessionService;

        public TrainingSessionController(ITrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TrainingSessionResponse), 200)]
        [ProducesResponseType(typeof(ApiError),404)]
        [ProducesResponseType(typeof(ApiError), 409)]
        [ProducesResponseType(typeof(ApiError), 400)]

        public async Task<IActionResult> GetTrainingSessionByTrainingPlan(string id){
            try
            {
                return new JsonResult(_trainingSessionService.GetTrainingSessionById(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }

            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { message = ex.Message });
            }
            catch (ConflictException ex)
            {

                return Conflict(new ApiError { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }
        }

    }
}

