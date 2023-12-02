namespace day02tests;

public class UnitTest1
{
    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1, 4 + 1, 2 + 2, 3 + 6, true)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2, 1, 2 + 3 + 1, 1 + 4 + 1, true)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 3, 20 + 4 + 1, 8 + 13 + 5, 6 + 5, false)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 4, 3 + 6 + 14, 1 + 3 + 3, 6 + 15, false)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5, 6 + 1, 3 + 2, 1 + 2, true)]
    [InlineData("Game 99: 2 red, 2 green; 1 red, 1 green, 2 blue; 3 blue, 3 red, 3 green; 1 blue, 3 green, 7 red; 5 red, 3 green, 1 blue", 99, 2 + 1 + 3 + 7 + 5, 2 + 1 + 3 + 3 + 3, 2 + 3 + 1 + 1, true)]

    public void Test1(string input, int gameId, int reds, int greens, int blues, bool validGame)
    {
        var game = new Game(input, 12, 13, 14);
        game.Id.ShouldBe(gameId);
        game.Reds.ShouldBe(reds);
        game.Greens.ShouldBe(greens);
        game.Blues.ShouldBe(blues);
        game.IsValidGame.ShouldBe(validGame);
    }
}