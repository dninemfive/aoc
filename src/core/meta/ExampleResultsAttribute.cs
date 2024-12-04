namespace d9.aoc.core.meta;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExampleResultsAttribute(string exampleText, params object[] expectedResults)
    : Attribute
{
    public string ExampleText => exampleText;
    public object[] ExpectedResults => expectedResults;
}
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExampleResultsInFileAttribute(params object[] expectedResults)
    : Attribute
{
    public object[] ExpectedResults => expectedResults;
}