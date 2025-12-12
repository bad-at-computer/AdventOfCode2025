namespace AdventOfCode2025.Days;

public interface IDay
{
    int DayNumber { get; }
    string SolvePart1(string input);
    string SolvePart2(string input);
}