using MarketWeight.Core;

namespace MarketWeight.MVC.ViewModels.Account;

public class LoginViewModel(string email, string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;

    public static Usuario? CurrentUser { get; set; }
}