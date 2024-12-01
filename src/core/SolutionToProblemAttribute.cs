namespace d9.aoc.core;
/// <summary>
/// Specifies that the class on which the attribute is placed is a solution to an Advent of Code 
/// problem, allowing it to automatically be found by a <see cref="AocSolutionGroup"/> generated from
/// the assembly which contains it.
/// </summary>
/// <param name="day">The day the class is a solution for.</param>
/// <param name="startingIndex">The index of the first part. 1 by default, but sometimes zero if
/// there is a preinitialization part.</param>
[AttributeUsage(AttributeTargets.Class)]
public class SolutionToProblemAttribute(int day, int startingIndex = 1) : Attribute
{
    public int Day => day;
    public int StartingIndex => startingIndex;
}