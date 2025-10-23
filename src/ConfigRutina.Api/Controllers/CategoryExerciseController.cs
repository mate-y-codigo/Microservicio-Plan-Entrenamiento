using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using Microsoft.AspNetCore.Mvc;

namespace ConfigRutina.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryExerciseController : ControllerBase
    {
        private readonly ICategoryExcerciseQueryService _categoryExcerciseService;

        public CategoryExerciseController(ICategoryExcerciseQueryService categoryExcerciseService)
        {
            _categoryExcerciseService = categoryExcerciseService;
        }

        /// <summary>
        /// Obtener categorias de ejercicios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(CategoryExerciseResponse), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryExcerciseService.GetAll();
            if (result == null)
                return new JsonResult(new { }) { StatusCode = 200 };
            else
                return new JsonResult(result) { StatusCode = 200 };
        }

    }
}
