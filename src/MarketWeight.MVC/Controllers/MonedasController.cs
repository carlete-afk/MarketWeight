using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.Controllers;

public class MonedasController : Controller
{
    private IRepoMoneda _repoMoneda;

    public MonedasController(IRepoMoneda repoMoneda)
    {
        _repoMoneda = repoMoneda;
    }   

    public async Task<IActionResult> Index()
    {
        var monedas = await _repoMoneda.ObtenerAsync();
        return View(monedas);
    }
}
