using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response.Exercise
{
    public class ExerciseResponse
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;

        [JsonPropertyName("musculoPrincipal")]
        public string musculoPrincipal { get; set; } = string.Empty;

        [JsonPropertyName("grupoMuscular")]
        public string grupoMuscular {  get; set; } = string.Empty;

        [JsonPropertyName("urlDemo")]
        public string urlDemo {  get; set; } = string.Empty;

        [JsonPropertyName("activo")]
        public bool activo { get; set; }

        [JsonPropertyName("categoria")]
        public CategoryExerciseResponse? categoria { get; set; }
    }
}
