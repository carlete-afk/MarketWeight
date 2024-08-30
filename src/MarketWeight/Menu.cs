
using System.Runtime.CompilerServices;

class Menu
{
    private static ConsoleKey _tecla;
    private static int _opcionActual = 0;
    private static string[] _opciones = ["Registrarse", "Iniciar Sesión", "Calcular", "Salir"];
    internal static string Centrar(string text)
    {
        string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        int longestLength = lines.Max(line => line.Length);

        string spaces = new(' ', (Console.WindowWidth - longestLength) / 2);

        string centeredText = string.Join(Environment.NewLine, lines.Select(line => spaces + line));

        return centeredText;
    }

    internal static void ReiniciarColores()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static ConsoleKey EscucharTeclado()
    {
        _tecla = Console.ReadKey(true).Key;

        return _tecla;
    }

    internal static void ImprimirMenu(string[] opciones)
    {
        for (int i = 0; i < opciones.Length; i++)
        {
            if (_opcionActual == i)
            {
                ReiniciarColores();
                Console.WriteLine(Centrar($"> {opciones[i]} <"));
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Centrar(opciones[i]));
            }
        }

        ReiniciarColores();
    }

    internal static void ImprimirTitulo()
    {
        ReiniciarColores();
        Console.Clear();

        Console.WriteLine("\n\n\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Centrar(ASCII.a));

        Console.WriteLine("\n");

        ReiniciarColores();
    }

    internal static void NavegarMenu()
    {
        EscucharTeclado();
        
        if (_tecla == ConsoleKey.DownArrow && _opcionActual < _opciones.Length -1)
            _opcionActual++;

        else if (_tecla == ConsoleKey.DownArrow && _opcionActual >= _opciones.Length -1)
            _opcionActual = 0;
        

        if (_tecla == ConsoleKey.UpArrow && _opcionActual > 0)
            _opcionActual--;
        

        else if (_tecla == ConsoleKey.UpArrow && _opcionActual <= 0)
            _opcionActual = _opciones.Length -1;

        if (_tecla == ConsoleKey.Enter)
        {
            switch(_opciones[_opcionActual])
            {
                case "Registrarse":
                    ImprimirRegistro();
                break;

                case "Iniciar Sesión":
                break;

                case "Calcular":
                    MarketWeight.salirBucle = true;
                    ImprimirPedido();
                break;
                
                case "Salir":
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine(Centrar("Adios!!!"));
                    Console.WriteLine(Centrar("Gracias por utilizar nuestros servicios."));
                    Console.WriteLine("\n\n\n");
                    MarketWeight.salirBucle = true;                
                break;

                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Error! Opcion no disponible");
                break;
            }
        }
    }

    internal static void ImprimirPantallaPrincipal()
    {
        ImprimirTitulo();

        ImprimirMenu(_opciones);

        Console.WriteLine("\n\n");

        Console.ForegroundColor = ConsoleColor.DarkGray;

        foreach(string a in ASCII.Creditos)
            Console.WriteLine(Centrar(a));

        // Console.WriteLine(_tecla);
        // Console.WriteLine(_opcionActual);
        // Console.WriteLine(Convert.ToString(Console.WindowWidth));
        // Console.WriteLine(Convert.ToString(Console.WindowHeight));

        NavegarMenu();
    }

    internal static bool VerificarEmailValido(string email)
    {
        string trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false;
        }

        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }

    internal static void ImprimirRegistro()
    {
        ImprimirTitulo();

        string[] inputs = ["Nombre", "Apellido", "Email", "Contraseña", "Confirmar Contraseña"];
        string[] datos = new string[inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            Console.Write(Centrar($"{inputs[i]}: "));
            datos[i] = Console.ReadLine();
        }

        foreach (string x in datos)
            Console.Write($"{x}| ");

        Thread.Sleep(2000);

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine(ASCII.Creditos);
    }

    /*Pedido de datos*/

    internal static void ImprimirPedido ()
    {
        ImprimirTitulo();

        MarketWeight.salirBucle = true;        
        string[] inputs = [ "Cantidad Oferta", "Cantidad Demanda", "Precio Oferta", "Precio Demanda"];
        decimal[] datos = new decimal[inputs.Length];


        Console.Write(Centrar("Nombre del Activo: "));
        
        string NombreActivo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(NombreActivo))
        {
            Console.Clear();

                ImprimirTitulo();

                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Centrar("Error!! Ingrese un nombre válido"));
                ReiniciarColores();
                Console.WriteLine(Centrar("Presiona cualquier tecla para continuar."));

                Console.ReadKey();
                ImprimirPedido();
        }


        for (int i = 0; i < inputs.Length; i++)
        {
            try
            {
                Console.Write(Centrar($"{inputs[i]}: "));
                datos[i] = decimal.Parse(Console.ReadLine());
            }
            catch(Exception)
            {
                Console.Clear();

                ImprimirTitulo();

                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Centrar("Error!! Ingrese un número válido"));
                ReiniciarColores();
                Console.WriteLine(Centrar("Presiona cualquier tecla para continuar."));

                Console.ReadKey();
                ImprimirPedido();
            }
        } 

        MostrarResultados(NombreActivo, datos[0], datos[1], datos[2], datos[3]);
    }

    internal static void DireccionalidadActivo (decimal cantDemanda, decimal cantOferta)
    {
        char x = Calculo.PropiedadMercado(cantDemanda, cantOferta);

        switch(x)
        {
            case 'a':
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Centrar(ASCII.ActivoBaja));
                ReiniciarColores();
            break;

            case 'b':
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Centrar(ASCII.ActivoAlza));
                ReiniciarColores();
            break;

            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Centrar(ASCII.ActivoEquilibrio));
                ReiniciarColores();
            break;
        }
    }

    internal static void MostrarResultados(string NActivo, decimal cantOferta, decimal cantDemanda, decimal pOferta, decimal pDemanda)
    {
        ImprimirTitulo();

        Calculo.puntoEquilibrio = Calculo.CalcularPuntoEquilibrio(cantOferta, cantDemanda, pOferta, pDemanda);
        Calculo.oferta = Calculo.CalcularOferta(cantOferta, pOferta, Calculo.puntoEquilibrio);
        Calculo.demanda = Calculo.CalcularDemanda(cantDemanda, pDemanda, Calculo.puntoEquilibrio);

        Console.WriteLine(Centrar(""));
        Console.WriteLine(Centrar($"Precio de Demanda: " + pDemanda.ToString("F2") + "     Precio de Oferta: "  + pOferta.ToString("F2")));
        Console.WriteLine("");
        Console.WriteLine(Centrar($"Cantidad de {NActivo}: " + Calculo.demanda.ToString("F2") + "     Precio de Equilibrio: " + Calculo.puntoEquilibrio.ToString("F2")));
        DireccionalidadActivo(cantDemanda, cantOferta);
        
        Console.WriteLine("\n\n");

        Console.WriteLine(Centrar("Presione cualquier tecla para volver al inicio."));

        EscucharTeclado();
        MarketWeight.salirBucle = false;
        MarketWeight.BuclePantallaPrincipal();
    }
}