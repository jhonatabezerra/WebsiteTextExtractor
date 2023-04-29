using Generator;
using Generator.Domain;
using Generator.Providers;

internal class Program : ConsoleCommands
{
    private static WebConfiguration _web;
    private static FileConfiguration _file;

    private static void Main()
    {
        Console.WriteLine("Iniciando aplicação!");
        MainProvider mainProvider = new();
        GetInformation();
        mainProvider.Execute(_web, _file);
        Console.WriteLine("Finalizando aplicação!");
    }

    private static void GetInformation()
    {
        var isDefaultValue = CommandInputBool("Would you like to use the default value (Y/N): ");
        if (isDefaultValue)
        {
            DefaultInformation();
            return;
        }

        var booksName = CommandInputString("Input the Book's name: ");
        var language = CommandInputString("Input the language: ");
        var sitePath = CommandInputString("Input the website path (Without chapter number. Example: https://boxnovel.com/novel/release-that-witch/chapter-): ");
        var path = CommandInputString("Input the path to save the file(s): ");
        var xPath = CommandInputString("Input the xPath of website: ");
        var startChapter = CommandInputUint("Input the first chapter number: ");
        var endChapter = CommandInputUint("Input the last chapter number: ");

        _web = new(sitePath, xPath);
        _file = new(booksName, language, path, startChapter, endChapter);
    }

    private static void DefaultInformation()
    {
        uint startChapter = 1;
        uint endChapter = 10;
        var language = "EN";
        var bookName = "Release that Witch";
        var path = @"C:\Users\Jhonata\Documents";

        var url = $"https://boxnovel.com/novel/release-that-witch/chapter-";
        var xPath = "//div[contains(@class, 'text-left')]";

        _web = new(url, xPath);
        _file = new(bookName, language, path, startChapter, endChapter);
    }
}