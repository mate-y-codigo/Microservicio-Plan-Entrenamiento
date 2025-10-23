using ConfigRutina.Application.DTOs.Response.ExerciseSession;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Application.Services.TrainingSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Mappers
{
    public class TrainingSessionMapper
    {
        public static TrainingSessionResponse TrainingSessionToResponse(SesionEntrenamiento sesion) {
            var response = new TrainingSessionResponse
            {
                id = sesion.Id,
                trainingPlanId = sesion.IdPlanEntrenamiento,
                name = sesion.Nombre,
                order = sesion.Orden,
                exerciseSession = sesion.EjercicioSesionLista.Select(Ejercicio => new ExerciseSessionShortResponse
                {
                    id = Ejercicio.Id,
                    idExercise = Ejercicio.EjercicioEn.Id
                }).ToList()
            };
            return response;
              
        }
    }
}
