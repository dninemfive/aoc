namespace d9.aoc._23;
/// <summary>
/// When applied to a method with a signature of 
/// <inheritdoc cref="Program.HasAppropriateSignature(System.Reflection.MethodInfo)" path="/sig"/>,
/// causes <see cref="Program.Main"/> to run it along with other matching methods in order of
/// <paramref name="index"/>.
/// </summary>
/// <param name="index">Which problem the method is a solution for.</param>
[AttributeUsage(AttributeTargets.Method)]
public class SolutionToProblemAttribute(int index, bool hasStartupMarker = false) : Attribute
{
    public int Index { get; private set; } = index;
    /// <summary>
    /// If <see langword="true"/>, indicates that the method yields an item which should be ignored and is only used to
    /// measure time used to preprocess data before working on the main solution.
    /// </summary>
    public bool HasStartupMarker { get; private set; } = hasStartupMarker;
}