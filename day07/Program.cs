var hands = File.ReadAllLines("input.txt")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(handInput => new Hand(handInput));

var camelCardsGame = new CamelCardsGame(hands);
System.Console.WriteLine($"Part 1: Total winnings are {camelCardsGame.TotalWinnings}");

public class CamelCardsGame(IEnumerable<Hand> hands)
{
    public IEnumerable<Hand> Hands { get; } = hands;
    public int TotalWinnings
    {
        get
        {
            var rankedHands = Hands.Order();
            return rankedHands.Select((hand, index) => hand.Bid * (index + 1)).Sum();
        }
    }
}