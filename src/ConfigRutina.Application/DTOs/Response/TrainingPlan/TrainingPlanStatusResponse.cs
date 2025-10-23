using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.TrainingPlan
{
    public class TrainingPlanStatusResponse
    {
        public string nombre { get; set; } = string.Empty;
        public DateTime fechaActualizacion { get; set; }
        public bool activo { get; set; }

    }
}
