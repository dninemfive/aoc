using aoc_solns_2023;

static class Program
{
    public const string INPUT_FOLDER = @"C:\Users\dninemfive\Documents\workspaces\misc\_aoc\2023\";
    public delegate IEnumerable<object> Solution(string inputFile);
    public static List<Solution> Solutions = [
            Problem1.Solve,
            Problem2.Solve
        ];
    private static void Main()
    {
        int solutionNumber = 1;
        foreach(Solution solution in Solutions)
        {
            string inputFile = Path.Join(INPUT_FOLDER, $"{solutionNumber}_input.txt");
            Console.WriteLine($"Solution for Problem {solutionNumber++}:");
            int partNumber = 1;
            foreach (object part in solution(inputFile))
            {
                Console.WriteLine($"\tPart {partNumber++}: {part}");
            }
        }
    }
}