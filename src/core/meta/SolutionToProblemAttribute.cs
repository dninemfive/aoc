namespace d9.aoc.core;
/// <summary>
/// Specifies that the class on which the attribute is placed is a solution to an Advent of Code 
/// problem, allowing it to automatically be found by a <see cref="AocSolutionGroup"/> generated from
/// the assembly which contains it.
/// </summary>
/// <param name="day">The day the class is a solution for.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SolutionToProblemAttribute(int day, bool complete = false) : Attribute
{
    public int Day => day;
    /// <summary>
    /// If true, the solution is not executed when the project is run in order to save time.
    /// </summary>
    public bool Complete => complete;
}