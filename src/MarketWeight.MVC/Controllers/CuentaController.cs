using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MarketWeight.MVC.ViewModels.Account;

namespace MarketWeight.MVC.Controllers;

public class CuentaController : Controller
{

    private IRepoUsuario _repoUsuario;

    public CuentaController(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }

    [HttpGet]
    public IActionResult Login() => View();

    public IActionResult Registro() => View();

    public IActionResult Logout()
    {
        // Limpiar datos de sesión
        LoginViewModel.CurrentUser = null;
        TempData["CurrentUser"] = null;
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var usuario = (await _repoUsuario.ObtenerPorCondicionAsync($"email = '{email}'")).FirstOrDefault();

        if (usuario != null)
        {
            // Usar SHA256 exactamente como el trigger MySQL SHA2(pass, 256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                // Convertir a string hexadecimal en minúsculas para coincidir con MySQL SHA2
                var hashedPassword = BitConverter.ToString(hash).Replace("-", "").ToLower();

                // Debug info
                TempData["Debug"] = new
                {
                    inputPassword = password,
                    hashedInput = hashedPassword,
                    storedHash = usuario.Password
                };

                if (usuario.Password == hashedPassword)
                {
                    LoginViewModel.CurrentUser = usuario;

                    TempData["CurrentUser"] = new
                    {
                        id = usuario.IdUsuario,
                        nombre = usuario.Nombre,
                        email = usuario.Email,
                        debug = TempData["Debug"]  // Incluir info de debug
                    }; return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Contraseña incorrecta.";
                    return RedirectToAction("Index", "Usuarios");
                }
            }

        }
        else
        {
            ViewBag.Mensaje = "Este email no está registrado.";
            return RedirectToAction("Index", "Monedas");
        }
    }
}
