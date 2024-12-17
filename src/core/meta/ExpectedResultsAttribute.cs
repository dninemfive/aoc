using System.Collections;

namespace d9.aoc.core;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class ExpectedResultsAttribute<T>(params T[] expectedResults)
    : Attribute, IEnumerable<(int index, T expectedResult)>
{
    public abstract bool UseSampleData { get; }
    public T[] ExpectedResults => expectedResults;
    private IEnumerable<(int index, T expectedResult)> _enumerable
    {
        get
        {
            for (int i = 0; i < ExpectedResults.Length; i++)
                // the index is the *part* index, not the result index, since expectedResult is yielded as well
                yield return (i + 1, expectedResults[i]);
        }
    }
    public IEnumerator<(int index, T expectedResult)> GetEnumerator()
        => _enumerable.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => _enumerable.GetEnumerator();
    public static implicit operator T[](ExpectedResultsAttribute<T> attr)
        => attr.ExpectedResults;
}
public class FinalResultsAttribute(params object[] expectedResults)
    : ExpectedResultsAttribute<object>(expectedResults)
{
    public override bool UseSampleData => false;
}
public class FinalResultsAttribute<T>(params T[] expectedResults)
    : ExpectedResultsAttribute<T>(expectedResults)
{
    public override bool UseSampleData => false;
}
public class SampleResultsAttribute(params object[] expectedResults)
    : ExpectedResultsAttribute<object>(expectedResults)
{
    public override bool UseSampleData => true;
}
public class SampleResultsAttribute<T>(params T[] expectedResults)
    : ExpectedResultsAttribute<T>(expectedResults)
{
    public override bool UseSampleData => false;
}