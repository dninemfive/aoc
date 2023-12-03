using d9.aoc._23;
using System.Reflection;
static class Program
{
    public const string INPUT_FOLDER = @"C:\Users\dninemfive\Documents\workspaces\misc\_aoc\2023\";
    /// <summary>
    /// Main method for the program. Looks for static methods with the 
    /// <see cref="SolutionToProblemAttribute">SolutionToProblem</see> attribute with the appropriate
    /// signature and uses the results to generate output with a consistent structure.
    /// </summary>
    private static void Main()
    {
        // scary query just gets all the loaded methods with the appropriate signature
        // and associates them with their attribute, if any.
        foreach((MethodInfo solution, SolutionToProblemAttribute? attribute)
                in Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .SelectMany(type => type.GetMethods())
                           .Where(HasAppropriateSignature)
                           .Select(MethodAndAttribute)
                           .OrderBy(x => x.attr?.Index))
        {
            if (attribute is null)
                continue;
            Console.WriteLine($"Solution for Problem {attribute.Index}:");
            string inputFile = Path.Join(INPUT_FOLDER, $"{attribute.Index}_input.txt");
            int partNumber = 1;
            foreach (object part in solution.UsingFile(inputFile))
                Console.WriteLine($"\tPart {partNumber++}: {part}");
        }
    }
    /// <summary>
    /// Checks that the specified method has the desired signature.
    /// </summary>
    /// <param name="method">The method whose signature to compare.</param>
    /// <param name="returnType">The type the method should return.</param>
    /// <param name="argumentTypes">The type(s), if any, the method should have as arguments</param>
    /// <returns><see langword="true"/> if the method has the specified 
    /// <paramref name="argumentTypes"/> and <paramref name="returnType"/>, or
    /// <see langword="false"/> otherwise.</returns>
    public static bool ParametersMatch(this MethodInfo method, Type returnType, params Type[] argumentTypes)
    {
        if(method.ReturnType != returnType) return false;
        Type[] methodArgTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
        return methodArgTypes.Length == argumentTypes.Length 
            && methodArgTypes.Zip(argumentTypes)
                             .All(x => x.First == x.Second);
    }
    /// <summary>
    /// Checks that the given method has the signature desired for AoC 2023 solution methods, i.e. 
    /// <inheritdoc cref="HasAppropriateSignature(MethodInfo)" path="/sig"/>.
    /// </summary>
    /// <sig><c><see langword="static"/> <see cref="IEnumerable{T}">IEnumerable</see>
    /// &lt;<see langword="string"/>&gt; [method](<see langword="string"/>[])</c></sig>
    /// <param name="method"></param>
    /// <returns></returns>
    public static bool HasAppropriateSignature(this MethodInfo method)
        => method.IsStatic && method.ParametersMatch(typeof(IEnumerable<object>), typeof(string));
    public static (MethodInfo method, SolutionToProblemAttribute? attr) MethodAndAttribute(this MethodInfo method)
        => (method, method.GetCustomAttribute<SolutionToProblemAttribute>());
    public static IEnumerable<object> UsingFile(this MethodInfo method, string filePath)
        => (IEnumerable<object>)method.Invoke(null, [File.ReadAllLines(filePath)])!;
}