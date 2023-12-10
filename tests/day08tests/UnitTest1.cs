using Shouldly;

namespace day08tests;

[Trait("Day", "8")]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var node = new Node("AAA = (BBB, CCC)");
        node.NodeKey.ShouldBe("AAA");
        node.Navigation['L'].ShouldBe("BBB");
        node.Navigation['R'].ShouldBe("CCC");
    }

    [Fact]
    public void ShouldNavigate()
    {
        var nodes = new string[]
        {
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)",
        }
        .Select(l => new Node(l));

        var network = new Network("RL", nodes);
        network.WalkThrough().ShouldBe(2);
    }

    [Fact]
    public void ShouldNavigate2()
    {
        var nodes = new string[]
        {
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)",
        }
        .Select(l => new Node(l));

        var network = new Network("LLR", nodes);
        network.WalkThrough().ShouldBe(6);
    }

    [Fact]
    public void ShouldNavigate3()
    {
        var nodes = new string[]
        {
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)",
        }
        .Select(l => new Node(l));

        var network = new Network("LR", nodes);
        network.WalkThroughAToZ().ShouldBe(6);
    }

}

