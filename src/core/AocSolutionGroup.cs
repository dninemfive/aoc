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
    /// <summary>
    /// Main method for the program. Looks for static methods with the 
    /// <see cref="SolutionToProblemAttribute">SolutionToProblem</see> attribute with the appropriate
    /// signature and uses the results to generate output with a consistent structure.
    /// </summary>
    public void ExecuteAllSolutions()
    {
        // scary query just gets all the loaded methods with the appropriate signature
        // and associates them with their attribute, if any.
        foreach ((MethodInfo solution, SolutionToProblemAttribute? attribute)
                in Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .SelectMany(type => type.GetMethods())
                           .Where(HasAppropriateSignature)
                           .Select(MethodAndAttribute)
                           .OrderBy(x => x.attr?.Day))
        {
            if (attribute is null)
                continue;
            ExecuteSolution(solution, attribute);
        }
    }
    public IEnumerable<AocSolution> AllSolutions()
        => Assembly.GetTypes()
                   .Where(x => x.HasCustomAttribute<SolutionToProblemAttribute>())
                   .Select(x => AocSolution.From(x, ))
    public void ExecuteSolution(MethodInfo solution, SolutionToProblemAttribute attribute)
    {
        Console.WriteLine($"Solution for Problem {attribute.Day}:");
        string inputFile = Path.Join(InputFolder, $"{attribute.Day}.input");
        int partNumber = attribute.HasStartupMarker ? 0 : 1;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        foreach (object part in solution.UsingFile(inputFile))
        {
            stopwatch.Stop();
            if (partNumber < 1)
            {
                Console.WriteLine($"\tpreinit:\t{"",15}\t{$"{stopwatch.Elapsed:c}",16}");
                partNumber++;
            }
            else
            {
                Console.WriteLine($"\tPart {partNumber++}:\t{part,16}\t{$"{stopwatch.Elapsed:c}",16}");
            }
            stopwatch.Restart();
        }
    }
    /// <summary>
    /// Checks that the given method has the signature desired for AoC 2023 solution methods, i.e. 
    /// <inheritdoc cref="HasAppropriateSignature(MethodInfo)" path="/sig"/>.
    /// </summary>
    /// <sig><c><see langword="static"/> <see cref="IEnumerable{T}">IEnumerable</see>
    /// &lt;<see langword="object"/>&gt; [method](<see langword="string"/>[])</c></sig>
    /// <param name="method"></param>
    /// <returns></returns>
    public static bool HasAppropriateSignature(MethodInfo method)
        => method.IsStatic && method.ParametersMatch(typeof(IEnumerable<object>), typeof(string[]));
    public static (MethodInfo method, SolutionToProblemAttribute? attr) MethodAndAttribute(MethodInfo method)
        => (method, method.GetCustomAttribute<SolutionToProblemAttribute>());
}