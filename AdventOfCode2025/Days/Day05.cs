namespace AdventOfCode2025.Days;

public class Day05 : IDay
{
    public int DayNumber => 5;

    public string SolvePart1(string input)
    {
        // input = "3-5\n10-14\n16-20\n12-18\n\n1\n5\n8\n11\n17\n32";
        var initialInput = input.Split("\n\n");
        var ranges = initialInput[0].Split('\n');
        var ingredientIds = initialInput[1].Split('\n');

        var freshRanges = new List<Range>(); 
        foreach (var range in ranges)
        {
            var currentRange = range.Split('-');
            freshRanges.Add(new Range(long.Parse(currentRange[0]), long.Parse(currentRange[1])));
        }

        var freshIngredientsCount = 0;
        foreach (var ingredient in ingredientIds)
        {
            var fresh = false;
            var id = long.Parse(ingredient);
            foreach (var range in freshRanges)
            {
                if (id >= range.Start && id <= range.End)
                {
                    freshIngredientsCount++;
                    DebugPrint.Log($"Ingredient ID {id} is fresh because it falls into range {range.Start}-{range.End}.");
                    fresh = true;
                    break;
                }
            }
            if (!fresh)
            {
                DebugPrint.Log($"Ingredient Id {id} is spoiled because it does not fall into any range.");
            }
        }
        
        return freshIngredientsCount.ToString();
    }

    public string SolvePart2(string input)
    {
        // input = "3-5\n10-14\n16-25\n12-18\n19-22\n24-26\n30-32";
        // input = "1-3\n3-5";
        var initialInput = input.Split("\n\n");

        var ranges = initialInput[0].Split('\n');
        var freshRanges = new List<Range>();
        foreach (var range in ranges)
        {
            var rangeValues = range.Split('-');
            freshRanges.Add(new Range(long.Parse(rangeValues[0]), long.Parse(rangeValues[1])));
        }

        var orderedFreshRanges = freshRanges.OrderBy(r => r.Start);

        var ingredientRanges = new List<Range>();

        // check ranges, merging as needed
        long previousEnd = 0;
        foreach (var range in orderedFreshRanges)
        {
            var startingPoint = range.Start;
            if (range.Start <= previousEnd)
            {
                startingPoint = previousEnd + 1;
            }
            if (range.End <= previousEnd) continue;
            ingredientRanges.Add(new Range(startingPoint, range.End));
            
            previousEnd = range.End;
        }
        
        // get the total count using math
        long ingredientIdsTotal = 0;
        foreach (var range in ingredientRanges)
        {
            DebugPrint.Log($"{range.Start}-{range.End} has {range.End-range.Start + 1} values");
            ingredientIdsTotal += range.End - range.Start + 1;
        }
        
        return ingredientIdsTotal.ToString();
    }

    class Range
    {
        public Range(long start, long end)
        {
            Start = start;
            End = end;
        }
        public long Start { get; set; } = 0;
        public long End { get; set; } = 0;
    }
}