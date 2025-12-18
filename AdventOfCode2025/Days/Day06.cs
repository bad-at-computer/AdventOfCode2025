namespace AdventOfCode2025.Days;

public class Day06 : IDay
{
    public int DayNumber => 6;

    public string SolvePart1(string input)
    {
        var rows = input.Split('\n');
        var height = rows.Length;
        var mathArray = new string[height][];
        var total = 0L;

        for (var i = 0; i < height; i++)
        {
            mathArray[i] = rows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
        
        
        for (var i = 0; i < mathArray[0].Length; i++)
        {
            var currentTotal = 0L;

            if (mathArray[height - 1][i].Equals("+"))
            {
                for (var j = 0; j < mathArray.Length - 1; j++)
                {
                    currentTotal += long.Parse(mathArray[j][i]);
                }
            }

            if (mathArray[height - 1][i].Equals("*"))
            {
                currentTotal = 1;
                for (var j = 0; j < mathArray.Length - 1; j++)
                {
                    currentTotal *= long.Parse(mathArray[j][i]);
                }
            }

            total += currentTotal;
        }

        return total.ToString();
    }

    public string SolvePart2(string input)
    {
        return "not implemented";
    }
}