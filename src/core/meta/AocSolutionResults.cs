using System.Collections;

namespace d9.aoc.core;
public class AocSolutionResults
    : IEnumerable<AocPartResult>
{
    private readonly List<AocPartResult> _allResults = new();
    private readonly Dictionary<int, AocPartResult> _partsByIndex = new();
    public void Add(AocPartResultValue result, TimeSpan elapsed, int? index = null)
    {
        if (index is int i)
        {
            if (_partsByIndex.ContainsKey(i))
                throw new ArgumentException($"Cannot add AocPartialResult {result}: results already contain index {i}!");
            AocPartResult newPart = new(result, $"Part {index,2}", elapsed);
            _partsByIndex[i] = newPart;
            _allResults.Add(newPart);
        }
        else
        {
            _allResults.Add(new(result, result.Label!, elapsed));
        }
    }

    public IEnumerator<AocPartResult> GetEnumerator()
        => ((IEnumerable<AocPartResult>)_allResults).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_allResults).GetEnumerator();
    public AocPartResult this[int index]
        => _partsByIndex[index];
    public IEnumerable<(int index, AocPartResult part)> Parts
        => _partsByIndex.OrderBy(x => x.Key).Select(x => (x.Key, x.Value));
}