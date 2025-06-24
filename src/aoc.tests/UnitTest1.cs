using d9.aoc.core;
using d9.utl;
using d9.utl.utils.reflection;
using System.Reflection;

namespace d9.aoc.tests;

public class Tests
{
    private static readonly List<AocSolutionInfo> _solutionInfos = new();
    [OneTimeSetUp]
    public static void Init()
    {
        List<object> _stuffToForceAssembliesToLoad = [new _23.Program(), new _24.Program()];
        foreach ((Assembly assembly, SolutionsForYearAttribute _) in AppDomain.CurrentDomain.AssembliesWithAttribute<SolutionsForYearAttribute>())
            foreach ((Type type, SolutionToProblemAttribute _) in assembly.TypesWithAttribute<SolutionToProblemAttribute>())
                _solutionInfos.Add(new(type));
        foreach (AocSolutionInfo info in _solutionInfos)
        {
            foreach (TestCaseData datum in TestCases(info))
                _data.Add(datum);
        }
    }
    private static string ImplementationName(int part, bool sample)
        => $"Part {part:00}{(sample ? " (sample)" : "")}";
    private static IEnumerable<TestCaseData> TestCasesFor(AocSolutionInfo info, AocPartImplementation impl)
    {
        Console.WriteLine($"\t\t\tTestCasesFor({info}, {impl})");
        if(impl.ExpectedResults is ExpectedResultsAttribute era)
        {
            Console.WriteLine($"\t\t\t\t{era}");
            if (era.Sample is not null)
                yield return new TestCaseData(info, impl, true).Returns(era.Sample).SetName(ImplementationName(impl.Part, true));
            if (era.Final is not null)
                yield return new TestCaseData(info, impl, false).Returns(era.Final).SetName(ImplementationName(impl.Part, false));
        }
    }
    private static IEnumerable<TestCaseData> TestCases(AocSolutionInfo soln)
    {
        Console.WriteLine($"\t\t{soln}");
        foreach (AocPartImplementation impl in soln.ImplementedParts)
            foreach (TestCaseData datum in TestCasesFor(soln, impl))
                yield return datum.SetCategory($"Year {soln.Year} Day {soln.Day}");
    }
    private static readonly ICollection<TestCaseData> _data = [];
    public static IEnumerable<TestCaseData> Data
    {
        get
        {
            yield return new TestCaseData(new AocSolutionInfo(typeof(_23.day01.Solution)), new AocPartImplementation(1, typeof(_23.day01.Solution).GetMethod("Part1")!), false).Returns(55090);
        }
    }

    [TestCaseSource(nameof(Data))]
    public object? TestPart(AocSolutionInfo info, AocPartImplementation impl, bool sample)
    {
        AocSolution parent = info.Instantiate(out TimeSpan initTime, sample, impl.Part);
        Console.WriteLine($"Instantiated {parent.GetType().FullName} instance in {initTime:g}");
        AocPartResult? result = impl.Execute(parent);
        if (result is null)
            return null;
        Console.WriteLine($"Executed implementation in {result.Elapsed:g}");
        return result.Value;
    }
    [Test]
    public void TestTests()
    {
        Console.WriteLine("TestTests()");
        Console.WriteLine(Data.Select(x => $"{x.Arguments.ListNotation(brackets: ("T{", "}"))}").ListNotation());
        Console.WriteLine(typeof(_23.day01.Solution).GetConstructors().ListNotation());
        string path = @"C:\Users\dninemfive\Documents\workspaces\misc\aoc\src\2023\input\day01.txt";
        Console.WriteLine(Activator.CreateInstance(typeof(_23.day01.Solution), [File.ReadAllLines(path)]));
        Console.WriteLine(new AocSolutionInfo(typeof(_23.day01.Solution)).Instantiate(path, out TimeSpan _));
        Console.WriteLine(new AocSolutionInfo(typeof(_23.day01.Solution)).Instantiate(out TimeSpan _, false, 1));
        AocSolution soln = new AocSolutionInfo(typeof(_23.day01.Solution)).Instantiate(out TimeSpan _, false, 1);
        Console.WriteLine(new AocPartImplementation(1, typeof(_23.day01.Solution).GetMethod("Part1")!).Execute(soln));
        Assert.That(Data, Is.Not.Empty);
    }
}
