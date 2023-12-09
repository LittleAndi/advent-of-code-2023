var handsWithoutJoker = File.ReadAllLines("input.txt")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(handInput => new Hand(handInput));

var camelCardsGame = new CamelCardsGame(handsWithoutJoker);
System.Console.WriteLine($"Part 1: Total winnings are {camelCardsGame.TotalWinnings}");


var handsWithJoker = File.ReadAllLines("input.txt")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(handInput => new Hand(handInput, true));

var camelCardsGameWithJokers = new CamelCardsGame(handsWithJoker);
System.Console.WriteLine($"Part 2: Total winnings are {camelCardsGameWithJokers.TotalWinnings}");


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