var things = File.ReadAllLines("input.txt")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => new Thingy(line));

var totalOfNextHistoryValues = things.Sum(t => t.NextHistoryValue);
System.Console.WriteLine($"Part 1: Sum of extrapolated next values are {totalOfNextHistoryValues}");

var totalOfPreviousHistoryValues = things.Sum(t => t.PreviousHistoryValue);
System.Console.WriteLine($"Part 2: Sum of extrapolated previous values are {totalOfPreviousHistoryValues}");

public class Thingy
{
    int[] numbers = [];
    public Thingy(string input)
    {
        numbers = input.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
    }

    public int NextHistoryValue
    {
        get
        {
            // Find the zero-line
            var done = false;
            var currentLine = numbers;
            var savedNumbers = new List<int>() { numbers.Last() };

            while (!done)
            {
                int[] newLine = CreateDiffLine(currentLine);

                // Save some numbers...
                savedNumbers.Add(newLine.Last());

                currentLine = newLine;

                if (newLine.All(n => n == 0)) done = true;
            }

            // What to do with the saved numbers now?
            return savedNumbers.Sum();
        }
    }

    public int PreviousHistoryValue
    {
        get
        {
            // Find the zero-line
            var done = false;
            var currentLine = numbers;
            var savedNumbers = new List<int>() { numbers.First() };

            while (!done)
            {
                int[] newLine = CreateDiffLine(currentLine);

                // Save some numbers...
                savedNumbers.Add(newLine.First());

                currentLine = newLine;

                if (newLine.All(n => n == 0)) done = true;
            }

            // What to do with the saved numbers now?
            savedNumbers.Reverse();
            int lastValue = 0;
            foreach (var item in savedNumbers)
            {
                lastValue = item - lastValue;
            }

            return lastValue;
        }
    }

    private static int[] CreateDiffLine(int[] numbers)
    {
        int[] newLine = new int[numbers.Length - 1];
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            newLine[i] = numbers[i + 1] - numbers[i];
        }
        return newLine;
    }
}