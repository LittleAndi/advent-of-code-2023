using System.Diagnostics.Contracts;

var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var almanac = new Almanac(lines);
System.Console.WriteLine(almanac.SeedLocations.Min());

public class Almanac
{
    public enum PlantFactorMapping
    {
        FertilizerToWaterMap,
        HumidityToLocationMap,
        LightToTemperatureMap,
        SeedToSoilMap,
        SoilToFertilizerMap,
        TemperatureToHumidityMap,
        WaterToLightMap
    }

    private readonly long[] seeds = [];
    private readonly List<KeyValuePair<string, MappingLine>> mapping = [];

    public Almanac(string[] input)
    {
        var pos = 0;

        // seeds
        seeds = input[pos++].Remove(0, 7).Split(' ').Select(seed => Convert.ToInt64(seed)).ToArray();

        var currentMap = "";
        while (pos < input.Length)
        {
            if (string.IsNullOrWhiteSpace(input[pos]))
            {
                pos++;
                continue;
            }

            if (char.IsLetter(input[pos][0]))
            {
                currentMap = input[pos];
                pos++;
                continue;
            }

            long sourceCategoryStart = Convert.ToInt64(input[pos].Split(' ')[0]);
            long destinationCategoryStart = Convert.ToInt64(input[pos].Split(' ')[1]);
            long length = Convert.ToInt64(input[pos].Split(' ')[2]);

            mapping.Add(new KeyValuePair<string, MappingLine>(currentMap, new MappingLine(sourceCategoryStart, destinationCategoryStart, length)));

            pos++;
        }


    }

    // Used for testing the input mapping
    public long CategoryMapLineCount(string categoryMap) => mapping.Where(m => m.Key.Equals(categoryMap)).Count();

    public long[] SeedLocations
    {
        get
        {
            List<long> seedLocations = [];
            foreach (var seed in seeds)
            {
                long currentPosition = seed;

                // seed-to-soil map:
                currentPosition = GetDestination("seed-to-soil map:", currentPosition);

                // soil-to-fertilizer map:
                currentPosition = GetDestination("soil-to-fertilizer map:", currentPosition);

                // fertilizer-to-water map:
                currentPosition = GetDestination("fertilizer-to-water map:", currentPosition);

                // water-to-light map:
                currentPosition = GetDestination("water-to-light map:", currentPosition);

                // light-to-temperature map:
                currentPosition = GetDestination("light-to-temperature map:", currentPosition);

                // temperature-to-humidity map:
                currentPosition = GetDestination("temperature-to-humidity map:", currentPosition);

                // humidity-to-location map:
                currentPosition = GetDestination("humidity-to-location map:", currentPosition);

                seedLocations.Add(currentPosition);
            }
            return [.. seedLocations];
        }
    }

    private long GetDestination(string mapName, long source)
    {
        long destination = source;
        var map = mapping.Where(m => m.Key.Equals(mapName)).FirstOrDefault(m => m.Value.IsMappedWithThisLine(source)).Value;
        if (map != null) destination = map.Destination(source);
        return destination;
    }
}

public record MappingLine(long DestinationCategoryStart, long SourceCategoryStart, long Length)
{
    public bool IsMappedWithThisLine(long source) => source >= SourceCategoryStart && source < SourceCategoryStart + Length;
    public long Destination(long source) => IsMappedWithThisLine(source) ? DestinationCategoryStart + (source - SourceCategoryStart) : -1;
};