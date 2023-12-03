using System.Runtime.InteropServices;

namespace day03tests;

public class UnitTest1
{
    [Fact]
    public void ShouldFindAPartNumber()
    {
        string[] schematics =
        [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.*",
            ".........1",
        ];
        var engineSchematic = new EngineSchematic(schematics.Select(l => l.ToCharArray()).ToArray());
        engineSchematic.PartNumbers.ShouldBe([467, 35, 633, 617, 592, 755, 664, 598, 1]);
    }


    [Theory]
    [InlineData(0, 0, 467, 0, 2)]
    [InlineData(0, 1, 467, 0, 2)]
    [InlineData(0, 2, 467, 0, 2)]
    [InlineData(0, 3, 0, 3, 3)]
    [InlineData(7, 6, 755, 6, 8)]
    public void ShouldFindFullPartNumber(int y, int x, int expectedPartNumber, int expectedStart, int expectedEnd)
    {
        string[] schematics =
        [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.*",
            ".........1",
        ];
        var engineSchematic = new EngineSchematic(schematics.Select(l => l.ToCharArray()).ToArray());
        (int fullPartNumber, int start, int end) = engineSchematic.FindFullPartNumber(y, x);
        fullPartNumber.ShouldBe(expectedPartNumber);
        start.ShouldBe(expectedStart);
        end.ShouldBe(expectedEnd);
    }

    [Fact]
    public void ShouldFindGearRatios()
    {
        string[] schematics =
        [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
            "...123....",
            "....*123..",
            "..........",
        ];
        var engineSchematic = new EngineSchematic(schematics.Select(l => l.ToCharArray()).ToArray());
        engineSchematic.GearRatios.ShouldBe([16345, 451490, 123 * 123]);
        engineSchematic.GearRatios.Sum().ShouldBe(467835 + 123 * 123);
    }
}