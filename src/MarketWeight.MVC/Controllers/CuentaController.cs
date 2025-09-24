using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarketWeight.MVC.Models;
using MarketWeight.Core;
using MarketWeight.Ado.Dapper;
using MarketWeight.Core.Persistencia;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MarketWeight.MVC.ViewModels.Cuenta;

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

    [HttpGet]
    public IActionResult Registro() => View();

    [HttpGet]
    public IActionResult Perfil() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var usuario = (await _repoUsuario.ObtenerPorEmailAsync(email)).FirstOrDefault();

        if (usuario != null)
        {
            // Usar SHA256 exactamente como el trigger MySQL SHA2(pass, 256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                var hashedPassword = BitConverter.ToString(hash).Replace("-", "").ToLower();

                if (usuario.Password == hashedPassword)
                {
                    LoginViewModel.CurrentUser = usuario;

                    TempData["CurrentUserId"] = usuario.IdUsuario.ToString();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = $"Contraseña incorrecta.";
                    return RedirectToAction("Login");
                }
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Este email no está registrado.";
            return RedirectToAction("Login");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Registro(string nombre, string apellido, string email, string pass, string confirmPass)
    {
        if (pass != confirmPass)
        {
            TempData["ErrorMessage"] = "Las contraseñas no coinciden.";
            return RedirectToAction("Registro", "Cuenta");
        }

        var usuarioExistente = (await _repoUsuario.ObtenerPorEmailAsync(email)).FirstOrDefault();
        if (usuarioExistente != null)
        {
            TempData["ErrorMessage"] = "Este email ya está registrado.";
            return RedirectToAction("Registro", "Cuenta");
        }

        var nuevoUsuario = new Usuario
        {
            Nombre = nombre,
            Apellido = apellido,
            Email = email,
            Password = pass,
            Saldo = 0
        };

        await _repoUsuario.AltaAsync(nuevoUsuario);

        await Login(email, pass);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        LoginViewModel.CurrentUser = null;
        TempData["CurrentUserId"] = null;
        return RedirectToAction("Index", "Home");
    }
}
