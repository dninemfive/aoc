using d9.aoc.core;
using d9.utl;

namespace d9.aoc.test;


public class Tests
{
    public static readonly TestHelper Helper = new();
    public static IEnumerable<TestCaseData> TestCases
        => Helper.Cases;

    [Test, TestCaseSource(nameof(TestCases))]
    public object? TestPart(AocSolutionInfo info, AocPartImplementation impl, bool sample)
    {
        AocSolution parent = info.Instantiate(out TimeSpan initTime, sample, impl.Part);
        Console.WriteLine($"Part {impl.Part}{(sample ? " (sample)" : "")}:");
        Console.WriteLine($"  {parent.GetType().FullName,25}.ctor()   {initTime,16:g}");
        AocPartResult? result = impl.Execute(parent);
        if (result is null)
            return null;
        Console.WriteLine($"  {result.Value.PrintNull(),32}    {result.Elapsed:g}");
        return result.Value;
    }
}
