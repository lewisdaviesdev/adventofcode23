using System.Text.RegularExpressions;

internal class Program
{
    internal class GetPartsResult
    {
        public IEnumerable<int> ValidParts = null!;
        public int GearRatio;
    }

    public Program(string[] args)
    {
        var lines = File.ReadAllLines(@"C:\Repos\adventofcode23\advent-of-code\day3\data\day3.txt");
        var lineLength = lines[0].Length;

        var symbolsRegex = new Regex("[^0-9.\r\n]");
        var starRegex = new Regex("[*]");
        var numbersRegex = new Regex(@"\d+");

        var potentialGears = new Dictionary<string, List<int>>();
        var validPartNumbers = new List<int>();

        // Get all numbers, along with their line number, start index and end index relative to the line that they are on.
        var numbers = lines.SelectMany(
            (line, lineNumber) => numbersRegex.Matches(line)
                .Select(x => new
                {
                    Value = int.Parse(x.Value),
                    LineNumber = lineNumber,
                    StartIndex = x.Index, x.Length,
                    EndIndex = x.Index + x.Length
                }));
    }
}