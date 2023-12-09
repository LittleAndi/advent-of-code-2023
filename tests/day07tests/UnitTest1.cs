using System.Runtime.InteropServices;

namespace day07tests;

[Trait("Day", "7")]
public class UnitTest1
{
    [Theory]
    [InlineData("32T3K 765", "32T3K", 765, HandType.OnePair)]
    [InlineData("T55J5 684", "T55J5", 684, HandType.ThreeOfAKind)]
    [InlineData("KK677 28", "KK677", 28, HandType.TwoPair)]
    [InlineData("KTJJT 220", "KTJJT", 220, HandType.TwoPair)]
    [InlineData("QQQJA 483", "QQQJA", 483, HandType.ThreeOfAKind)]
    [InlineData("AA8AA 1", "AA8AA", 1, HandType.FourOfAKind)]
    [InlineData("AAAAA 1", "AAAAA", 1, HandType.FiveOfAKind)]
    [InlineData("23323 1", "23323", 1, HandType.FullHouse)]
    [InlineData("23456 1", "23456", 1, HandType.HighCard)]

    public void ShouldReturnHandType(
        string handInput,
        string cards,
        int bid,
        HandType handType
    )
    {
        var hand = new Hand(handInput);
        hand.Cards.ShouldBe(cards);
        hand.Bid.ShouldBe(bid);
        hand.Type.ShouldBe(handType);
    }
    [Theory]
    [InlineData("AAAAA 0", "KKKKK 0", true)]
    [InlineData("KKKKK 0", "KKKKK 0", false)]
    [InlineData("KKKKK 0", "KK8KK 0", true)]
    [InlineData("32T3K 0", "KK677 0", false)]
    public void ShouldCompareHands(string leftHandInput, string rightHandInput, bool leftHandIsBetter)
    {
        var left = new Hand(leftHandInput);
        var right = new Hand(rightHandInput);
        (left.CompareTo(right) > 0).ShouldBe(leftHandIsBetter);
    }

    [Fact]
    public void ShouldReturnTotalWinnings()
    {
        var handsInput = new string[] {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483",
        };

        var hands = handsInput.Select(i => new Hand(i));

        var camelCardsGame = new CamelCardsGame(hands);
        camelCardsGame.TotalWinnings.ShouldBe(6440);
    }
}