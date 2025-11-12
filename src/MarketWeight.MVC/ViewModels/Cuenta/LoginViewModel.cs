using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.ViewModels.Cuenta;

public class LoginViewModel
{
    public static Usuario? CurrentUser { get; set; }
    public static List<Moneda> Criptos { get; private set; } = new();
    private static IRepoMoneda? _repoMoneda;

    public static void Initialize(IRepoMoneda repoMoneda)
    {
        _repoMoneda = repoMoneda;
    }

    public static async Task UpdateCriptosAsync()
    {
        if (_repoMoneda == null || CurrentUser?.Billetera == null)
        {
            ResetCriptos();
            return;
        }

        var nuevasCriptos = new List<Moneda>();

        foreach (var usuarioMoneda in CurrentUser.Billetera)
        {
            var cripto = await _repoMoneda.DetalleAsync(usuarioMoneda.idMoneda);
            if (cripto != null)
            {
                nuevasCriptos.Add(cripto);
            }
        }

        Criptos = nuevasCriptos;
    }

    public static void ResetCriptos() => Criptos = [];
}