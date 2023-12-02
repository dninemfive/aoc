namespace d9.aoc._23;
[AttributeUsage(AttributeTargets.Method)]
public class SolutionToProblemAttribute(int index)
    : Attribute
{
    public int Index { get; private set; } = index;
}