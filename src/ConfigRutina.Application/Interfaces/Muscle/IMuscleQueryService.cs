using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.Muscle
{
    public interface IMuscleQueryService
    {
        Task<List<MuscleResponse>?> GetAll();
        Task<List<MuscleResponse>?> Search(int idMuscle, int idMuscleGroup, string? muscle, string? muscleGroup);
    }
}
