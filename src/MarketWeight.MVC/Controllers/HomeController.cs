using Microsoft.AspNetCore.Mvc;
using MarketWeight.Core.Persistencia;
using MarketWeight.MVC.ViewModels.Cuenta;
using System.Threading.Tasks;

namespace MarketWeight.MVC.Controllers;

public class HomeController : Controller
{

    private IRepoMoneda _repoMoneda;
    private IRepoUsuario _repoUsuario;

    public HomeController(IRepoMoneda repoMoneda, IRepoUsuario repoUsuario)
    {
        _repoMoneda = repoMoneda;
        _repoUsuario = repoUsuario;
    }

    public async Task<IActionResult> Index()
    {
        if (TempData["CurrentUserId"] != null)
        {
            uint userId = Convert.ToUInt32(TempData["CurrentUserId"]);
            LoginViewModel.CurrentUser = await _repoUsuario.DetalleAsync(userId);
        }
        return View();
    }
}
