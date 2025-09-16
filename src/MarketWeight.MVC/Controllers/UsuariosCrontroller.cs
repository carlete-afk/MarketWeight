using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using System.Reflection.Metadata;

namespace MarketWeight.MVC.Controllers;

public class UsuariosController : Controller
{
    private IRepoUsuario _repoUsuario;
    private IRepoMoneda _repoMoneda;

    public UsuariosController(IRepoUsuario repoUsuario, IRepoMoneda repoMoneda)
    {
        _repoUsuario = repoUsuario;
        _repoMoneda = repoMoneda;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usuarios = await _repoUsuario.ObtenerAsync();
        return View(usuarios);
    }

    [HttpGet]
    public async Task<IActionResult> Compra()
    {
        var criptos = await _repoMoneda.ObtenerAsync();
        return View(criptos);
    }

    [HttpPost]
    public async Task<IActionResult> Compra(uint idusuario, decimal cantidad, uint idmoneda)
    {
        await _repoUsuario.CompraAsync(idusuario, cantidad, idmoneda);
        ViewBag.Mensaje = "Compra realizada correctamente.";
        return RedirectToAction("Index", "Home");
    }
}
