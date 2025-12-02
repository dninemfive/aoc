[assembly: SolutionsForYear(2025)]

namespace d9.aoc._25;

public class Program
{
    public static void Main()
    {
        AocSolutionInfo info = new(typeof(Solution));
        AocSolution soln = info.Instantiate(out TimeSpan initTime, part: 2)!;
        Console.WriteLine($"Initialized solution in {initTime:g}");
    }
}