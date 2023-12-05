var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var almanac = new Almanac(lines, false);
System.Console.WriteLine(almanac.SeedLocations.Min());

var almanac2 = new Almanac(lines, true);
System.Console.WriteLine(almanac2.LowestSeedLocation);

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
    private readonly bool useRange;

    public Almanac(string[] input, bool useRange = false)
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

        this.useRange = useRange;
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

    public long LowestSeedLocation
    {
        get
        {
            long location = 0;
            System.Console.WriteLine(location);

            while (true)
            {
                if (location % 100000 == 0) System.Console.WriteLine(location);

                long currentPosition = location;

                // humidity-to-location map:
                currentPosition = GetSource("humidity-to-location map:", currentPosition);
                // temperature-to-humidity map:
                currentPosition = GetSource("temperature-to-humidity map:", currentPosition);
                // light-to-temperature map:
                currentPosition = GetSource("light-to-temperature map:", currentPosition);
                // water-to-light map:
                currentPosition = GetSource("water-to-light map:", currentPosition);
                // fertilizer-to-water map:
                currentPosition = GetSource("fertilizer-to-water map:", currentPosition);
                // soil-to-fertilizer map:
                currentPosition = GetSource("soil-to-fertilizer map:", currentPosition);
                // seed-to-soil map:
                currentPosition = GetSource("seed-to-soil map:", currentPosition);

                // check result
                if (IsASeedLocation(currentPosition)) break;

                location++;
            }
            return location;
        }
    }

    private bool IsASeedLocation(long currentPosition)
    {
        if (!useRange) return seeds.Contains(currentPosition);

        // Think of the seed info as ranges
        for (int i = 0; i < seeds.Length; i += 2)
        {
            if (currentPosition > seeds[i] && currentPosition < seeds[i] + seeds[i + 1]) return true;
        }

        return false;
    }

    private long GetDestination(string mapName, long source)
    {
        long destination = source;
        var map = mapping.Where(m => m.Key.Equals(mapName)).FirstOrDefault(m => m.Value.IsSourceMappedWithThisLine(source)).Value;
        if (map != null) destination = map.Destination(source);
        return destination;
    }

    private long GetSource(string mapName, long destination)
    {
        long source = destination;
        var map = mapping.Where(m => m.Key.Equals(mapName)).FirstOrDefault(m => m.Value.IsDestinationMappedWithThisLine(destination)).Value;
        if (map != null) source = map.Source(destination);
        return source;
    }
}

public record MappingLine(long DestinationCategoryStart, long SourceCategoryStart, long Length)
{
    public bool IsSourceMappedWithThisLine(long source) => source >= SourceCategoryStart && source < SourceCategoryStart + Length;
    public bool IsDestinationMappedWithThisLine(long destination) => destination >= DestinationCategoryStart && destination < DestinationCategoryStart + Length;
    public long Destination(long source) => IsSourceMappedWithThisLine(source) ? DestinationCategoryStart + (source - SourceCategoryStart) : -1;
    public long Source(long destination) => IsDestinationMappedWithThisLine(destination) ? SourceCategoryStart + (destination - DestinationCategoryStart) : -1;
};