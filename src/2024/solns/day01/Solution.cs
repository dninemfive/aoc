using d9.utl;

namespace d9.aoc._24.day01;
[SolutionToProblem(1)]
internal class Solution : AocSolution
{
    public readonly IEnumerable<int> Left, Right;
    public Solution(params string[] lines)
        => (Left, Right) = ParseData(lines);

    [ExpectedResults(final: 1660292)]
    public override AocPartialResult Part1()
        => PairUp(Left, Right).Select(x => Math.Abs(x.l - x.r)).Sum();

    [ExpectedResults(final: 22776016)]
    public override AocPartialResult Part2()
        => SimilarityScores(Left, Right).Sum();

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
