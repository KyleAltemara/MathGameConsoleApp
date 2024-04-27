namespace MathGameConsoleApp;

internal class MathGame
{
    public GameType GameType { get; set; }
    public List<string> Questions { get; set; } = [];
    public List<int> Answers { get; set; } = [];
    public List<int> CorrectAnswers { get; set; } = [];
    public int Score { get; set; }
}