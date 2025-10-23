using ConfigRutina.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Request.Exercise
{
    public class ExerciseCreateRequest
    {
        [JsonPropertyName("nombre")]
        public required string nombre { get; set; } = string.Empty;

        [JsonPropertyName("musculoPrincipal")]
        public required string musculoPrincipal { get; set; } = string.Empty;

        [JsonPropertyName("grupoMuscular")]
        public required string grupoMuscular { get; set; } = string.Empty;

        [JsonPropertyName("urlDemo")]
        public string urlDemo { get; set; } = string.Empty;

        [JsonPropertyName("categoria")]
        public required int categoriaEjercicio { get; set; }
    }
}
