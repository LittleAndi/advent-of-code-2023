using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");
var nodes = File.ReadAllLines("input.txt")
    .Skip(2)
    .Select(line => new Node(line));

var network = new Network(lines[0], nodes);
var steps = network.WalkThrough();

System.Console.WriteLine($"Part 1: {steps} steps needed");

var steps2 = network.WalkThroughAToZ();

System.Console.WriteLine($"Part 2: {steps2} steps needed");

public class Network(string Instructions, IEnumerable<Node> Nodes)
{
    public int WalkThrough()
    {
        var currentNode = Nodes.First(n => n.NodeKey.Equals("AAA"));
        var instructionCount = Instructions.Length;

        var pos = 0;
        var steps = 0;

        while (!currentNode.NodeKey.Equals("ZZZ"))
        {
            steps++;

            var instruction = Instructions[pos];
            currentNode = Nodes.First(n => n.NodeKey.Equals(currentNode.Navigation[instruction]));

            pos++;
            if (pos >= instructionCount) pos = 0;
        }

        return steps;
    }

    public long WalkThroughAToZ()
    {
        Dictionary<int, long> visitedZNodesCount = [];

        var currentNodes = Nodes.Where(n => n.NodeKey[2].Equals('A')).ToArray();
        var instructionCount = Instructions.Length;

        var pos = 0;
        var steps = 0;

        //var currentNode = currentNodes.First();
        while (currentNodes.Count(n => n.NodeKey[2].Equals('Z')) != currentNodes.Length)
        {
            steps++;

            var instruction = Instructions[pos];

            // Move all nodes
            for (int i = 0; i < currentNodes.Length; i++)
            {
                currentNodes[i] = Nodes.First(n => n.NodeKey.Equals(currentNodes[i].Navigation[instruction]));
                if (currentNodes[i].NodeKey.EndsWith('Z'))
                {
                    if (visitedZNodesCount.TryAdd(i, steps))
                        System.Console.WriteLine($"{i}: {steps}");
                }
            }

            if (visitedZNodesCount.Count == currentNodes.Length)
            {
                break;
            }

            pos++;
            if (pos >= instructionCount) pos = 0;
        }

        long[] numbers = visitedZNodesCount.Select(n => n.Value).ToArray();
        var lcm = LCM(numbers);
        return lcm;
    }

    // From https://stackoverflow.com/questions/147515/least-common-multiple-for-3-or-more-numbers/29717490#29717490
    static long LCM(long[] numbers)
    {
        return numbers.Aggregate(lcm);
    }
    static long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

    private void PrintNodes(Dictionary<int, long> visitedNodes)
    {
        foreach (var node in visitedNodes.OrderByDescending(n => n.Value).Take(10))
        {
            System.Console.WriteLine($"{node.Key}: {node.Value}");
        }
        System.Console.WriteLine("---------------------");
    }
}

public partial class Node
{
    public string NodeKey { get; }
    public Dictionary<char, string> Navigation = [];
    public long Visited { get; set; } = 0;
    public Node(string nodeInput)
    {
        Regex regex = MyRegex();
        var matches = regex.Matches(nodeInput);
        NodeKey = matches[0].Value;
        Navigation.Add('L', matches[1].Value);
        Navigation.Add('R', matches[2].Value);
    }

    [GeneratedRegex(@"\w{3}")]
    private static partial Regex MyRegex();
}