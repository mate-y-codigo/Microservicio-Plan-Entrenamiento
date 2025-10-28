using ConfigRutina.Application.DTOs.Response.MuscleGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.MuscleGroup
{
    public interface IMuscleGroupQueryService
    {
        Task<List<MuscleGroupResponse>?> GetAll();
    }
}
