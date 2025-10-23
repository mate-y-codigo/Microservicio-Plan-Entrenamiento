using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Services.CategoryExercise;
using ConfigRutina.Application.Services.Exercise;
using ConfigRutina.Application.Services.ExerciseSession;
using ConfigRutina.Application.Services.TrainingPlan;
using ConfigRutina.Application.Services.TrainingSession;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Commands;
using ConfigRutina.Infrastructure.Data;
using ConfigRutina.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MicroserviceCorsPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<ConfigRutinaDB>(options => options.UseNpgsql(connectionString));

// each time the dependency is requested, a new instance is created.
builder.Services.AddTransient<IValidatorExerciseCreateRequest, ValidatorExerciseCreateRequest>();

builder.Services.AddTransient<IValidatorExerciseDeleteRequest, ValidatorExerciseDeleteRequest>();

builder.Services.AddTransient<IValidateExerciseUpdateRequest, ValidateExerciseUpdateRequest>();

builder.Services.AddTransient<IValidatorExerciseSearchRequest, ValidatorExerciseSearchRequest>();
builder.Services.AddTransient<IValidatorExerciseSearchByIdRequest, ValidatorExerciseSearchByIdRequest>();

// a single instance for the entire application.
builder.Services.AddScoped<IExcerciseQueryService, ExerciseQueryService>();
builder.Services.AddScoped<IExcerciseQuery<Ejercicio>, ExerciseQuery>();

builder.Services.AddScoped<IExcerciseCommandService, ExerciseCommandService>();
builder.Services.AddScoped<IExcerciseCommand, ExerciseCommand>();

builder.Services.AddScoped<ICategoryExcerciseQueryService, CategoryExerciseQueryService>();
builder.Services.AddScoped<ICategoryExcerciseQuery<List<CategoriaEjercicio>>, CategoryExerciseQuery>();

// training plan dependencies
builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();

//training Session Dependencies
builder.Services.AddScoped<ITrainingSessionCommand, TrainingSessionCommand>();
builder.Services.AddScoped<ITrainingSessionQuery, TrainingSessionQuery>();
builder.Services.AddScoped<ITrainingSessionService, TrainingSessionService>();

// Excercise Session Dependencies
builder.Services.AddScoped<IExerciseSessionCommand, ExerciseSessionCommand>();
builder.Services.AddScoped<IExerciseSessionQuery, ExerciseSessionQuery>();
builder.Services.AddScoped<IExerciseSessionService, ExerciseSessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MicroserviceCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();