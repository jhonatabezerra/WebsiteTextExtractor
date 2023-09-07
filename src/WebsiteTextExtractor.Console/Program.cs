using Generator;
using WebsiteTextExtractor.Core;
using WebsiteTextExtractor.Core.Domain.Models;

internal class Program : ConsoleCommands
{
    private static WebConfiguration _web;
    private static FileConfiguration _file;

    private static void Main()
    {
        Console.WriteLine("Starting process!");
        //GetInformation();
        DefaultInformation();

        Startup startup = new();
        startup.Run(_web, _file);

        // Chame o método Execute
        Console.WriteLine("Finished process!");
    }

    private static void GetInformation()
    {
        var isDefaultValue = CommandInputBool("Would you like to use an example with default chapter (Y/N): ");
        if (isDefaultValue)
        {
            DefaultInformation2();
            return;
        }

        var isBigBook = CommandInputBool("Would you like to use an example with default books (Y/N): ");
        if (isBigBook)
        {
            DefaultInformation3();
            return;
        }

        var xPathTitle = string.Empty;
        var booksName = CommandInputString("Input the Book's name: ");
        var language = CommandInputString("Input the language: ");
        var sitePath = CommandInputString("Input the website path (Without chapter number. Example: https://boxnovel.com/novel/release-that-witch/chapter-): ");
        var path = CommandInputString("Input the path to save the file(s): ");
        var xPath = CommandInputString("Input the xPath of website: ");
        var startChapter = CommandInputUint("Input the first chapter number: ");
        var endChapter = CommandInputUint("Input the last chapter number: ");
        var hasTitle = CommandInputBool("Would you like to get the title too?(Y/N): ");
        if (hasTitle) xPathTitle = CommandInputString("Input the xPath of title: ");

        _web = new(sitePath, xPath, xPathTitle, hasTitle);
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

    private static void DefaultInformation2()
    {
        uint startChapter = 1;
        uint endChapter = 100;
        var language = "EN";
        var bookName = "The Cursed Prince";
        var path = @"C:\Users\Jhonata\Documents";

        var url = $"https://novelbin.net/n/the-cursed-prince/chapter-";
        var xPathTitle = "//*[@id=\"chapter\"]/div/div/h2/a";
        var xPathText = "//*[@id=\"chr-content\"]";

        _web = new(url, xPathText, xPathTitle, true);
        _file = new(bookName, language, path, startChapter, endChapter);
    }

    private static void DefaultInformation3()
    {
        uint startChapter = 139;
        uint endChapter = 400;
        var language = "EN";
        var bookName = "The Cursed Prince";
        var path = @"C:\Users\Jhonata\Documents";

        var url = $"https://novelbin.net/n/the-cursed-prince/chapter-";
        var xPathTitle = "//*[@id=\"chapter\"]/div/div/h2/a";
        var xPathText = "//*[@id=\"chr-content\"]";
        List<string> tagsToFix = new() { "62e886631a93af4356fc7a46", ">>>>>>\\s" };

        _web = new(url, xPathText, xPathTitle, true, tagsToFix);
        _file = new(bookName, language, path, startChapter, endChapter, null, null, true);
    }
}