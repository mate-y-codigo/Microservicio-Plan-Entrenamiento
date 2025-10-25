using ConfigRutina.Application.DTOs.Request.TrainingSession;
using ConfigRutina.Application.DTOs.Response.TrainingSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Interfaces.TrainingSession
{
    public interface ITrainingSessionService
    {
        Task<SesionEntrenamiento> CreateAsync(Guid planId, TrainingSessionCreateRequest request);
        Task<TrainingSessionResponse> GetTrainingSessionById(string id);
    }
}
