
namespace MathGameConsoleApp;

internal class Program
{
    const int GameRounds = 5;

    static Random Random = new Random();

    static void Main()
    {
        while (true)
        {
            WriteMainMenu();
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'v':
                case 'V':
                    break;
                case 'a':
                case 'A':
                    AdditionGame();
                    Console.Clear();
                    break;
                case 's':
                case 'S':
                    break;
                case 'm':
                case 'M':
                    break;
                case 'd':
                case 'D':
                    break;
                case 'q':
                case 'Q':
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine(key.KeyChar);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
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

    private static void AdditionGame()
    {
        int score = 0;
        for (int i = 0; i < GameRounds; i++)
        {
            Console.Clear();
            Console.WriteLine($"Addition Game Round {i + 1} of {GameRounds}");
            int a = Random.Next(1, 101);
            int b = Random.Next(1, 101);
            Console.WriteLine($"{a} + {b} = ?");
            var answer = Console.ReadLine();
            int intAnswer;
            while (answer is null || !int.TryParse(answer, out intAnswer))
            {
                Console.WriteLine("Answer is not a number, please try again.");
            }

            if (intAnswer == a + b)
            {
                Console.Write($"{answer} is correct!");
                score++;
            }
            else
            {
                Console.Write($"{answer} is intcorrect, the correct answer is {a + b}.");
            }

            if (i + 1 < GameRounds)
            {
                Console.WriteLine(" Type any key for the next question");
                Console.ReadKey();
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Game over. Your final score is {score}/{GameRounds}.");
        Console.WriteLine("Press any key to go back to the main menu.");
        Console.ReadKey();
    }
}
