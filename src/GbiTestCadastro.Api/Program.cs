using GbiTestCadastro.Api;
using GbiTestCadastro.Api.Infra.Configurations;
using GbiTestCadastro.Application.Usecases.Usuarios.Create;
using GbiTestCadastro.Application.Usecases.Usuarios.Delete;
using GbiTestCadastro.Application.Usecases.Usuarios.Read;
using GbiTestCadastro.Application.Usecases.Usuarios.Update;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Infra.Mappers.GbiTestCadastroProfile;
using GbiTestCadastro.Infra.Persistence.Sql.Contexts;
using GbiTestCadastro.Infra.Persistence.Sql.Repositories;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped<IUsuarioCreateUsecases, UsuarioCreateUsecases>();
builder.Services.AddScoped<IUsuarioReadUsecases, UsuarioReadUsecases>();
builder.Services.AddScoped<IUsuarioDeleteUsecases, UsuarioDeleteUsecases>();
builder.Services.AddScoped<IUsuarioUpdateUsecases, UsuarioUpdateUsecases>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAutoMapper(typeof(UsuariosProfile));


var app = builder.Build();

app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  
    endpoints.MapControllers();
});

// Garantir que o banco de dados esteja criado ao iniciar o aplicativo
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


await app.RunAsync();

public partial class Program { }
