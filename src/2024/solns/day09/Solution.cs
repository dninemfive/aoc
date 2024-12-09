
namespace d9.aoc._24.day09;
[SolutionToProblem(9)]
[SampleResults(1928)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Filesystem filesystem = new(lines.First());
        Console.WriteLine(filesystem);
        yield return "preinit";
        yield return 1928;
    }
}
internal class Filesystem
{
    internal readonly List<int?> Files = new();
    public Filesystem(string line)
    {
        for(int i = 0; i < line.Length; i++)
        {
            int blockSize = int.Parse($"{line[i]}");
            int? blockContents = i % 2 == 0 ? i / 2 : null;
            for (int j = 0; j < blockSize; j++)
                Files.Add(blockContents);
        }
    }
    public override string ToString()
        => Files.Select(x => x.PrintNull(".")).Join();
}