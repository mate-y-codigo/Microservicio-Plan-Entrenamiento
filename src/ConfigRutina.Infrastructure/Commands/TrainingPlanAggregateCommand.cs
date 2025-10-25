using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Commands
{
    using ConfigRutina.Application.CustomExceptions;
    using ConfigRutina.Application.Interfaces.TrainingPlan;
    using ConfigRutina.Domain.Entities;
    using ConfigRutina.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class TrainingPlanAggregateCommand : ITrainingPlanAggregateCommand
    {
        private readonly ConfigRutinaDB _db;

        public TrainingPlanAggregateCommand(ConfigRutinaDB db)
        {
            _db = db;
        }

        public async Task CreatePlanCompleteAsync(
            PlanEntrenamiento plan,
            List<SesionEntrenamiento> sessions,
            List<EjercicioSesion> exercises)
        {
            // 1) VALIDACIONES
            // 1.1 entrenador + nombre
            var dupName = await _db.PlanEntrenamientos.AsNoTracking()
                .AnyAsync(p => p.IdEntrenador == plan.IdEntrenador &&
                               p.Nombre.ToLower() == plan.Nombre.ToLower());
            if (dupName)
                throw new ConflictException($"Ya existe un plan con el nombre '{plan.Nombre}' para este entrenador.");

            // 1.2 Ejercicios deben existir
            var exerciseIds = exercises.Select(e => e.IdEjercicio).Distinct().ToList();
            if (exerciseIds.Count > 0)
            {
                var existentes = await _db.Ejercicios.AsNoTracking()
                    .Where(e => exerciseIds.Contains(e.Id))
                    .Select(e => e.Id)
                    .ToListAsync();

                var faltantes = exerciseIds.Except(existentes).ToList();
                if (faltantes.Any())
                    throw new NotFoundException($"No existen los ejercicios: {string.Join(", ", faltantes)}");
            }

            // 1.3 Validar órdenes por sesión: únicos y consecutivos (>0)
            var porSesion = exercises.GroupBy(e => e.IdSesionEntrenamiento);
            foreach (var g in porSesion)
            {
                var orders = g.Select(x => x.Orden).ToList();
                if (orders.Any(o => o <= 0))
                    throw new BadRequestException("Los órdenes de ejercicios deben ser > 0.");
                if (orders.Count != orders.Distinct().Count())
                    throw new BadRequestException("Hay órdenes de ejercicios repetidos dentro de una sesión.");
            }

            if (sessions.Any(s => s.Orden <= 0))
                throw new BadRequestException("Los órdenes de las sesiones deben ser > 0.");
            if (sessions.Count != sessions.Select(s => s.Orden).Distinct().Count())
                throw new BadRequestException("Hay órdenes de sesión repetidos.");

            // 2) INSERCIÓN
            await using var tx = await _db.Database.BeginTransactionAsync();

            _db.PlanEntrenamientos.Add(plan);
            _db.SesionEntrenamientos.AddRange(sessions);
            _db.EjercicioSesiones.AddRange(exercises);

            await _db.SaveChangesAsync();
            await tx.CommitAsync();
        }
    }

}
