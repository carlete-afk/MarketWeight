namespace MarketWeight.Core.Persistencia;

public interface IRepoUsuarioMoneda
{
    public UsuarioMoneda? ObtenerCriptosUsuario(uint idUsuario, uint idMoneda);
    public Task<UsuarioMoneda?> ObtenerCriptosUsuarioAsync(uint idUsuario, uint idMoneda);

}
