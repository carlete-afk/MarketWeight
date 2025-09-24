namespace MarketWeight.Core;
public class Usuario
{
    public uint IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public  decimal Saldo { get; set; }
    public List<Historial>? Transacciones { get; set; }
    public List<UsuarioMoneda>? Billetera { get; set; }
}