namespace Generator;

public class ConsoleCommands
{
    public static string CommandInputString(string message)
    {
        Console.Write(message);
        return ReadCommand(message);
    }

    public static uint CommandInputUint(string message)
    {
        Console.Write(message);
        return Convert.ToUInt32(Console.ReadLine());
    }

    public static bool CommandInputBool(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();
        if (IsNullOrEmpty(input)) WriteInvalidInput(message);
        if (input!.ToUpper() == "Y") return true;
        return false;
    }

    private static string ReadCommand(string message)
    {
        var input = Console.ReadLine();

        do
        {
            if (IsNullOrEmpty(input)) WriteInvalidInput(message);
            break;
        } while (true);

        return input ?? string.Empty;
    }

    private static bool IsNullOrEmpty(string? value) => string.IsNullOrEmpty(value);

    private static void WriteInvalidInput(string message)
    {
        Console.WriteLine("Invalid input.");
        Console.Write(message);
    }
}