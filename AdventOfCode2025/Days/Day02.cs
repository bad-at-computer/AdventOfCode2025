using System.Diagnostics;

namespace AdventOfCode2025.Days;

public class Day02 : IDay
{
    public int DayNumber => 2;

    public string SolvePart1(string input)
    {
        long total = 0;
        var ranges = input.Split(',');
        foreach (var range in ranges)
        {
            var rangeArray = range.Split('-');
            var firstId = rangeArray[0].Trim();
            var endId = rangeArray[1].Trim();
            var firstIdNumeric = long.Parse(firstId);
            var endIdNumeric = long.Parse(endId);
            
            for (var currentId = firstIdNumeric; currentId <= endIdNumeric; currentId++)
            {
                if (IsInvalidIdA(currentId.ToString()))
                {
                    total += currentId;
                }
            }
        }
        
        return total.ToString();
    }

    public string SolvePart2(string input)
    {
        long total = 0;
        var ranges = input.Split(',');
        foreach (var range in ranges)
        {
            var rangeArray = range.Split('-');
            var firstId = rangeArray[0].Trim();
            var endId = rangeArray[1].Trim();
            var firstIdNumeric = long.Parse(firstId);
            var endIdNumeric = long.Parse(endId);

            long max = 0;
            
            for (var currentId = firstIdNumeric; currentId <= endIdNumeric; currentId++)
            {
                // if (IsInvalidIdB(currentId.ToString()))
                // {
                //     total += currentId;
                // }
                if (currentId > max)
                {
                    max = currentId;
                }
            }

            DebugPrint.Log($"longest string is {max.ToString().Length}");
        }
        
        return total.ToString();
    }

    public bool IsInvalidIdA(string currentIdString)
    {
        var length = currentIdString.Length;
        if (length % 2 != 0) return false;
        var firstHalf = currentIdString.Substring(0, length / 2);
        var secondHalf = currentIdString.Substring(length / 2, length / 2);
        return firstHalf.Equals(secondHalf);
    }

    public bool IsInvalidIdB(string currentIdString)
    {
       // TODO use Regex to identify repetition
        return false;
        
    }
}