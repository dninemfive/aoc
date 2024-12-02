using System.Linq;

namespace d9.aoc._24.day01;
[SolutionToProblem(1)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        (IEnumerable<int> left, IEnumerable<int> right) = ParseData(lines);
        yield return "preinit";
        yield return PairUp(left, right).Select(x => Math.Abs(x.l - x.r)).Sum();
        yield return SimilarityScores(left, right).Sum();
        
    }
    public static (IEnumerable<int>, IEnumerable<int>) ParseData(string[] lines)
    {
        List<int> left = new(), right = new();
        foreach (string line in lines)
        {
            List<string> split = line.SplitAndTrim(" ");
            left.Add(int.Parse(split[0]));
            right.Add(int.Parse(split[1]));
        }
        return (left, right);
    }
    public static IEnumerable<(int l, int r)> PairUp(IEnumerable<int> left, IEnumerable<int> right)
        => left.Order().Zip(right.Order());
    public static IEnumerable<int> SimilarityScores(IEnumerable<int> left, IEnumerable<int> right)
    {
        Count count = new(right);
        foreach (int l in left)
            yield return l * count[l];
    }
}
