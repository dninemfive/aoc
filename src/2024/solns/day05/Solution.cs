namespace d9.aoc._24.day05;
[SolutionToProblem(5)]
[SampleResults(143)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        List<Order> orders = new();
        foreach(string line in lines)
        {
            if (line.IsNullOrEmpty())
                break;
            orders.Add(new Order(line));
        }
        foreach(Order order in orders.Order())
            Console.WriteLine(order);
        yield return "preinit";
    }
}
public class Order(int first, int second)
    : IComparable<Order>
{
    public int First => first;
    public int Second => second;
    public Order(IEnumerable<int> ints) : this(ints.First(), ints.Second()) { }
    public Order(string line) : this(line.ToMany<int>("|")) { }
    public int CompareTo(Order? other)
    {
        if(other is Order order)
        {
            int cmp = First.CompareTo(other.First);
            if (cmp != 0)
                return cmp;
            return Second.CompareTo(other.First);
        }
        return -1;
    }
    public override string ToString()
        => $"{First}|{Second}";
}
