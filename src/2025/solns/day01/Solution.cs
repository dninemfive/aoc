using Rotation = d9.aoc._25.day01.Rotation<int>;
using N = int;

namespace d9.aoc._25.day01;
[SolutionToProblem(1)]
internal class Solution(params string[] lines) 
    : AocSolution
{
    IEnumerable<Rotation> Rotations => lines.Select(x => (Rotation<int>)x);
    public override AocPartResultValue? Part1()
    {
        N cur = 50, result = 0;
        foreach(Rotation rotation in Rotations)
        {
            cur += rotation;
            if(cur == 0)
                result++;
        }
        return result;
    }
}
