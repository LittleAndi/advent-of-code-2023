var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

long winProductSum = 1;

var races = lines[0].Replace("Time:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => Convert.ToInt32(x)).ToArray();
var recordDistances = lines[1].Replace("Distance:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => Convert.ToInt32(x)).ToArray();

for (long i = 0; i < races.Length; i++)
{
    winProductSum *= GetWins(races[i], recordDistances[i]);
}

System.Console.WriteLine($"Part 1: {winProductSum}");

var race2 = Convert.ToInt64(string.Join("", lines[0].Replace("Time:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)));
var recordDistance2 = Convert.ToInt64(string.Join("", lines[1].Replace("Distance:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)));

var wins2 = GetWins(race2, recordDistance2);

System.Console.WriteLine($"Part 2: {wins2}");

long GetWins(long raceTime, long recordDistance)
{
    long wins = 0;
    for (long buttonTime = 0; buttonTime < raceTime; buttonTime++)
    {
        var testRaceDistance = buttonTime * (raceTime - buttonTime);
        if (testRaceDistance > recordDistance) wins++;
    }
    return wins;
}
