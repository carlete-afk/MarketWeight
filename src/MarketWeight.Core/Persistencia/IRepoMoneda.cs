namespace MarketWeight.Core.Persistencia;

public interface IRepoMoneda :
    IRepoAlta<Moneda>,
    IRepoListado<Moneda>,
    IRepoDetalle<Moneda, uint>
{
    public IEnumerable<Moneda> ObtenerConCondicion(string condicion);
    public Task<IEnumerable<Moneda>> ObtenerConCondicionAsync(string condicion);
    public void Actualizar(Moneda moneda, uint id);
    public Task ActualizarAsync(Moneda moneda, uint id);
    public void Eliminar(uint id);
    public Task EliminarAsync(uint id);
}
