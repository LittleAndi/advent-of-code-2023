using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var sumOfGameIds = GetSumOfGameIds(lines);
System.Console.WriteLine($"Sum of valid game ids: {sumOfGameIds}");

int GetSumOfGameIds(string[] lines)
{
    var sumOfGameIds = 0;
    foreach (var line in lines)
    {
        var game = new Game(line, 12, 13, 14);
        if (game.IsValidGame)
        {
            System.Console.WriteLine($"Game {game.Id} is a valid game; {game.Reds} {game.Greens} {game.Blues}");
            sumOfGameIds += game.Id;
        }
        else
        {
            System.Console.WriteLine($"Game {game.Id} is not valid game; {game.Reds} {game.Greens} {game.Blues}");
        }

    }
    return sumOfGameIds;
}

public class Game
{
    private readonly int gameId;
    private readonly List<Dictionary<string, int>> sets = [];
    private readonly string input;
    private readonly int bagReds;
    private readonly int bagGreens;
    private readonly int bagBlues;

    public Game(string input, int bagReds, int bagGreens, int bagBlues)
    {
        var gameInfo = input.Split(':')[0].Remove(0, 5);
        gameId = Convert.ToInt32(gameInfo);

        var setsInfo = input.Split(':')[1].Trim()
                        .Split(';')
                        .Select(set => set.Trim());

        foreach (var setInfo in setsInfo)
        {
            var set = new Dictionary<string, int>();

            var cubes = setInfo.Split(',').Select(cube => cube.Trim());

            foreach (var cube in cubes)
            {
                var count = Convert.ToInt32(cube.Split(' ')[0]);
                var color = cube.Split(' ')[1];
                if (!set.TryAdd(color, count)) set[color] += count;
            }

            sets.Add(set);
        }

        this.input = input;
        this.bagReds = bagReds;
        this.bagGreens = bagGreens;
        this.bagBlues = bagBlues;
    }

    public int Id => gameId;

    public int Reds => sets.Where(s => s.ContainsKey("red")).Sum(s => s["red"]);
    public int Greens => sets.Where(s => s.ContainsKey("green")).Sum(s => s["green"]);
    public int Blues => sets.Where(s => s.ContainsKey("blue")).Sum(s => s["blue"]);
    public bool IsValidGame
    {
        get
        {
            foreach (var set in sets)
            {
                if (set.TryGetValue("red", out int reds) && reds > bagReds) return false;
                if (set.TryGetValue("green", out int greens) && greens > bagGreens) return false;
                if (set.TryGetValue("blue", out int blues) && blues > bagBlues) return false;
            }
            return true;
        }
    }
}