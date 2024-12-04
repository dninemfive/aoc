namespace d9.aoc.core;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FinalResultsAttribute(params object[] expectedResults) : Attribute
{
    public object[] ExpectedResults => expectedResults;
}
