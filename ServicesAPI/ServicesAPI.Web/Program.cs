using FluentValidation;
using MediatR;
using ServicesAPI.Application.Behaviors;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Mappings;
using ServicesAPI.Application.Validators;
using ServicesAPI.Web.Extensions;
using ServicesAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration, "ContextSettings");
builder.Services.ConfigureRepositories();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateService).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();
app.MigrateDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
