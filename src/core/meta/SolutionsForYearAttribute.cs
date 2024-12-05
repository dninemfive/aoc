namespace d9.aoc.core;
/// <summary>
/// Specifies that the assembly on which the attribute is placed contains solutions to Advent of Code
/// problems for a particular year, allowing for an <see cref="AocSolutionGroup"/> to be automatically
/// generated from it.
/// </summary>
/// <param name="year">The year the solutions in the assembly are for.</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public class SolutionsForYearAttribute(int year) : Attribute
{
    public int Year => year;
}