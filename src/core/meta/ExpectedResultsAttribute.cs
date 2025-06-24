namespace d9.aoc.core;
[AttributeUsage(AttributeTargets.Method)]
public class ExpectedResultsAttribute(object? sample = null, object? final = null)
    : Attribute
{
    public object? Sample => sample;
    public object? Final => final;
}