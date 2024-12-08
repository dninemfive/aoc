using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    //    => rules.Any(Violates);
    {
        foreach(Rule rule in rules)
            if(Violates(rule))
            {
                Console.WriteLine($"{this} violates rule {rule}");
                return true;
            }
        Console.WriteLine($"{this} does not violate any rule.");
        return false;
    }
    public int MiddleValue //=> _items[_items.Count / 2];
    {
        get
        {
            int result = _items[_items.Count / 2];
            Console.WriteLine($"{this} middle value: {result}");
            return result;
        }
    }
    public override string ToString()
        => _items.ListNotation(brackets: null);
}
