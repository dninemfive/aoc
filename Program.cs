using aoc_solns_2023;
using System.Reflection;
static class Program
{
    public const string INPUT_FOLDER = @"C:\Users\dninemfive\Documents\workspaces\misc\_aoc\2023\";
    private static void Main()
    {
        foreach((MethodInfo method, SolutionToProblemAttribute? attr) in Assembly.GetExecutingAssembly()
                                             .GetTypes()
                                             .SelectMany(type => type.GetMethods())
                                             .Select(method => (method, attr: method.GetCustomAttribute<SolutionToProblemAttribute>()))
                                             .OrderBy(x => x.attr?.Index))
        {
            if (attr is null 
                || !method.IsStatic 
                || method.ReturnType != typeof(IEnumerable<object>)
                || method.GetParameters().Select(x => x.ParameterType).First() != typeof(string))
                continue;
            Console.WriteLine($"Solution for Problem {attr.Index}:");
            string inputFile = Path.Join(INPUT_FOLDER, $"{attr.Index}_input.txt");
            int partNumber = 1;
            foreach (object part in (IEnumerable<object>)method.Invoke(null, [inputFile])!)
                Console.WriteLine($"\tPart {partNumber++}: {part}");
        }
    }
}