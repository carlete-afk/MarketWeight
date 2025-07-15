using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using MarketWeight.Core;

var builder = WebApplication.CreateBuilder(args);

//  Obtener la cadena de conexi�n desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySQL");

//  Registrando IDbConnection para que se inyecte como dependencia
//  Cada vez que se inyecte, se crear� una nueva instancia con la cadena de conexi�n
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.MapGet("/marketweight/monedas", async (IRepoMoneda repo) =>
    await repo.ObtenerAsync());

app.MapGet("/marketweight/monedas/{id}", async (uint id, IRepoMoneda repo) =>
    await repo.DetalleAsync(id)
        is Moneda moneda
            ? Results.Ok(moneda)
            : Results.NotFound());

app.MapPost("/marketweight/monedas", async (Moneda moneda, IRepoMoneda repo) =>
{
    await repo.AltaAsync(moneda);

    return Results.Created($"/marketweight/monedas/{moneda.Nombre}", moneda);
});

app.MapPut("/marketweight/monedas/{id}", async (uint id, Moneda inputMoneda, IRepoMoneda repo) =>
{
    var moneda = await repo.DetalleAsync(id);

    if (moneda is null) return Results.NotFound();

    moneda.Nombre = inputMoneda.Nombre;
    moneda.Precio = inputMoneda.Precio;
    moneda.Cantidad = inputMoneda.Cantidad;

    await repo.ActualizarAsync(moneda, id);

    return Results.NoContent();
});

app.MapDelete("/marketweight/monedas/{id}", async (uint id, IRepoMoneda repo) =>
{
    if (await repo.DetalleAsync(id) is Moneda moneda)
    {
        await repo.EliminarAsync(id);
        return Results.NoContent();
    }

    return Results.NoContent();
});

// ---------------------
// ---------------------

app.MapGet("/marketweight/usuarios", async (IRepoUsuario repo) =>
    await repo.ObtenerAsync());

app.MapGet("/marketweight/usuarios/{id}", async (uint id, IRepoUsuario repo) =>
    await repo.DetalleAsync(id)
        is Usuario usuario
            ? Results.Ok(usuario)
            : Results.NotFound());

app.MapPost("/marketweight/usuarios", async (Usuario usuario, IRepoUsuario repo) =>
{
    await repo.AltaAsync(usuario);

    return Results.Created($"/marketweight/usuarios/{usuario.Nombre}", usuario);
});

app.MapGet("/marketweight/usuarios/detalle/{id}", async (uint id, IRepoUsuario repo) =>
    await repo.DetalleCompletoAsync(id)
        is Usuario usuario
            ? Results.Ok(usuario)
            : Results.NotFound());

app.Run();