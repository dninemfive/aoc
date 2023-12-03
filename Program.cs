using d9.aoc._23;
using System.Reflection;
static class Program
{
    public const string INPUT_FOLDER = @"C:\Users\dninemfive\Documents\workspaces\misc\_aoc\2023\";
    private static void Main()
    {
        foreach((MethodInfo solution, SolutionToProblemAttribute? attribute) in Assembly.GetExecutingAssembly()
                                             .GetTypes()
                                             .SelectMany(type => type.GetMethods())
                                             .Select(method => (method, attr: method.GetCustomAttribute<SolutionToProblemAttribute>()))
                                             .OrderBy(x => x.attr?.Index))
        {
            if (attribute is null 
                || !solution.IsStatic 
                || !solution.ParametersMatch(typeof(IEnumerable<object>), typeof(string[])))
                continue;
            Console.WriteLine($"Solution for Problem {attribute.Index}:");
            string inputFile = Path.Join(INPUT_FOLDER, $"{attribute.Index}_input.txt");
            int partNumber = 1;
            foreach (object part in (IEnumerable<object>)solution.Invoke(null, [File.ReadAllText(inputFile)])!)
                Console.WriteLine($"\tPart {partNumber++}: {part}");
        }
    }
    public static bool ParametersMatch(this MethodInfo method, Type returnType, params Type[] argumentTypes)
    {
        if(method.ReturnType != returnType) return false;
        Type[] methodArgTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
        return methodArgTypes.Length == argumentTypes.Length 
            && methodArgTypes.Zip(argumentTypes)
                             .All(x => x.First == x.Second);
    }
}