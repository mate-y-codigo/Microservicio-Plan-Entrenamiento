using ConfigRutina.Application.DTOs.Response;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Services.CategoryExercise
{
    public class CategoryExerciseQueryService : ICategoryExcerciseQueryService
    {
        private readonly ICategoryExcerciseQuery<List<CategoriaEjercicio>> _categoryExercise;

        public CategoryExerciseQueryService(ICategoryExcerciseQuery<List<CategoriaEjercicio>> categoryExcercise)
        {
            _categoryExercise = categoryExcercise;
        }

        public async Task<List<CategoryExerciseResponse>?> GetAll()
        {
            List<CategoryExerciseResponse> categoryExerciseResponse = new List<CategoryExerciseResponse>();

            var result = await _categoryExercise.GetAll();

            if (result != null)
            {
                foreach (var ce in result)
                    categoryExerciseResponse.Add(CategoryExerciseMapper.ToCategoryExerciseResponse(ce));

                return categoryExerciseResponse;
            }

            return null;
        }
    }
}
