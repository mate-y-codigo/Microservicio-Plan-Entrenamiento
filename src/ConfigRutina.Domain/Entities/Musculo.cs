using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class Musculo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // FK
        public int IdGrupoMuscular { get; set; }

        // relacion
        public GrupoMuscular? GrupoMuscularEn { get; set; }
        public ICollection<Ejercicio>? EjercicioLista { get; set; }
    }
}
