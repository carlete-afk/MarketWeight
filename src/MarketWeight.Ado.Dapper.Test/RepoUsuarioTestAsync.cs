using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoUsuarioTestAsync : TestBase
{

    IRepoUsuario _repo;
    public RepoUsuarioTestAsync() : base()
        => _repo = new RepoUsuario(Conexion);

    [Fact]
    public async Task TraerAsyncOK()
    {
        var usuarios = await _repo.ObtenerAsync();

        Assert.NotEmpty(usuarios);
        Assert.Contains(usuarios,
            m => m.Nombre == "Ana");
    }

    [Fact]
    public async Task IngresarDineroAsyncOK()
    {
        await _repo.IngresoAsync(1, 7707m);
        await _repo.IngresoAsync(2, 0420m);
        await _repo.IngresoAsync(3, 5000m);
        await _repo.IngresoAsync(4, 6666m);
    }

    [Fact]
    public async Task AltaUsuarioAsyncOK()
    {
        Usuario usuarioWalter = new()
        {
            Nombre = "Walte",
            Apellido = "Beníte",
            Email = "waltercoocker@gmail.com",
            Password = "314159265358979"
        };

        Usuario usuarioJorge = new()
        {
            Nombre = "Jorge",
            Apellido = "Casco",
            Email = "JorgeCasco@gmail.com",
            Password = "jorge123"
        };

        Usuario usuarioGuido = new()
        {
            Nombre = "Guido",
            Apellido = "Gavilán",
            Email = "guidopepin@gmail.com",
            Password = "guidopepin123"
        };

        Usuario usuarioCarlos = new()
        {
            Nombre = "Carlos",
            Apellido = "Bello",
            Email = "carloselbello@gmail.com",
            Password = "carlos123"
        };

        await _repo.AltaAsync(usuarioWalter);
        await _repo.AltaAsync(usuarioJorge);
        await _repo.AltaAsync(usuarioGuido);
        await _repo.AltaAsync(usuarioCarlos);

        var usuarios = await _repo.ObtenerAsync();

        Assert.NotEmpty(usuarios);
        
        Assert.Contains(usuarios,
            m => m.Nombre == "Walte" ||
            m.Nombre == "Jorge" ||
            m.Nombre == "Guido" ||
            m.Nombre == "Carlos"
        );
    }

    [Fact]
    public async Task ComprarMonedaAsyncOK()
    {
        await _repo.CompraAsync(3, 0.5m, 2);
        await _repo.CompraAsync(2, 1m, 3);
        await _repo.CompraAsync(2, 5m, 1);
    }

    [Fact]
    public async Task ComprarMonedaAsyncFail()
    {
        var error = await Assert.ThrowsAnyAsync<Exception>(async () => await _repo.CompraAsync(6, 0.5m, 1));
        Assert.Contains("Insuficiente", error.Message);
    }

    [Fact]
    public async Task VenderMonedaAsyncOK()
    {
        await _repo.VenderAsync(2, 1m, 1);
    }

    [Fact]
    public async Task VenderMonedaAsyncFail()
    {
        var error = await Assert.ThrowsAnyAsync<Exception>(async () => await _repo.VenderAsync(5, 0.5m, 2));
        Assert.Contains("Insuficiente", error.Message);

        error = await Assert.ThrowsAnyAsync<Exception>(async () => await _repo.VenderAsync(6, 0.5m, 5));
        Assert.Contains("Insuficiente", error.Message);
    }

    [Fact]
    public async Task ObtenerPorCondicionAsyncOK()
    {
        var usuarios = await _repo.ObtenerPorCondicionAsync("saldo >= 1000");
        Assert.NotEmpty(usuarios);
    }

    [Fact]
    public async Task TransferenciaAsyncOK()
    {
        var usuariosMoneda1 = await _repo.ObtenerPorCondicionUsuarioMonedaAsync(2, 0.5m); /*string? userid, decimal cantidad*/
        await _repo.CompraAsync(2, 2.5m, 1);

        Assert.NotEmpty(usuariosMoneda1);

        await _repo.TransferenciaAsync(2, 0.5m, 2, 6);

        var usuariosMoneda2 = await _repo.ObtenerPorCondicionUsuarioMonedaAsync(6, 0.5m);
        Assert.NotEmpty(usuariosMoneda2);
    }

    [Fact]
    public async Task TransferenciaAsyncFAIL()
    {
        var error = await Assert.ThrowsAnyAsync<Exception>(async () => await _repo.TransferenciaAsync(2, 0.5m, 8, 6));
        Assert.Equal("Cantidad Insuficiente!", error.Message);
    }

    [Fact]
    public async Task DetalleCompletoAsyncOK()
    {
        var usuario = await _repo.DetalleCompletoAsync(1);
        Assert.NotNull(usuario);
    }
    
    [Fact]
    public async Task DetalleCompletoBilleteraAsyncOK()
    {
        var usuario = await _repo.DetalleCompletoAsync(2);
        Assert.NotNull(usuario);
        Assert.NotNull(usuario.Billetera);
        Assert.NotNull(usuario.Transacciones);
        Assert.NotEmpty(usuario.Billetera);
        Assert.NotEmpty(usuario.Transacciones);
    }
}