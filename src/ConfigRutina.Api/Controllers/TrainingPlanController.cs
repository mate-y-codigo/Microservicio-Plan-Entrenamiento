using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.Services.TrainingPlan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingPlanController : ControllerBase
    {
        private readonly TrainingPlanService _trainingPlanService;
        public TrainingPlanController(TrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 400)]

        public async Task<IActionResult> GetTrainingPlanById(string id)
        {
            try
            {
                return new JsonResult(_trainingPlanService.GetTrainingPlanById(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TrainingPlanStatusResponse), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 409)]
        public async Task<IActionResult> ChangeTrainingPlanStatus(string id, [FromBody] UpdateTrainingPlanStatusRequest request) {
            try
            {
                return new JsonResult(_trainingPlanService.ChangeStateTrainingPlan(id, request));
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

        [HttpGet]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]

        public async Task<IActionResult> GetTrainingPlanFilter([FromQuery] string? Name, bool? Plantilla, string? IdEntrenador, bool? Active, DateTime? CreateDate, DateTime? UpdateDate)
        {
            try
            {
                return new JsonResult(_trainingPlanService.GetFilterTrainingPlan(Name, Plantilla, IdEntrenador, Active, CreateDate,UpdateDate));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(TrainingPlanResponse), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]


        //preguntar si se espera del body las sesiones o se asignan en service
        public async Task<IActionResult> CreateTrainingPlan([FromBody] CreateTrainingPlanRequest request)
        {
            try
            {
                return new JsonResult(_trainingPlanService.CreateTrainingPLan(request));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiError { message = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }

        }

    }
}
