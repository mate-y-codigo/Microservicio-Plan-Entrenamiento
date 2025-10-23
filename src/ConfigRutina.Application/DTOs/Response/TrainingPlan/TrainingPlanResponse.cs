using ConfigRutina.Application.DTOs.Response.TrainingSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.TrainingPlan
{
    public class TrainingPlanResponse
    {
        public Guid id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public bool esPlantilla { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public bool activo { get; set; } = false;
        public List<TrainingSessionResponse> trainingSessions { get; set; }
    }
}
