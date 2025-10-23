using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConfigRutina.Application.DTOs.Response
{
    public class CategoryExerciseResponse
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("nombre")]
        public string nombre { get; set; } = string.Empty;
    }
}
