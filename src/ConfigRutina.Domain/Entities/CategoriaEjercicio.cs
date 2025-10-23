using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Domain.Entities
{
    public class CategoriaEjercicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // relacion
        public ICollection<Ejercicio>? EjercicioLista { get; set; }
    }
}
