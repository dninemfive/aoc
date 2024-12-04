namespace d9.aoc.core.meta;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExampleResultsAttribute(params object[] expectedResults)
    : Attribute
{
    public object[] ExpectedResults => expectedResults;
}