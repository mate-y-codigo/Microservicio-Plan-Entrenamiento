using ConfigRutina.Application.DTOs.Response.Muscle;
using ConfigRutina.Application.Interfaces.CategoryExcercise;
using ConfigRutina.Application.Interfaces.Excercise;
using ConfigRutina.Application.Interfaces.ExcerciseSession;
using ConfigRutina.Application.Interfaces.ExerciseSession;
using ConfigRutina.Application.Interfaces.Muscle;
using ConfigRutina.Application.Interfaces.MuscleGroup;
using ConfigRutina.Application.Interfaces.TrainingPlan;
using ConfigRutina.Application.Interfaces.TrainingSession;
using ConfigRutina.Application.Interfaces.Validators;
using ConfigRutina.Application.Mappers;
using ConfigRutina.Application.Services.CategoryExercise;
using ConfigRutina.Application.Services.Exercise;
using ConfigRutina.Application.Services.ExerciseSession;
using ConfigRutina.Application.Services.Muscle;
using ConfigRutina.Application.Services.MuscleGroup;
using ConfigRutina.Application.Services.TrainingPlan;
using ConfigRutina.Application.Services.TrainingSession;
using ConfigRutina.Application.Validators;
using ConfigRutina.Domain.Entities;
using ConfigRutina.Infrastructure.Commands;
using ConfigRutina.Infrastructure.Data;
using ConfigRutina.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto hace que los enums se serialicen como texto (no números)
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConfigRutina API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.UseInlineDefinitionsForEnums();
});

// custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<ConfigRutinaDB>(options => options.UseNpgsql(connectionString));

// each time the dependency is requested, a new instance is created.
builder.Services.AddTransient<IValidatorExerciseCreateRequest, ValidatorExerciseCreateRequest>();

builder.Services.AddTransient<IValidatorExerciseDeleteRequest, ValidatorExerciseDeleteRequest>();

builder.Services.AddTransient<IValidateExerciseUpdateRequest, ValidateExerciseUpdateRequest>();

builder.Services.AddTransient<IValidatorExerciseSearchRequest, ValidatorExerciseSearchRequest>();
builder.Services.AddTransient<IValidatorExerciseSearchByIdRequest, ValidatorExerciseSearchByIdRequest>();

builder.Services.AddTransient<IValidatorMuscleSearchRequest, ValidatorMuscleSearchRequest>();

// a single instance for the entire application.
builder.Services.AddScoped<IExcerciseQueryService, ExerciseQueryService>();
builder.Services.AddScoped<IExcerciseQuery<Ejercicio>, ExerciseQuery>();

builder.Services.AddScoped<IExcerciseCommandService, ExerciseCommandService>();
builder.Services.AddScoped<IExcerciseCommand, ExerciseCommand>();

builder.Services.AddScoped<ICategoryExcerciseQueryService, CategoryExerciseQueryService>();
builder.Services.AddScoped<ICategoryExcerciseQuery<List<CategoriaEjercicio>>, CategoryExerciseQuery>();

builder.Services.AddScoped<IMuscleGroupQueryService, MuscleGroupQueryService>();
builder.Services.AddScoped<IMuscleGroupQuery<List<GrupoMuscular>>, MuscleGroupQuery>();

builder.Services.AddScoped<IMuscleQueryService, MuscleQueryService>();
builder.Services.AddScoped<IMuscleQuery<Musculo>, MuscleQuery>();

// training plan dependencies
builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();
builder.Services.AddScoped<ITrainingPlanCommand, TrainingPlanCommand>();
builder.Services.AddScoped<ITrainingPlanQuery, TrainingPlanQuery>();
builder.Services.AddScoped<TrainingPlanMapper>();
builder.Services.AddScoped<TrainingPlanValidator>();
builder.Services.AddScoped<ITrainingPlanAggregateCommand, TrainingPlanAggregateCommand>();

//training Session Dependencies
builder.Services.AddScoped<ITrainingSessionCommand, TrainingSessionCommand>();
builder.Services.AddScoped<ITrainingSessionQuery, TrainingSessionQuery>();
builder.Services.AddScoped<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddScoped<TrainingSessionMapper>();
builder.Services.AddScoped<TrainingSessionValidator>();

// Excercise Session Dependencies
builder.Services.AddScoped<IExerciseSessionCommand, ExerciseSessionCommand>();
builder.Services.AddScoped<IExerciseSessionQuery, ExerciseSessionQuery>();
builder.Services.AddScoped<IExerciseSessionService, ExerciseSessionService>();
builder.Services.AddScoped<ExerciseSessionMapper>();
builder.Services.AddScoped<ExerciseSessionValidator>();

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