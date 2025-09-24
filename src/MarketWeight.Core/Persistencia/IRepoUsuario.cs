namespace MarketWeight.Core.Persistencia;

public interface IRepoUsuario :
    IRepoAlta<Usuario>,
    IRepoListado<Usuario>,
    IRepoDetalle<Usuario, uint>
{
    public void Compra(uint idusuario, decimal cantidad, uint idmoneda);
    public Task CompraAsync(uint idusuario, decimal cantidad, uint idmoneda);

    public void Vender(uint idusuario, decimal cantidad, uint idmoneda);
    public Task VenderAsync(uint idusuario, decimal cantidad, uint idmoneda);

    public void Ingreso(uint idusuario, decimal saldo);
    public Task IngresoAsync(uint idusuario, decimal saldo);

    public void Transferencia(uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);
    public Task TransferenciaAsync(uint idmoneda, decimal cantidad, uint idusuarioTransfiere, uint idusuarioTransferido);

    public IEnumerable<Usuario> ObtenerPorCondicion(string condicion);
    public Task<IEnumerable<Usuario>> ObtenerPorCondicionAsync(string condicion);

    public IEnumerable<Usuario> ObtenerPorEmail(string email);
    public Task<IEnumerable<Usuario>> ObtenerPorEmailAsync(string email);

    public IEnumerable<UsuarioMoneda> ObtenerUsuarioMoneda();
    public Task<IEnumerable<UsuarioMoneda>> ObtenerUsuarioMonedaAsync();

    public IEnumerable<UsuarioMoneda> ObtenerPorCondicionUsuarioMoneda(uint? userid, decimal? cantidad);
    public Task<IEnumerable<UsuarioMoneda>> ObtenerPorCondicionUsuarioMonedaAsync(uint? userid, decimal? cantidad);

    public Usuario? DetalleCompleto(uint idUsuario);
    public Task<Usuario?> DetalleCompletoAsync(uint idUsuario);
}
