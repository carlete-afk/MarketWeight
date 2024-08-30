class Calculo
{
    internal static decimal puntoEquilibrio = 0;
    internal static decimal oferta = 0;
    internal static decimal demanda = 0;

    internal static decimal CalcularPuntoEquilibrio(decimal cantOferta, decimal cantDemanda, decimal pOferta, decimal pDemanda)
    {
        if ((pOferta + pDemanda) > 0)
            return (cantDemanda - cantOferta) / (pOferta + pDemanda);

        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(Menu.Centrar("\n\n\nError en los datos! El precio de oferta y demanda tienen que ser mayores a 0."));
            Console.WriteLine(Menu.Centrar("Presiona cualquier tecla para continuar\n\n\n"));
            Console.ReadKey(true);

            MarketWeight.salirBucle = false;
            MarketWeight.BuclePantallaPrincipal();
            return 0;
        }

        return 0;
    }

    // Método para calcular oferta
    internal static decimal CalcularOferta(decimal cantOferta, decimal pOferta, decimal puntoEquilibrio)
    {
        return cantOferta + (pOferta * puntoEquilibrio);
    }

    // Método para calcular demanda
    internal static decimal CalcularDemanda(decimal cantDemanda, decimal pDemanda, decimal puntoEquilibrio)
    {
        return cantDemanda - (pDemanda * puntoEquilibrio);
    }

    internal static char PropiedadMercado (decimal cantDemanda, decimal cantOferta)
    {
        if(cantDemanda > cantOferta)
            return 'a';

        else if(cantDemanda < cantOferta)
            return 'b';

        else
            return 'e';
    }
}