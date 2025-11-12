using Microsoft.AspNetCore.Mvc;

namespace MarketWeight.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
