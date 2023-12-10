namespace day09tests;

[Trait("Day", "9")]
public class UnitTest1
{
    [Theory]
    [InlineData("0 3 6 9 12 15", 18)]
    [InlineData("1 3 6 10 15 21", 28)]
    [InlineData("10 13 16 21 30 45", 68)]
    public void ShouldCalculateNextHistoryValue(string input, int expectedHistoryValue)
    {
        var t = new Thingy(input);
        t.NextHistoryValue.ShouldBe(expectedHistoryValue);
    }

    [Theory]
    [InlineData("0 3 6 9 12 15", -3)]
    [InlineData("1 3 6 10 15 21", 0)]
    [InlineData("10 13 16 21 30 45", 5)]
    public void ShouldCalculatePreviousHistoryValue(string input, int expectedHistoryValue)
    {
        var t = new Thingy(input);
        t.PreviousHistoryValue.ShouldBe(expectedHistoryValue);
    }

}