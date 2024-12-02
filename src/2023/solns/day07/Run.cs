namespace d9.aoc._23.day07;
// basically just a tuple, but "Run" is a lot shorter than "(CamelCard card, int count)"
public readonly struct Run(CamelCard card, int ct)
{
    public readonly CamelCard Card = card;
    public readonly int Count = ct;
    public void Deconstruct(out CamelCard card, out int count)
    {
        card = Card;
        count = Count;
    }
    public static implicit operator Run((CamelCard card, int ct) tuple)
        => new(tuple.card, tuple.ct);
    public static Run operator +(Run run, int amt)
        => (run.Card, run.Count + amt);
}