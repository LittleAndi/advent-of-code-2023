using System.Buffers;
using System.Text.RegularExpressions;

public enum HandType
{
    HighCard,         // 23456
    OnePair,          // A23A4
    TwoPair,          // 23432
    ThreeOfAKind,     // TTT98
    FullHouse,        // 23332
    FourOfAKind,      // AA8AA
    FiveOfAKind       // AAAAA
}

public partial class Hand : IComparable<Hand>
{
    private Dictionary<char, int> CardValues = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { 'j', 1 },
    };
    private readonly SearchValues<char> jokers = SearchValues.Create(['J', 'j']);
    public string OriginalCards { get; }
    public string Cards { get; }
    public int Bid { get; }
    public Hand(string handInput, bool useJokers = false)
    {
        OriginalCards = handInput[0..5];
        if (useJokers) OriginalCards = OriginalCards.Replace('J', 'j');  // To have another value for this card

        Cards = FindBestHand(handInput[0..5], useJokers);

        // A fix to be able to reuse this class for joker tests
        if (handInput.Length > 6)
            Bid = Convert.ToInt32(handInput[6..]);
        else
            Bid = 0;
    }

    private string FindBestHand(string handInput, bool useJokers)
    {
        if (!useJokers) return handInput;

        var currentBestHand = new Hand(handInput.Replace('J', '2'), false); // Start with the lowest variant
        foreach (var replacementCard in CardValues.Where(v => !jokers.Contains(v.Key) && v.Value > 2).OrderBy(v => v.Value))
        {
            var testHand = new Hand(handInput.Replace('J', replacementCard.Key));
            if (testHand.CompareTo(currentBestHand) > 0) currentBestHand = testHand;
        }

        return currentBestHand.Cards;
    }

    public HandType Type
    {
        get
        {
            if (IsFiveOfAKind) return HandType.FiveOfAKind;
            if (IsFourOfAKind) return HandType.FourOfAKind;
            if (IsFullHouse) return HandType.FullHouse;
            if (IsThreeOfAKind) return HandType.ThreeOfAKind;
            if (IsTwoPairs) return HandType.TwoPair;
            if (IsOnePair) return HandType.OnePair;
            return HandType.HighCard;
        }
    }

    private bool IsFiveOfAKind
    {
        get
        {
            Regex regex = FiveOfAKindRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }
    private bool IsFourOfAKind
    {
        get
        {
            Regex regex = FourOfAKindRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }
    private bool IsFullHouse
    {
        get
        {
            Regex regex = FullHouseRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }
    private bool IsThreeOfAKind
    {
        get
        {
            Regex regex = ThreeOfAKindRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }
    private bool IsTwoPairs
    {
        get
        {
            Regex regex = TwoPairRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }
    private bool IsOnePair
    {
        get
        {
            Regex regex = OnePairRegex();
            return regex.IsMatch(string.Join("", Cards.Order().ToArray()));
        }
    }

    [GeneratedRegex(@"(.)\1{4}")]
    private static partial Regex FiveOfAKindRegex();
    [GeneratedRegex(@"((.)\2{1}(.)\3{2})|((.)\5{2}(.)\6{1})")]
    private static partial Regex FullHouseRegex();
    [GeneratedRegex(@"(.)\1{3}")]
    private static partial Regex FourOfAKindRegex();
    [GeneratedRegex(@"(.)\1{2}")]
    private static partial Regex ThreeOfAKindRegex();
    [GeneratedRegex(@"(.)\1{1}.?(.)\2")]
    private static partial Regex TwoPairRegex();
    [GeneratedRegex(@"(.)\1{1}")]
    private static partial Regex OnePairRegex();

    public int CompareTo(Hand? other)
    {
        if (Type != other!.Type) return Type.CompareTo(other!.Type);

        for (int i = 0; i < 5; i++)
        {
            if (OriginalCards[i] == other.OriginalCards[i]) continue;
            return CardValues[OriginalCards[i]].CompareTo(CardValues[other.OriginalCards[i]]);
        }

        // What to return here?
        return 0;
    }
}