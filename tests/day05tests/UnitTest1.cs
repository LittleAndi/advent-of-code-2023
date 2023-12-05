namespace day05tests;

[Trait("Day", "5")]
public class UnitTest1
{
    [Fact]
    public void ShouldCreateAlmanacAndMapSeedLocations()
    {
        var lines = new string[]
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4",
        };

        var almanac = new Almanac(lines);
        almanac.ShouldNotBeNull();
        almanac.CategoryMapLineCount("seed-to-soil map:").ShouldBe(2);
        almanac.CategoryMapLineCount("soil-to-fertilizer map:").ShouldBe(3);
        almanac.CategoryMapLineCount("fertilizer-to-water map:").ShouldBe(4);
        almanac.CategoryMapLineCount("water-to-light map:").ShouldBe(2);
        almanac.CategoryMapLineCount("light-to-temperature map:").ShouldBe(3);
        almanac.CategoryMapLineCount("temperature-to-humidity map:").ShouldBe(2);
        almanac.CategoryMapLineCount("humidity-to-location map:").ShouldBe(2);
        almanac.SeedLocations.ShouldBe([82, 43, 86, 35]);
    }

    [Fact]
    public void ShouldMap()
    {
        var mappingLine = new MappingLine(52, 50, 48);
        mappingLine.IsMappedWithThisLine(49).ShouldBeFalse();
        mappingLine.IsMappedWithThisLine(50).ShouldBeTrue();
        mappingLine.IsMappedWithThisLine(51).ShouldBeTrue();
        mappingLine.IsMappedWithThisLine(52).ShouldBeTrue();

        mappingLine.Destination(49).ShouldBe(-1);
        mappingLine.Destination(50).ShouldBe(52);
        mappingLine.Destination(51).ShouldBe(53);
        mappingLine.Destination(52).ShouldBe(54);
        mappingLine.Destination(79).ShouldBe(81);
    }
}