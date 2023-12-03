using System.Xml.Serialization;

var lines = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray())
    .ToArray();

int sumOfPartNumbers = GetSumOfPartNumbers(lines);
System.Console.WriteLine($"Part 1: Sum of part numbers are {sumOfPartNumbers}");

int sumOfGearRatios = GetSumOfGearRatios(lines);
System.Console.WriteLine($"Part 2: Sum of gear ratios are {sumOfGearRatios}");

int GetSumOfPartNumbers(char[][] input)
{
    var engineSchematic = new EngineSchematic(input);
    return engineSchematic.PartNumbers.Sum();
}

int GetSumOfGearRatios(char[][] input)
{
    var engineSchematic = new EngineSchematic(input);
    return engineSchematic.GearRatios.Sum();
}

public class EngineSchematic
{
    private readonly char[][] schematic;
    private readonly int xSize;
    private readonly int ySize;

    public EngineSchematic(char[][] schematicInput)
    {
        xSize = schematicInput[0].Length;
        ySize = schematicInput.Length;

        schematic = schematicInput;
    }

    public int[] PartNumbers
    {
        get
        {
            var partNumbers = new List<int>();

            for (int y = 0; y < ySize; y++)
            {
                //string line = new(schematic[y]);
                //System.Console.WriteLine(line);

                // Find numbers on that line
                for (int x = 0; x < xSize; x++)
                {
                    if (char.IsDigit(schematic[y][x]))
                    {
                        int numberStart = x;
                        int numberEnd = x;

                        char[] number = [schematic[y][x]];
                        x++;
                        while (x < xSize && char.IsDigit(schematic[y][x]))
                        {
                            numberEnd = x;
                            number = [.. number, schematic[y][x]];
                            x++;
                        }

                        int potentialPartNumber = Convert.ToInt32(new string(number));

                        // Look around for symbols next to it
                        bool isPartNumber = false;

                        // Above -1..+1
                        if (y > 0)
                        {
                            var rangeWithPotentialSymbol = schematic[y - 1][(numberStart - 1 > 0 ? numberStart - 1 : numberStart)..(numberEnd + 2 < xSize + 1 ? numberEnd + 2 : numberEnd + 1)];
                            if (rangeWithPotentialSymbol.Any(c => c != '.' && !char.IsDigit(c))) isPartNumber = true;
                        }

                        // Left
                        if (numberStart > 0)
                        {
                            if (schematic[y][numberStart - 1] != '.' && !char.IsDigit(schematic[y][numberStart - 1])) isPartNumber = true;
                        }

                        // Right
                        if (numberEnd < xSize - 1)
                        {
                            if (schematic[y][numberEnd + 1] != '.' && !char.IsDigit(schematic[y][numberEnd + 1])) isPartNumber = true;
                        }

                        // Below -1..+1
                        if (y < ySize - 1)
                        {
                            var rangeWithPotentialSymbol = schematic[y + 1][(numberStart - 1 > 0 ? numberStart - 1 : numberStart)..(numberEnd + 2 < xSize + 1 ? numberEnd + 2 : numberEnd + 1)];
                            if (rangeWithPotentialSymbol.Any(c => c != '.' && !char.IsDigit(c))) isPartNumber = true;
                        }

                        if (isPartNumber) partNumbers.Add(potentialPartNumber);
                    }
                }

            }
            return [.. partNumbers];
        }
    }

    public int[] GearRatios
    {
        get
        {
            var gearRatios = new List<int>();

            int stars = 0;

            for (int y = 0; y < ySize; y++)
            {
                if (!schematic[y].Contains('*')) continue;


                for (int x = 0; x < xSize; x++)
                {
                    if (schematic[y][x] != '*') continue;

                    List<int> partNumbers = [];

                    // Found *
                    stars++;

                    // Look around
                    int xLookedEnd = x;

                    // Top left
                    if (y > 0 && x > 0 && char.IsDigit(schematic[y - 1][x - 1]))
                    {
                        (int fullPartNumber, _, int end) = FindFullPartNumber(y - 1, x - 1);
                        partNumbers.Add(fullPartNumber);
                        xLookedEnd = end;
                    }

                    // Top
                    if (y > 0 && x > xLookedEnd && char.IsDigit(schematic[y - 1][x]))
                    {
                        (int fullPartNumber, _, int end) = FindFullPartNumber(y - 1, x);
                        partNumbers.Add(fullPartNumber);
                        xLookedEnd = end;
                    }

                    // Top right
                    if (y > 0 && x < xSize - 1 && x + 1 > xLookedEnd && char.IsDigit(schematic[y - 1][x + 1]))
                    {
                        (int fullPartNumber, _, _) = FindFullPartNumber(y - 1, x + 1);
                        partNumbers.Add(fullPartNumber);
                    }

                    // Left
                    if (x > 0 && char.IsDigit(schematic[y][x - 1]))
                    {
                        (int fullPartNumber, int start, int end) = FindFullPartNumber(y, x - 1);
                        partNumbers.Add(fullPartNumber);
                    }

                    // Right
                    if (x < xSize - 1 && char.IsDigit(schematic[y][x + 1]))
                    {
                        (int fullPartNumber, int start, int end) = FindFullPartNumber(y, x + 1);
                        partNumbers.Add(fullPartNumber);
                    }

                    // Bottom left
                    xLookedEnd = x;
                    if (y < ySize - 1 && x > 0 && char.IsDigit(schematic[y + 1][x - 1]))
                    {
                        (int fullPartNumber, _, int end) = FindFullPartNumber(y + 1, x - 1);
                        partNumbers.Add(fullPartNumber);
                        xLookedEnd = end;
                    }

                    // Bottom
                    if (y < ySize - 1 && x > xLookedEnd && char.IsDigit(schematic[y + 1][x]))
                    {
                        (int fullPartNumber, _, int end) = FindFullPartNumber(y + 1, x);
                        partNumbers.Add(fullPartNumber);
                        xLookedEnd = end;
                    }

                    // Bottom right
                    if (y < ySize - 1 && x < xSize - 1 && x + 1 > xLookedEnd && char.IsDigit(schematic[y + 1][x + 1]))
                    {
                        (int fullPartNumber, _, int end) = FindFullPartNumber(y + 1, x + 1);
                        partNumbers.Add(fullPartNumber);
                    }

                    if (partNumbers.Count == 2)
                    {
                        gearRatios.Add(partNumbers.First() * partNumbers.Last());
                    }
                }

            }

            return [.. gearRatios];
        }
    }

    public (int partNumber, int start, int end) FindFullPartNumber(int y, int x)
    {
        // Check that it is a digit at all
        if (!char.IsDigit(schematic[y][x])) return (0, x, x);

        // Find the first digit
        var xTest = x - 1;
        while (xTest >= 0 && char.IsDigit(schematic[y][xTest]))
        {
            xTest--;
            x--;
        }

        // Create a part number
        int start = x;
        char[] number = [schematic[y][x]];
        x++;
        while (x < xSize && char.IsDigit(schematic[y][x]))
        {
            number = [.. number, schematic[y][x]];
            x++;
        }
        int end = x - 1;

        return (Convert.ToInt32(new string(number)), start, end);
    }
}