namespace aoc_solns_2023;
[AttributeUsage(AttributeTargets.Method)]
public class SolutionToProblemAttribute(int index)
    : Attribute
{
    public int Index { get; private set; } = index;
}
