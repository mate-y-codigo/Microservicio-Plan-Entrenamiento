using ConfigRutina.Application.Interfaces.MuscleGroup;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Queries
{
    public class MuscleGroupQuery : IMuscleGroupQuery<List<GrupoMuscular>>
    {
        public readonly ConfigRutinaDB _configRutina;

        public MuscleGroupQuery(ConfigRutinaDB configRutina)
        {
            _configRutina = configRutina;
        }

        public async Task<List<GrupoMuscular>> GetAll()
        {
            return await _configRutina.GruposMusculares.AsNoTracking().ToListAsync();
        }
    }
}
