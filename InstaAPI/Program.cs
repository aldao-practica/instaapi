using Infrastructure;
using Application;
using InstaAPI.Middleware;
using Domain.Interfaces;
using InstaAPI.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApplication();

if (builder.Configuration.GetValue<bool>("UseMockRepositories"))
{
    // Usás mocks
    builder.Services.AddScoped<IUnitOfWork>(provider =>
    {
        var mockUserRepo = new MockUserRepository();
        return new MockUnitOfWork(mockUserRepo);
    });
}
else
{
    // Usás repositorios reales
    builder.Services.AddInfrastructure(builder.Configuration);
    //AddApplication(): Registra MediatR y FluentValidation
    //AddInfrastructure(): Registra DbContext y Repositorios
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();