using Solution = d9.aoc._24.day11.Solution;

[assembly: SolutionsForYear(2024)]

namespace d9.aoc._24;

public class Program
{
    public static void Main()
    {
        // todo: find latest & run it automatically
        AocSolutionInfo info = new(typeof(Solution));
        AocSolution soln = info.Instantiate(out TimeSpan initTime, part: 2)!;
        Console.WriteLine($"Initialized solution in {initTime:g}");
    }
}