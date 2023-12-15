using System.Text.RegularExpressions;

Console.WriteLine(
    File.ReadAllLines("C:/Repos/adventofcode23/advent-of-code/day1/dataexport.txt")
        .Select(input => Regex.Matches(input, "(?=([1-9]|one|two|three|four|five|six|seven|eight|nine))")
            .Select(m => (len: m.Groups[1].Length, dig: m.Groups[1].Value switch
            {
                "1" or "one" => 1,
                "2" or "two" => 2,
                "3" or "three" => 3,
                "4" or "four" => 4,
                "5" or "five" => 5,
                "6" or "six" => 6,
                "7" or "seven" => 7,
                "8" or "eight" => 8,
                "9" or "nine" => 9,
                _ => throw new Exception(),
            }))
            .Aggregate<(int len, int dig), (int part1digit1, int part1digit2, int part2digit1, int part2digit2)>
            ((-1, -1, -1, -1), (s, m) => m.len == 1 ?
                (
                    (s.part2digit1 < 0) ? (m.dig, m.dig, m.dig, m.dig) :
                    (s.part1digit1 < 0) ? (m.dig, m.dig, p2d1: s.part2digit1, m.dig) :
                    (s.part1digit1, m.dig, p2d1: s.part2digit1, m.dig)
                ) :
                (
                    (s.part2digit1 < 0) ? (p1d1: s.part1digit1, p1d2: s.part1digit2, m.dig, m.dig) :
                        (p1d1: s.part1digit1, p1d2: s.part1digit2, s.part2digit1, m.dig)
                )
            )
        )
        .Select(agg => (part1: agg.part1digit1 * 10 + agg.part1digit2, part2: agg.part2digit1 * 10 + agg.part2digit2))
        .Aggregate( (part1: 0, part2: 0), (acc, x) => (acc.part1 + x.part1, acc.part2 + x.part2))
        .ToString()
);