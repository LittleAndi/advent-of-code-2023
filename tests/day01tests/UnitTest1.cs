namespace day01tests;

public class UnitTest1
{
    [Theory]
    // Part 1
    [InlineData("[1-9]", "1abc2", 12)]
    [InlineData("[1-9]", "pqr3stu8vwx", 38)]
    [InlineData("[1-9]", "a1b2c3d4e5f", 15)]

    // Part 2
    [InlineData("[1-9]", "treb7uchet", 77)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "two1nine", 29)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "eightwothree", 83)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "abcone2threexyz", 13)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "xtwone3four", 24)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "4nineeightseven2", 42)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "zoneight234", 14)]
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "7pqrstsixteen", 76)]

    // Additional tests
    [InlineData("[1-9]|one|two|three|four|five|six|seven|eight|nine", "oneight", 18)]
    public void ShouldReturnCalibrationValue(string regex, string line, int calibrationValue)
    {
        var calibrationThing = new CalibrationThing(line, regex);
        calibrationThing.CalibrationValue.ShouldBe(calibrationValue);
    }
}