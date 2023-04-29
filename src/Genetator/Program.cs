using Generator.Providers;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Iniciando aplicação!");
        MainProvider mainProvider = new();
        mainProvider.Execute();
        Console.WriteLine("Finalizando aplicação!");
    }
}