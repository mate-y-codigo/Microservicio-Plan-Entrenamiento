using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.TrainingPlan
{
    public class TrainingPlanStatusResponse
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [JsonPropertyName("fechaActualizacion")]
        public DateTime fechaActualizacion { get; set; }

        [JsonPropertyName("activo")]
        public bool activo { get; set; }

    }
}
