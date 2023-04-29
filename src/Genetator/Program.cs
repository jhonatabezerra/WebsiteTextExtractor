using ConsoleApp1.Providers;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Iniciando aplicação!");

        MainProvider mainProvider = new();
        //mainProvider.Execute();
        mainProvider.ExecuteAll(1, 10);
        Console.WriteLine("Finalizando aplicação!");
    }
}