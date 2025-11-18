using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.ViewModels;

public class CriptoUsuario
{
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public decimal Cantidad { get; set; }

    public CriptoUsuario (Moneda moneda, UsuarioMoneda usuarioMoneda)
    {
        Nombre = moneda.Nombre;
        Precio = moneda.Precio;
        Cantidad = usuarioMoneda.Cantidad;
    }
}