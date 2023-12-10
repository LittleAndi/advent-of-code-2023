using Shouldly;
using Xunit.Sdk;

namespace day10tests;

[Trait("Day", "10")]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        string[] lines = [
            "..F7.",
            ".FJ|.",
            "SJ.L7",
            "|F--J",
            "LJ...",
        ];
        var maze = new PipeMaze(lines);
        maze.PositionS.ShouldBe((2, 0));
        maze.TotalLapSteps.ShouldBe(16);
    }
}