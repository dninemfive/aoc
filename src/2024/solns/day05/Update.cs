namespace d9.aoc._24.day05;
internal class Update(IEnumerable<int> items)
{
    private readonly IReadOnlyList<int> _items = items.ToList();
    internal Update(string line) : this(line.ToMany<int>(",")) { }
    public bool Violates(Rule rule)
    {
        if (!_items.Contains(rule.First) || !_items.Contains(rule.Second))
            return false;
        bool foundFirst = false;
        foreach(int i in _items)
        {
            if (i == rule.First)
                foundFirst = true;
            if (i == rule.Second)
            {
                return !foundFirst;
            }
        }
        return false;
    }
    public bool ViolatesAny(IEnumerable<Rule> rules)
        => rules.Any(Violates);
    public int MiddleValue
        => _items[_items.Count / 2];
    public override string ToString()
        => _items.ListNotation(brackets: null);
}
