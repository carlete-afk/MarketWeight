using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.ViewModels;

public class LoginViewModel
{
    public static Usuario? CurrentUser { get; set; }
    public static List<CriptoUsuario> Criptos { get; private set; } = new();
    private static IRepoMoneda? _repoMoneda;
    private static IRepoUsuarioMoneda? _repoUsuarioMoneda;

    public static void Initialize(IRepoMoneda repoMoneda, IRepoUsuarioMoneda repoUsuarioMoneda)
    {
        _repoUsuarioMoneda = repoUsuarioMoneda;
        _repoMoneda = repoMoneda;
    }

    public static async Task UpdateCriptosAsync()
{
    if (_repoUsuarioMoneda == null || _repoMoneda == null || CurrentUser?.Billetera == null)
    {
        ResetCriptos();
        return;
    }

    var nuevasCriptos = new List<CriptoUsuario>();

    foreach (var usuarioMoneda in CurrentUser.Billetera)
    {
        // Obtener el registro de UsuarioMoneda filtrando por idUsuario e idMoneda
        var userCripto = await _repoUsuarioMoneda.ObtenerCriptosUsuarioAsync(usuarioMoneda.idUsuario, usuarioMoneda.idMoneda);

        // Obtener los detalles de la moneda
        var cripto = await _repoMoneda.DetalleAsync(usuarioMoneda.idMoneda);

        if (userCripto != null && cripto != null)
        {
            nuevasCriptos.Add(new CriptoUsuario(cripto, userCripto));
        }
    }

    Criptos = nuevasCriptos;
}

    public static void ResetCriptos() => Criptos = [];
}