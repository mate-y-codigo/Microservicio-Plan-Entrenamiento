using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.DTOs.Response.MuscleGroup;
using ConfigRutina.Application.Interfaces.MuscleGroup;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscleGroupController : ControllerBase
    {
        private readonly IMuscleGroupQueryService _muscleGroupQueryService;

        public MuscleGroupController(IMuscleGroupQueryService muscleGroupQueryService)
        {
            _muscleGroupQueryService = muscleGroupQueryService;
        }

        /// <summary>
        /// Obtener grupos musculares
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(MuscleGroupResponse), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _muscleGroupQueryService.GetAll();
            if (result == null)
                return new JsonResult(new { }) { StatusCode = 200 };
            else
                return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
