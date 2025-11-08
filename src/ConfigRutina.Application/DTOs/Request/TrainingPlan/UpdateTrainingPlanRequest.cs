using ConfigRutina.Application.DTOs.Request.TrainingSession;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.TrainingPlan
{
    public class UpdateTrainingPlanRequest
    {
       public List<TrainingSessionUpdateRequest> sesionEntrenamientos { get; set; } = new();
    }
}
