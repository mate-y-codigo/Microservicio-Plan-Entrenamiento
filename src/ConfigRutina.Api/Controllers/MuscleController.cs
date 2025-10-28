using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Response.CategoryExercise;
using ConfigRutina.Application.DTOs.Response.Exercise;
using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Interfaces.Muscle;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscleController : ControllerBase
    {
        private readonly IMuscleQueryService _muscleQueryService;

        public MuscleController(IMuscleQueryService muscleQueryService)
        {
            _muscleQueryService = muscleQueryService;
        }

        /// <summary>
        /// Obtener musculos por filtros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(MuscleResponse), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> Search([FromQuery] int idMusculo, [FromQuery] int idGrupoMuscular, [FromQuery] string? Musculo, [FromQuery] string? grupoMuscular)
        {
            try
            {
                return new JsonResult(await _muscleQueryService.Search(idMusculo, idGrupoMuscular, Musculo, grupoMuscular)) { StatusCode = 200 };
            }
            catch (BadRequestException ex)
            {
                return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = ex.Status };
            }
        }
    }
}
