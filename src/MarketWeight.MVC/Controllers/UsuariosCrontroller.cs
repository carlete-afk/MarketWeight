using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;

namespace MarketWeight.MVC.Controllers;

public class UsuariosController : Controller
{
    private IRepoUsuario _repoUsuario;

    public UsuariosController(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }   

    public async Task<IActionResult> Index()
    {
        var usuarios = await _repoUsuario.ObtenerAsync();
        return View(usuarios);
    }
}
