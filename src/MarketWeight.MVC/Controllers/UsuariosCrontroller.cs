using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using System.Reflection.Metadata;
using MarketWeight.MVC.ViewModels;

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
    public async Task<IActionResult> Compra(string cantidad, uint idmoneda)
    {
        if (LoginViewModel.CurrentUser == null)
        {
            return RedirectToAction("Login", "Cuenta");
        }

        uint idusuario = LoginViewModel.CurrentUser.IdUsuario;

        try
        {
            await _repoUsuario.CompraAsync(idusuario, Convert.ToDecimal(cantidad.Replace(".", ",")), idmoneda);
            TempData["SuccessMessage"] = "Compra realizada correctamente.";
            return RedirectToAction("Compra");
        }
        catch (MySqlConnector.MySqlException ex) when (ex.Message.Contains("Saldo Insuficiente"))
        {
            TempData["ErrorMessage"] = "No tienes suficiente saldo para realizar esta compra.";
            return RedirectToAction("Compra");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocurrió un error inesperado. Por favor, inténtalo de nuevo más tarde.";
            return RedirectToAction("Compra");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Deposito(decimal monto)
    {
        if (monto <= 0)
        {
            TempData["ErrorMessage"] = "El monto del depósito debe ser mayor a cero.";
            return RedirectToAction("Perfil", "Cuenta");
        }
            
        if (LoginViewModel.CurrentUser == null)
            return RedirectToAction("Login", "Cuenta");

        uint idusuario = LoginViewModel.CurrentUser.IdUsuario;

        try
        {
            await _repoUsuario.IngresoAsync(idusuario, monto);
            TempData["SuccessMessage"] = "Deposito realizado correctamente.";
            return RedirectToAction("Perfil", "Cuenta");
        }

        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ocurrió un error inesperado. Por favor, inténtalo de nuevo más tarde.\n" + ex.Message;
            return RedirectToAction("Perfil", "Cuenta");
        }
    }
}
