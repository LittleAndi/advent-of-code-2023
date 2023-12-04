using System.Data.Common;

var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

// Part 1
System.Console.WriteLine(lines.Select(l => new Card(l)).Sum(card => card.Points));

// Part 2
var cards = lines.Select(l => new Card(l)).Select(c => (c.Id, Card: c, Instances: 1)).ToArray();

for (int i = 0; i < cards.Length; i++)
{
    for (int instances = 0; instances < cards[i].Instances; instances++)
    {
        for (int j = i + 1; j < cards[i].Card.MatchingNumbers + i + 1; j++)
        {
            cards[j].Instances++;
        }
    }
}

System.Console.WriteLine(cards.Sum(c => c.Instances));

public class Card
{
    private readonly int id;
    private readonly HashSet<int> winningNumbers = [];
    private readonly HashSet<int> numbersYouHave = [];

    public Card(string input)
    {
        id = Convert.ToInt32(input.Split(':')[0].Replace("Card ", "").Trim());
        winningNumbers = input.Split(':')[1].Split('|')[0].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n)).ToHashSet();
        numbersYouHave = input.Split(':')[1].Split('|')[1].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => Convert.ToInt32(n)).ToHashSet();
    }

    public int Id => id;
    public int Points
    {
        get
        {
            return (int)Math.Pow(2, winningNumbers.Intersect(numbersYouHave).Count() - 1);
        }
    }

    public int MatchingNumbers
    {
        get
        {
            return winningNumbers.Intersect(numbersYouHave).Count();
        }
    }
}