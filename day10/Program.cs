// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");


public class PipeMaze
{
    char[][] map;
    List<(int i, int j, int step)> path = [];
    private (int i, int j) positionS;

    public PipeMaze(string[] input)
    {
        map = input.Select(line => line.ToCharArray()).ToArray();

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i].Contains('S')) positionS = (i, Array.FindIndex(map[i], x => x == 'S'));
        }

        MapTheSteps();
    }

    private void MapTheSteps()
    {
        // Start at S
        var pos = map[positionS.i][positionS.j];
        var steps = 0;
        bool weAreBackAtStart = false;

        while (!weAreBackAtStart)
        {
            (int i, int j) = MoveNext(pos);
        }
    }

    private (int i, int j) MoveNext(int iCurrent, int jCurrent)
    {
        return map[iCurrent][jCurrent] switch
        {
            '|'
            _ => (1, 2)
        };
    }

    public (int x, int y) PositionS => positionS;
}

