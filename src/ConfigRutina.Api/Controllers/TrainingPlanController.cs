using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.Exercise;
using ConfigRutina.Application.DTOs.Request.TrainingPlan;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.DTOs.Response.TrainingPlan;
using ConfigRutina.Application.Enums;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingPlanController : ControllerBase
    {
        private readonly ITrainingPlanService _trainingPlanService;
        public TrainingPlanController(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        /// <summary>
        /// Crear plan de entrenamiento
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TrainingPlanResponse), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 409)]

        public async Task<IActionResult> CreateTrainingPlan([FromBody] CreateTrainingPlanRequest request)
        {
            try
            {
                var result = await _trainingPlanService.CreateTrainingPlan(request);
                return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
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

        /// <summary>
        /// Obtener planes de entrenamiento por filtros
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="IsTemplate"></param>
        /// <param name="TrainerId"></param>
        /// <param name="Active"></param>
        /// <param name="From">desde</param>
        /// <param name="To">hasta</param>
        /// <param name="OrderBy"></param>
        [HttpGet]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]

        public async Task<IActionResult> GetTrainingPlanFilter([FromQuery] string? Name, bool? IsTemplate, Guid? TrainerId, bool? Active, DateTime? From, DateTime? To, TrainingPlanOrderBy OrderBy = TrainingPlanOrderBy.createdate_desc)
        {
            try
            {
                var result = await _trainingPlanService.GetFilterTrainingPlan(Name, IsTemplate, TrainerId, Active, From, To, OrderBy);
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
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

        /// <summary>
        /// Obtener plan de entrenamiento por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]

        public async Task<IActionResult> GetTrainingPlanById([FromRoute] Guid id)
        {
            try
            {
                var result = await _trainingPlanService.GetTrainingPlanById(id);
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

        /// <summary>
        /// Modificar el estado plan de entrenamiento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> SetStatusTrainingPlan(string? id, bool status)
        {
            try
            {
                return new JsonResult(await _trainingPlanService.SetStatus(id, status)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }
        }


        /// <summary>
        /// Eliminar un plan de entrenamiento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IsUsed"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TrainingPlanResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 409)]

        public async Task<IActionResult> DeleteTrainingPlan(string id){
            try
            {
                return new JsonResult(await _trainingPlanService.DeleteTrainingPlan(id)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
            catch (ConflictException ex) { 
                return new JsonResult(new ApiError { message = ex.Message}) { StatusCode = ex.Status };
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = "Ocurrio un error inesperado." + " " + ex.Message });
            }

        }

        /// <summary>
        /// Actualizar un plan de entrenamiento
        /// </summary>

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TrainingPlanStatusResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [ProducesResponseType(typeof(ApiError), 409)]
        public async Task<IActionResult> UpdateTrainingPlan(string id, [FromBody] UpdateTrainingPlanRequest request)
        {
            try
            {
                return new JsonResult(await _trainingPlanService.UpdateTrainingPlan(id, request));
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
