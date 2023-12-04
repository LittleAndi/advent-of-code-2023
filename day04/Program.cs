var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

System.Console.WriteLine(lines.Select(l => new Card(l)).Sum(card => card.Points));


public class Card
{
    private readonly HashSet<int> winningNumbers = [];
    private readonly HashSet<int> numbersYouHave = [];

    public Card(string input)
    {
        winningNumbers = input.Split(':')[1].Split('|')[0].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n)).ToHashSet();
        numbersYouHave = input.Split(':')[1].Split('|')[1].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n)).ToHashSet();
    }

    public int Points
    {
        get
        {
            return (int)Math.Pow(2, winningNumbers.Intersect(numbersYouHave).Count() - 1);
        }
    }
}