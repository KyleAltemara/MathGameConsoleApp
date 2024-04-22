
namespace MathGameConsoleApp;

internal class Program
{
    const int GameRounds = 3;

    static readonly Random Random = new();

    static void Main()
    {
        var gameHistory = new List<MathGame>();
        while (true)
        {
            WriteMainMenu();
            var key = Console.ReadKey(intercept: true);
            switch (key.KeyChar)
            {
                case 'v':
                case 'V':
                    if (gameHistory.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine(key.KeyChar);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No games in history.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                    GameHistory(gameHistory);
                    break;
                case 'a':
                case 'A':
                    gameHistory.Add(PlayMathGame(GameType.Addition));
                    Console.Clear();
                    break;
                case 's':
                case 'S':
                    gameHistory.Add(PlayMathGame(GameType.Subtraction));
                    Console.Clear();
                    break;
                case 'm':
                case 'M':
                    gameHistory.Add(PlayMathGame(GameType.Multiplication));
                    Console.Clear();
                    break;
                case 'd':
                case 'D':
                    gameHistory.Add(PlayMathGame(GameType.Division));
                    Console.Clear();
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

            if (gameHistory.Count > 10)
            {
                gameHistory.RemoveAt(0);
            }
        }
    }

    private static void WriteMainMenu()
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

    private static void GameHistory(List<MathGame> gameHistory)
    {
        Console.Clear();
        while (true)
        {
            for (int i = 0; i < gameHistory.Count; i++)
            {
                var game = gameHistory[i];
                var gameType = game.GameType;
                Console.WriteLine($"[{i}] Game {i + 1}: {nameof(gameType)} game, score {game.Score}/{GameRounds}");
            }

            Console.WriteLine("[E]xit to main menu, or enter game id to see more details");
            var key = Console.ReadKey(intercept: true);
            var keyString = key.KeyChar.ToString().ToLower();
            if (keyString == "e")
            {
                Console.Clear();
                return;
            }
            else
            {
                if (int.TryParse(keyString, out int gameId) && gameId < gameHistory.Count)
                {
                    Console.Clear();
                    var game = gameHistory[gameId];
                    var gameType = game.GameType;
                    Console.WriteLine($"[{gameId}] Game {gameId + 1}: {nameof(gameType)} game, score {game.Score}/{GameRounds}");
                    var mathSymbol = GetMathSymbol(gameType);
                    for (int i = 0; i < GameRounds; i++)
                    {
                        (int a, int b) = game.Questions[i];
                        int anwser = game.Answers[i];
                        int correctAnwser = game.CorrectAnswers[i];
                        Console.Write($"{a} {mathSymbol} {b} = ");
                        if (anwser == correctAnwser)
                        {
                            Console.Write($"{anwser}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{anwser}");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"({correctAnwser})");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine("Press any key to go back to the previous menu.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }

    private static MathGame PlayMathGame(GameType gameType)
    {
        var game = new MathGame { GameType = gameType };
        var mathSymbol = GetMathSymbol(gameType);
        int score = 0;
        for (int i = 0; i < GameRounds; i++)
        {
            Console.Clear();
            Console.WriteLine($"Addition Game Round {i + 1} of {GameRounds}");
            int a, b, correctAnswer;
            do
            {
                (a, b, correctAnswer) = GetQuestion(gameType);
            } while (game.CorrectAnswers.Contains(correctAnswer));
            Console.WriteLine($"{a} {mathSymbol} {b} = ?");
            var answer = Console.ReadLine();
            int intAnswer;
            while (answer is null || !int.TryParse(answer, out intAnswer))
            {
                Console.WriteLine("Answer is not a number, please try again.");
                answer = Console.ReadLine();
            }

            game.Questions.Add((a, b));
            game.CorrectAnswers.Add(correctAnswer);
            game.Answers.Add(intAnswer);
            if (intAnswer == correctAnswer)
            {
                Console.Write($"{answer} is correct!");
                score++;
            }
            else
            {
                Console.Write($"{answer} is intcorrect, the correct answer is {correctAnswer}.");
            }

            if (i + 1 < GameRounds)
            {
                Console.WriteLine(" Type any key for the next question");
                Console.ReadKey();
            }
        }

        game.Score = score;
        Console.WriteLine();
        Console.WriteLine($"Game over. Your final score is {score}/{GameRounds}.");
        Console.WriteLine("Press any key to go back to the main menu.");
        Console.ReadKey();
        return game;
    }

    private static string GetMathSymbol(GameType gameType) =>
        gameType switch
        {
            GameType.Addition => "+",
            GameType.Subtraction => "-",
            GameType.Division => "/",
            GameType.Multiplication => "x",
            _ => throw new NotImplementedException(),
        };

    private static (int a, int b, int correctAnswer) GetQuestion(GameType gameType)
    {
        int a, b;
        switch (gameType)
        {
            case GameType.Addition:
                a = Random.Next(1, 101);
                b = Random.Next(1, 101);
                return (a, b, a + b);
            case GameType.Subtraction:
                a = Random.Next(1, 101);
                b = Random.Next(1, a + 1);
                return (a, b, a - b);
            case GameType.Multiplication:
                do
                {
                    a = Random.Next(1, 101);
                    b = Random.Next(1, 101);
                } while (a * b > 1_000);
                return (a, b, a * b);
            case GameType.Division:
                double answer;
                do
                {
                    a = Random.Next(1, 101);
                    b = Random.Next(2, a + 1);
                    answer = (double)a / b;
                } while (answer != Math.Floor(answer));
                return (a, b, (int)answer);
            default:
                throw new NotImplementedException();
        }
    }
}
