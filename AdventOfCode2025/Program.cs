// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net;
using System.Reflection;
using AdventOfCode2025.Days;

Console.WriteLine("Welcome to Advent of Code!");

// Read Day number
var day = 0;
while (day is < 1 or > 31)
{
    Console.Write("\nPlease enter the day: ");
    var response = Console.ReadLine();
    
    // Validate input
    if (int.TryParse(response, out day) && day >= 1 && day <= 31)
    {
        break;
    }
    Console.WriteLine("Invalid input. Please enter a number between 1 and 31.");
}

// Get session cookie
var session = Environment.GetEnvironmentVariable("AOC_SESSION");

if (session == null)
{
    Console.WriteLine("No session cookie found in the app configuration.");
    return -1;
}

var fetcher = new AdventOfCodeFetcher(session);
var input = await fetcher.GetInputAsync(day);
input = input.Trim();

// Find the matching day class dynamically
var dayInstance = LoadDay(day);

if (dayInstance == null)
{
    Console.WriteLine($"Day {day} not implemented yet.");
    return -1;
}

// Execute
Console.WriteLine("\n--- Part 1 ---");
Console.WriteLine(dayInstance.SolvePart1(input));

Console.WriteLine("\n--- Part 2 ---");
Console.WriteLine(dayInstance.SolvePart2(input));
return 0;


static IDay? LoadDay(int day)
{
    var allDays = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface);
    return allDays
        .Select(t => Activator.CreateInstance(t) as IDay)
        .FirstOrDefault(d => d!.DayNumber == day);
}

// Debug-only printing helper
internal static class DebugPrint
{
    [Conditional("DEBUG")]
    public static void Log(string message)
    {
        if (!Debugger.IsAttached) return;
        Console.WriteLine(message);
    }

    [Conditional("DEBUG")]
    public static void Log(string message, params object?[] args)
    {
        if (!Debugger.IsAttached) return;
        Console.WriteLine(message, args);
    }
}

public class AdventOfCodeFetcher
{
    private readonly HttpClient _httpClient;

    public AdventOfCodeFetcher(string sessionCookie)
    {
        var handler = new HttpClientHandler()
        {
            CookieContainer = new CookieContainer()
        };
        
        handler.CookieContainer.Add(
            new Uri("https://adventofcode.com"),
            new Cookie("session", sessionCookie)
        );

        _httpClient = new HttpClient(handler);
    }

    public async Task<string> GetInputAsync(int day)
    {
        var url = $"https://adventofcode.com/2025/day/{day}/input";
        return await _httpClient.GetStringAsync(url);
    }
}
