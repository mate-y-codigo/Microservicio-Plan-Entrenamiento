using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class Ejercicio
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? UrlDemo { get; set; }
        public bool Activo { get; set; }

        // FK
        public int IdCategoriaEjercicio { get; set; }
        public int IdMusculo { get; set; }

        // Relacion
        public CategoriaEjercicio? CategoriaEjercicioEn { get; set; }
        public Musculo? MusculoEn { get; set; }
        public ICollection<EjercicioSesion>? EjercicioSesionLista { get; set; }
    }
}
