namespace d9.aoc.core;
[AttributeUsage(AttributeTargets.Method)]
public class ExpectedResultsAttribute(object? sample, object? final)
    : Attribute
{
    public ExpectedResultsAttribute(object? final)
        : this(null, final) { }
    public object? Sample => sample;
    public object? Final => final;
    public override string ToString()
        => $"E(S: {Sample}, F: {Final})";
}