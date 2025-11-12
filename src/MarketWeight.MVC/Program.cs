using System.Data;
using MySqlConnector;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using MarketWeight.MVC.ViewModels.Cuenta;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));
builder.Services.AddScoped<IRepoMoneda, RepoMoneda>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    if (context.Request.Cookies.TryGetValue("CurrentUserId", out var userIdValue)
        && uint.TryParse(userIdValue, out var userId))
    {
        var repoUsuario = context.RequestServices.GetRequiredService<IRepoUsuario>();
        LoginViewModel.CurrentUser =
            await repoUsuario.DetalleCompletoAsync(userId);

        var repoMoneda = context.RequestServices.GetRequiredService<IRepoMoneda>();
        LoginViewModel.Initialize(repoMoneda);
        await LoginViewModel.UpdateCriptosAsync();
    }
    else
    {
        LoginViewModel.CurrentUser = null;
        LoginViewModel.ResetCriptos();
    }

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
