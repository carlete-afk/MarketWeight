using MarketWeight.Core;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.ViewModels.Cuenta;

public class LoginViewModel
{
    public static Usuario? CurrentUser { get; set; }
    private static IRepoUsuario _repoUsuario;

    public static void Initialize(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }
}