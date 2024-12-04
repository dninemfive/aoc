using System.Collections;

namespace d9.aoc.core;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class ExpectedResultsAttribute(params object[] expectedResults)
    : Attribute, IEnumerable<(int index, object expectedResult)>
{
    public abstract bool UseSampleData { get; }
    public object[] ExpectedResults => expectedResults;
    private IEnumerable<(int index, object expectedResult)> _enumerable
    {
        get
        {
            for (int i = 0; i < ExpectedResults.Length; i++)
                // the index is the *part* index, not the result index, since expectedResult is yielded as well
                yield return (i + 1, expectedResults[i]);
        }
    }
    public IEnumerator<(int index, object expectedResult)> GetEnumerator()
        => _enumerable.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => _enumerable.GetEnumerator();
    public static implicit operator object[](ExpectedResultsAttribute attr)
        => attr.ExpectedResults;
}
public class FinalResultsAttribute(params object[] expectedResults)
    : ExpectedResultsAttribute(expectedResults)
{
    public override bool UseSampleData => false;
}
public class SampleResultsAttribute(params object[] expectedResults)
    : ExpectedResultsAttribute(expectedResults)
{
    public override bool UseSampleData => true;
}