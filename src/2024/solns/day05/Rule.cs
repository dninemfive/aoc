namespace d9.aoc._24.day05;
public class Rule(int first, int second)
{
    public int First => first;
    public int Second => second;
    public Rule(IEnumerable<int> ints) : this(ints.First(), ints.Second()) { }
    public Rule(string line) : this(line.ToMany<int>("|")) { }
    public override string ToString()
        => $"{First}|{Second}";
    public bool? ComesBefore(Rule other)
    {
        if (First == other.Second)
            return false;
        if (Second == other.First)
            return true;
        return null;
    }
    public void Deconstruct(out int first, out int second)
    {
        first = First;
        second = Second;
    }
}
