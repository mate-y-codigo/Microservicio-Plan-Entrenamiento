using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Interfaces.TrainingSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Obtener sesión de entrenamiento por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrainingSessionWithPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError),404)]

        public async Task<IActionResult> GetTrainingSessionById(Guid id){
            try
            {
                var result = await _trainingSessionService.GetTrainingSessionById(id);
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
