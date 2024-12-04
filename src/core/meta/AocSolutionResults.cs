using System.Collections;

namespace d9.aoc.core;
public class AocSolutionResults
    : IEnumerable<AocSolutionResult>
{
    private readonly List<AocSolutionResult> _allResults = new();
    private readonly Dictionary<int, AocSolutionResult> _partsByIndex = new();
    public void Add(AocPartialResult result, TimeSpan elapsed, int? index = null)
    {
        if (index is int i)
        {
            if (_partsByIndex.ContainsKey(i))
                throw new ArgumentException($"Cannot add AocPartialResult {result}: results already contain index {i}!");
            AocSolutionResult newPart = new(result, $"Part {index,2}", elapsed);
            _partsByIndex[i] = newPart;
            _allResults.Add(newPart);
        }
        else
        {
            _allResults.Add(new(result, result.Label!, elapsed));
        }
    }

    public IEnumerator<AocSolutionResult> GetEnumerator()
        => ((IEnumerable<AocSolutionResult>)_allResults).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_allResults).GetEnumerator();
    public AocSolutionResult this[int index]
        => _partsByIndex[index];
    public IEnumerable<(int index, AocSolutionResult part)> Parts
        => _partsByIndex.OrderBy(x => x.Key).Select(x => (x.Key, x.Value));
}