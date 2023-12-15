using System.Text.RegularExpressions;

(int, int, int) initialCubes = (12, 13, 14);

var part1Sum = File.ReadAllText("C:/Repos/adventofcode23/advent-of-code/day2/data/day2.txt")
    .Split("\n")
    .Select(line => new Game(line))
    .Where(game => game.IsValid(initialCubes))
    .Aggregate(0, (sum, game) => sum + game.IdNumber);

Console.WriteLine(part1Sum);

var part2Sum = File.ReadAllText("C:/Repos/adventofcode23/advent-of-code/day2/data/day2.txt")
    .Split("\n")
    .Select(line => new Game(line))
    .Select(game => game.GetSmallestPossibleConfiguration())
    .Select(tuple => tuple.Item1 * tuple.Item2 * tuple.Item3)
    .Aggregate(0, (sum, power) => sum + power);

Console.WriteLine(part2Sum);

internal readonly struct Game
{
    public readonly int IdNumber { get; }
    
    //  (red, green, blue) cubes.
    public readonly IEnumerable<(int, int, int)> RgbTuples { get; }

    //  Assumes perfectly formatted input 
    public Game(string information)
    {
        IdNumber = int.Parse(new Regex(@"\d+").Match(new Regex(@"Game +\d+: +").Match(information).Value).Value);

        information = Regex.Replace(information, @"Game +\d+: +", "");  //  Removes the 'Game x:' prefix
        information = Regex.Replace(information, " +", "");             //  Removes all whitespaces

        RgbTuples = information
            .Split(";")
            .Select(GetTuple);
    }

    public bool IsValid((int, int, int) initialConfiguration)
    {
        return RgbTuples.All(tuple => tuple.Item1 <= initialConfiguration.Item1 && 
                               tuple.Item2 <= initialConfiguration.Item2 &&
                               tuple.Item3 <= initialConfiguration.Item3);
    }

    public (int, int, int) GetSmallestPossibleConfiguration()
    {
        (int, int, int) currentTuple = (0, 0, 0);

        foreach (var tuple in RgbTuples)
        {
            currentTuple = 
                (Int32.Max(currentTuple.Item1, tuple.Item1), 
                Int32.Max(currentTuple.Item2, tuple.Item2),
                Int32.Max(currentTuple.Item3, tuple.Item3));
        }

        return currentTuple;
    }

    //  Also assumes perfectly formatted input
    private static (int, int, int) GetTuple(string rawTupleInformation)
    {
        Match redCubesMatch = new Regex(@"\d+red").Match(rawTupleInformation);
        int redCubes = redCubesMatch.Success
            ? int.Parse(new Regex(@"\d+").Match(redCubesMatch.Value).Value)
            : 0;
        
        Match greenCubesMatch = new Regex(@"\d+green").Match(rawTupleInformation);
        int greenCubes = greenCubesMatch.Success
            ? int.Parse(new Regex(@"\d+").Match(greenCubesMatch.Value).Value)
            : 0;
        
        Match blueCubesMatch = new Regex(@"\d+blue").Match(rawTupleInformation);
        int blueCubes = blueCubesMatch.Success
            ? int.Parse(new Regex(@"\d+").Match(blueCubesMatch.Value).Value)
            : 0;
        
        return (redCubes, greenCubes, blueCubes);
    }
}