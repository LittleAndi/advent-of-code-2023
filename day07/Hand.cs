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
    };
    public string Cards { get; }
    public int Bid { get; }
    public Hand(string handInput)
    {
        Cards = handInput[0..5];
        Bid = Convert.ToInt32(handInput[6..]);
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
            if (Cards[i] == other.Cards[i]) continue;
            return CardValues[Cards[i]].CompareTo(CardValues[other.Cards[i]]);
        }

        // What to return here?
        return 0;
    }
}