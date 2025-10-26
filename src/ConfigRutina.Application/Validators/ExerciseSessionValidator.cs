using ConfigRutina.Application.CustomExceptions;
using ConfigRutina.Application.DTOs.Request.SessionExercise;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Application.Validators
{
    public class ExerciseSessionValidator
    {
        private readonly IExcerciseQuery<Ejercicio?> _exerciseQuery;
        public ExerciseSessionValidator(IExcerciseQuery<Ejercicio?> exerciseQuery)
        {
            _exerciseQuery = exerciseQuery;
        }
        public void ValidateCreate(SessionExerciseCreateRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException("El request no puede ser nulo.");
            }
            if (request.id == Guid.Empty)
            {
                throw new BadRequestException("El id de ejercicio debe ser un id válido.");
            }
            if (request.orden <= 0)
            {
                throw new BadRequestException("El orden del ejercicio sesión debe ser válido.");
            }
            if (request.seriesObjetivo <= 0)
            {
                throw new BadRequestException("Las series objetivo deben ser un número positivo.");
            }
            if (request.repeticionesObjetivo <= 0)
            {
                throw new BadRequestException("Las repeticiones objetivo deben ser un número positivo.");
            }
            if (request.pesoObjetivo < 0)
            {
                throw new BadRequestException("El peso objetivo no puede ser negativo.");
            }
            if (request.descanso < 0)
            {
                throw new BadRequestException("El tiempo de descanso no puede ser negativo.");
            }

            //var exercise = await _exerciseQuery.GetById(request.exerciseId);
            //if (exercise == null)
            //{
            //    throw new NotFoundException($"No se encontró un ejercicio con el id {request.exerciseId}.");
            //}
        }
    }
}
