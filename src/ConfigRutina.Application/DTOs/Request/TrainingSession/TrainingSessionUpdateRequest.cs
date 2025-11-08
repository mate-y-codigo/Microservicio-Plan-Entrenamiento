using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.TrainingSession
{
    public class TrainingSessionUpdateRequest
    {
        public Guid IdSesionEntrenamiento { get; set; }
        public required int orden { get; set; }
        public List<SessionExerciseUpdateRequest> EjercicioSesiones { get; set; } = new();
    }
}
