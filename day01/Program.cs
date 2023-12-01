using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt")
    .ToArray<string>();

PartOne(lines, "[1-9]");
PartOne(lines, "[1-9]|one|two|three|four|five|six|seven|eight|nine");


static void PartOne(string[] lines, string matchString)
{
    int total = 0;
    foreach (var line in lines)
    {
        var ct = new CalibrationThing(line, matchString);
        var firstDigit = ct.FirstDigit;
        var lastDigit = ct.LastDigit;

        var number = Convert.ToInt32($"{firstDigit}{lastDigit}");

        total += number;
    }

    System.Console.WriteLine(total);
}

public record CalibrationThing(string Line, string MatchString)
{
    private readonly Regex regex1 = new(MatchString);
    private readonly Regex regex2 = new(MatchString, RegexOptions.RightToLeft);
    public char FirstDigit
    {
        get
        {
            var matches = regex1.Matches(Line);
            var match = matches.First().Value;
            if (match.Length > 1)
            {
                match = match switch
                {
                    "one" => "1",
                    "two" => "2",
                    "three" => "3",
                    "four" => "4",
                    "five" => "5",
                    "six" => "6",
                    "seven" => "7",
                    "eight" => "8",
                    "nine" => "9",
                    _ => "X"
                };
            }
            return Convert.ToChar(match);
        }
    }
    public char LastDigit
    {
        get
        {
            var matches = regex2.Matches(Line);
            var match = matches.First().Value;
            if (match.Length > 1)
            {
                match = match switch
                {
                    "one" => "1",
                    "two" => "2",
                    "three" => "3",
                    "four" => "4",
                    "five" => "5",
                    "six" => "6",
                    "seven" => "7",
                    "eight" => "8",
                    "nine" => "9",
                    _ => "X"
                };
            }
            return Convert.ToChar(match);
        }
    }
}

