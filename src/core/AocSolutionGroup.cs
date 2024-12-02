using d9.utl;
using System.Collections;
using System.Reflection;

namespace d9.aoc.core;
public class AocSolutionGroup(Assembly assembly, string? name = null)
    : IEnumerable<AocSolution>
{
    public Assembly Assembly => assembly;
    public string Name => name ?? assembly.ToString();
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
        foreach (AocSolution solution in Solutions)
            foreach (string line in solution.ResultLines(InputFolder))
                Console.WriteLine(line);
    }
    public IEnumerator<AocSolution> GetEnumerator()
        => Solutions.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}