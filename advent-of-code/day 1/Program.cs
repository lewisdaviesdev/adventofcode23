using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        const string filePath = "C:/Repos/advent-of-code/advent-of-code/day 1/dataexport.txt";
        
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }
        
        var inputArray = File.ReadAllLines(filePath);
        
        var sum = 0;

        foreach (var str in inputArray)
        {
            var firstDigit = FindFirstDigit(str);
            var lastDigit = FindLastDigit(str);
            
            //var result = int.Parse(firstDigit.ToString() + lastDigit.ToString());
            var result = Convert.ToInt32($"{firstDigit}{lastDigit}");
            
            sum += result;

            Console.WriteLine($"String: {str}, First Digit: {firstDigit}, Last Digit: {lastDigit}, Sum: {result}");
        }

        Console.WriteLine($"Final Sum: {sum}");
    }

    private static int FindFirstDigit(string str)
    {
        foreach (char c in str)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
        }

        // Return 0 if no digit is found
        return 0;
    }

    private static int FindLastDigit(string str)
    {
        for (var i = str.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(str[i]))
            {
                return int.Parse(str.Substring(i, 1));
            }
        }

        // Return 0 if no digit is found
        return 0;
    }
}