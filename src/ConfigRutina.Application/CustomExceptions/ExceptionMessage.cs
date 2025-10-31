using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.CustomExceptions
{
    public static class ExceptionMessage
    {
        public const string ExerciseNameLength = "El nombre del ejercicio es demasiado largo";
        public const string ExerciseNameRequired = "El nombre del ejercicio es obligatorio";
        public const string ExerciseNameExist = "El nombre del ejercicio ya existe";

        public const string ExerciseIdInvalidFormat = "Formato del ID del ejercicio inválido";
        public const string ExerciseIdUnknown = "Ejercicio no encontrado";

        public const string ExerciseCategoryInvalid = "La categoría del ejercicio seleccionada no existe";
        public const string ExerciseStatusInvalid = "El estado del ejercicio especificado no es válido";

        public const string ExerciseUrlDemoInvalid = "La url ingresada no es válida";

        public const string MuscleInvalid = "El ID del músculo seleccionado no existe";
        public const string MuscleGroupInvalid = "El ID del grupo muscular seleccionado no existe";
        public const string MuscleNameLength = "El nombre del musculo es demasiado largo";
        public const string MuscleGroupNameLength = "El nombre del grupo muscular es demasiado largo";

        public const string TrainingPlanIdInvalidFormat = "Formato del ID del ejercicio inválido";
        public const string TrainingPlanInvalid = "El ID del plan de entrenamiento seleccionado no existe";
    }
}
