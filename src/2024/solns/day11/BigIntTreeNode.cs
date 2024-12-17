using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace d9.aoc._24.day11;
public readonly struct BigIntTreeNode(IEnumerable<BigInteger> values)
    : IEnumerable<BigInteger>
{
    public readonly IEnumerable<BigInteger> Values = values;
    public readonly byte Increase = (byte)(values.Count() - 1);
    public IEnumerator<BigInteger> GetEnumerator()
        => Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)Values).GetEnumerator();
}
public class StoneSuccessorCache
{
    private readonly Dictionary<BigInteger, BigIntTreeNode> _cache = new();
    public bool TryGet(BigInteger key, [NotNullWhen(true)] out BigIntTreeNode value)
        => _cache.TryGetValue(key, out value);
    public BigIntTreeNode GetOrGenerate(BigInteger key)
    {
        if (TryGet(key, out BigIntTreeNode value))
            return value;
        BigIntTreeNode result = new(ReplacementRules.ApplyFirst(key));
        _cache[key] = result;
        return result;
    }
    public IEnumerable<BigIntTreeNode> DepthFirstTree(BigInteger root, int maxDepth = 25)
    {
        if (maxDepth <= 0)
            yield break;
        BigIntTreeNode result = GetOrGenerate(root);
        foreach (BigInteger leafKey in result)
            foreach (BigIntTreeNode leaf in DepthFirstTree(leafKey, maxDepth - 1))
                yield return leaf;
        yield return result;
    }
    public IEnumerable<BigIntTreeNode> BreadthFirstTree(BigInteger root, int maxDepth = 25)
    {
        if (maxDepth <= 0)
            yield break;
        BigIntTreeNode result = GetOrGenerate(root);
        yield return result;
        foreach (BigInteger leafKey in result)
            foreach (BigIntTreeNode leaf in DepthFirstTree(leafKey, maxDepth - 1))
                yield return leaf;
    }
    public BigInteger CountTree(BigInteger root, int maxDepth = 25)
    {
        string filePath = Path.Join(Solution.DebugFolder, $"_Day11_tree_progress_{root}_{maxDepth}.txt");
        File.WriteAllText(filePath, $"{DateTime.Now,16:u}\tCountTree({root}, {maxDepth})\n");
        BigInteger result = 0;
        foreach (BigIntTreeNode bitn in DepthFirstTree(root, maxDepth))
        {
            result += bitn.Increase;
            if (BigInteger.IsPow2(result) && bitn.Increase > 0)
                File.AppendAllText(filePath, $"{DateTime.Now,16:u}\t\t{result,16}\n");
        }
        return result;
    }
}
