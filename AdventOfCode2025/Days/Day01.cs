namespace AdventOfCode2025.Days;

public class Day01 : IDay
{
    public int DayNumber => 1;

    public string SolvePart1(string input)
    {
        var currentPosition = 50;
        var countOfZeroes = 0;

        var inputArray = input.Split('\n');

        DebugPrint.Log($"The dial starts by pointing at {currentPosition}");

        foreach (var line in inputArray)
        {
            var direction = line[0];
            var change = int.Parse(line.Substring(1));

            if (direction == 'L')
            {
                change *= -1;
            }

            var initialResult = currentPosition + change;
            var finalResult = (100 + initialResult) % 100;
            if (finalResult == 0)
            {
                countOfZeroes++;
            }
            
            DebugPrint.Log(
                $"The dial at \t{currentPosition} is rotated \t{line} to point at \t{finalResult} passing zero \t{countOfZeroes} times");

            currentPosition = Math.Abs(finalResult);
        }

        return countOfZeroes.ToString();
    }

    public string SolvePart2(string input)
    {
        var currentPosition = 50;
        var countOfZeroes = 0;

        var inputArray = input.Split('\n');


        DebugPrint.Log($"The dial starts by pointing at {currentPosition}");

        foreach (var line in inputArray)
        {
            var zeroPasses = 0;
            var direction = line[0];
            var change = int.Parse(line.Substring(1));

            if (direction == 'L')
            {
                change *= -1;
            }

            var initialResult = currentPosition + change;
            var finalResult = (100 + initialResult) % 100;
            if (finalResult == 0)
            {
                zeroPasses++;
            }

            // TODO this is the issue: 
            
            countOfZeroes += zeroPasses;

            DebugPrint.Log(
                $"The dial at \t{currentPosition} is rotated \t{line} to point at \t{finalResult} passing zero \t{zeroPasses} times");

            currentPosition = Math.Abs(finalResult);
        }

        return countOfZeroes.ToString();
    }
}