using System.Diagnostics;
using System.Reflection;
using d9.utl;

namespace d9.aoc.core;
public class AocSolutionGroup(Assembly assembly, string? name = null)
{
    public Assembly Assembly => assembly;
    public string Name => name ?? assembly.ToString();
    /// <remarks>
    /// See 
    /// <see href="https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in#comment26343106_2887537">
    /// this StackOverflow comment
    /// </see>.
    /// </remarks>
    public string BaseFolderPath
        => $"{Path.GetDirectoryName(Assembly.Location)}/input";
    public IEnumerable<IEnumerable<AocSolutionPart>> ExecuteAllSolutions()
        => Solutions.OrderBy(x => x.Day)
                    .Select(x => x.Execute(BaseFolderPath));
    private List<AocSolution>? _solutions = null;
    public IEnumerable<AocSolution> Solutions
    {
        get
        {
            if (_solutions is null)
            {
                _solutions = new();
                foreach (Type type in Assembly.GetTypes()
                                         .Where(x => x.HasCustomAttribute<SolutionToProblemAttribute>()))
                {
                    AocSolution? solution = AocSolution.From(type, BaseFolderPath);
                    if (solution is not null)
                        _solutions.Add(solution);
                }
            }
            return _solutions;
        }
    }
    public void ExecuteAll()
    {
        foreach (AocSolution solution in Solutions)
            foreach (string line in solution.ResultLines(BaseFolderPath))
                Console.WriteLine(line);
    }
}