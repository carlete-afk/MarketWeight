using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.Ado.Dapper.Test;

public class RepoHistorialTestAsync : TestBase
{
    IRepoHistorial _repo;
    public RepoHistorialTestAsync() : base()
        => _repo = new RepoHistorial(Conexion);

    [Fact]
    public async Task TraerAsyncOK()
    {
        var historiales = await _repo.ObtenerAsync();
        
        Assert.NotEmpty(historiales);
        Assert.Contains(historiales,
            h => h.IdUsuario == 2);
    }
}