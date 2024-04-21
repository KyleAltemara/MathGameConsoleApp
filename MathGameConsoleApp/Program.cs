namespace MathGameConsoleApp;

internal class Program
{
    static void Main()
    {
        while (!MainMenu()) 
        {
            Console.WriteLine();
        }
    }

    public static bool MainMenu()
    {
        WriteMainMenu();
        var readingKey = true;
        while (readingKey)
        {
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'v':
                case 'V':
                    readingKey = false;
                    Console.WriteLine(key.ToString());
                    break;
                case 'a':
                case 'A':
                    readingKey = false;
                    Console.WriteLine(key.ToString());
                    break;
                case 's':
                case 'S':
                    readingKey = false;
                    Console.WriteLine(key.ToString());
                    break;
                case 'm':
                case 'M':
                    readingKey = false;
                    Console.WriteLine(key.ToString());
                    break;
                case 'd':
                case 'D':
                    readingKey = false;
                    Console.WriteLine(key.ToString());
                    break;
                case 'q':
                case 'Q':
                    Console.WriteLine(key.ToString());
                    return true;
                default:
                    Console.Clear();
                    Console.WriteLine(key.KeyChar);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteMainMenu();
                    break;
            }
        }

        return false;
    }

    public static void WriteMainMenu()
    {
        Console.WriteLine("What game would you like to play today? Choose from the options below:");
        Console.WriteLine("V - View Previous Games");
        Console.WriteLine("A - Addition");
        Console.WriteLine("S - Subraction");
        Console.WriteLine("M - Multiplication");
        Console.WriteLine("D - Division");
        Console.WriteLine("Q - Quit the program");
        Console.WriteLine("-------------------------------------------");
    }
}
