using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

var part1Result = SumOfCalibrationValues(lines, "[1-9]");
System.Console.WriteLine($"Part 1: Sum of calibration values: {part1Result}");

var part2Result = SumOfCalibrationValues(lines, "[1-9]|one|two|three|four|five|six|seven|eight|nine");
System.Console.WriteLine($"Part 2: Sum of calibration values: {part2Result}");

static int SumOfCalibrationValues(string[] lines, string matchString)
{
    int total = 0;
    foreach (var line in lines)
    {
        var ct = new CalibrationThing(line, matchString);
        total += ct.CalibrationValue;
    }

    return total;
}

public record CalibrationThing(string Line, string MatchString)
{
    private readonly Regex regexLeftToRight = new(MatchString);
    private readonly Regex regexRightToLeft = new(MatchString, RegexOptions.RightToLeft);
    private readonly Dictionary<string, string> textLookup = new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
    };

    public char FirstDigit
    {
        get
        {
            var matches = regexLeftToRight.Matches(Line);
            var match = matches.First().Value;
            if (match.Length > 1)
            {
                match = textLookup[match];
            }
            return Convert.ToChar(match);
        }
    }
    public char LastDigit
    {
        get
        {
            var matches = regexRightToLeft.Matches(Line);
            var match = matches.First().Value;
            if (match.Length > 1)
            {
                match = textLookup[match];
            }
            return Convert.ToChar(match);
        }
    }

    public int CalibrationValue => Convert.ToInt32($"{FirstDigit}{LastDigit}");
}

