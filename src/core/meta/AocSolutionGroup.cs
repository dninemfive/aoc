using d9.utl;
using System.Collections;
using System.Reflection;
using static d9.aoc.core.Constants;

namespace d9.aoc.core;
public class AocSolutionGroup(Assembly assembly)
    : IEnumerable<AocSolution>
{
    public Assembly Assembly => assembly;
    private SolutionsForYearAttribute? Attribute => Assembly.GetCustomAttribute<SolutionsForYearAttribute>();
    public int Year
        => Attribute?.Year
        ?? throw new Exception("Cannot create an AocSolutionGroup for an assembly without a SolutionsForYear attribute!");
    /// <remarks>
    /// See 
    /// <see href="https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in#comment26343106_2887537">
    /// this StackOverflow comment
    /// </see>.
    /// </remarks>
    public string InputFolder
        => $"{Path.GetDirectoryName(Assembly.Location)!.Ancestor(4)}/input";
    private Dictionary<int, AocSolution>? _solutions = null;
    private Dictionary<int, AocSolution> SolutionDict
    {
        get
        {
            if (_solutions is null)
            {
                _solutions = new();
                foreach (Type type in Assembly.GetTypes()
                                         .Where(x => x.HasCustomAttribute<SolutionToProblemAttribute>()))
                {
                    AocSolution? solution = AocSolution.Instantiate(type);
                    if (solution is not null)
                        _solutions[solution.Day] = solution;
                }
            }
            return _solutions;
        }
    }
    public AocSolution this[int day]
        => SolutionDict[day];
    public IEnumerable<AocSolution> Solutions
        => SolutionDict.OrderBy(x => x.Key)
                       .Select(x => x.Value);
    public void ExecuteAll()
    {
        Console.WriteLine($"Solutions for year {Year}:");
        foreach (AocSolution solution in Solutions)
            if(!solution.Attribute.Complete)
                foreach (string line in solution.ResultLines(InputFolder))
                    Console.WriteLine($"{TAB}{line}");
    }
    public IEnumerator<AocSolution> GetEnumerator()
        => Solutions.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}