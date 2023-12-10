namespace d9.aoc._23;
/// <summary>
/// When applied to a method with a signature of 
/// <inheritdoc cref="Program.HasAppropriateSignature(System.Reflection.MethodInfo)" path="/sig"/>,
/// causes <see cref="Program.Main"/> to run it along with other matching methods in order of
/// <paramref name="index"/>.
/// </summary>
/// <param name="index">Which problem the method is a solution for.</param>
[AttributeUsage(AttributeTargets.Method)]
public class SolutionToProblemAttribute(int index) : Attribute
{
    public int Index { get; private set; } = index;
}