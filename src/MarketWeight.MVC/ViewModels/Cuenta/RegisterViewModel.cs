namespace MarketWeight.MVC.ViewModels.Account
{
    public class RegisterViewModel (string nombre, string apellido, string email, string password)
    {
        public string Nombre { get; set; } = nombre;
        public string Apellido { get; set; } = apellido;
        public string Email { get; set; } = email;
         public string Password { get; set; } = password;
    }
}