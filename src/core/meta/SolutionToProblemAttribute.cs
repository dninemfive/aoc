namespace d9.aoc.core;
/// <summary>
/// Specifies that the class on which the attribute is placed is a solution to an Advent of Code 
/// problem, allowing it to automatically be found by a <see cref="AocSolutionGroup"/> generated from
/// the assembly which contains it.
/// </summary>
/// <param name="day">The day the class is a solution for.</param>
[AttributeUsage(AttributeTargets.Class)]
public class SolutionToProblemAttribute(int day) : Attribute
{
    public int Day => day;
}