using aoc_solns_2023;

static class Program
{
    public static List<Action> Solutions = [
            Problem1.Solve
        ];
    private static void Main()
    {
        int index = 1;
        foreach(Action solution in Solutions)
        {
            Console.WriteLine($"Solution for {index++}:");
            solution();
        }
    }
}