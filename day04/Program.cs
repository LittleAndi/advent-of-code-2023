var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var scratchcards = new Scratchcards(lines);

// Part 1
System.Console.WriteLine($"Part 1: Total sum of points are {scratchcards.TotalPoints}");

// Part 2
System.Console.WriteLine($"Part 1: Total sum of instances are {scratchcards.TotalInstances}");

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

public class Scratchcards
{
    private (int Id, Card Card, int Instances)[] cards = [];
    public Scratchcards(string[] cardInputs)
    {
        cards = cardInputs.Select(l => new Card(l)).Select(c => (c.Id, Card: c, Instances: 1)).ToArray();

        UpdateInstances();
    }

    private void UpdateInstances()
    {
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
    }

    public int TotalPoints => cards.Sum(cardInstance => cardInstance.Card.Points);

    public int TotalInstances => cards.Sum(card => card.Instances);
}