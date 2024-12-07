using System.Numerics;

namespace d9.aoc._24.day07;
[SolutionToProblem(7)]
[SampleResults(3749)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<(IEnumerable<int> numbers, int expected)> Parse(string[] lines)
    {
        foreach(string line in lines)
        {
            string[] split = line.Split(": ");
            yield return (split[1].ToMany<int>(), int.Parse(split[0]));
        }
    }
    public delegate T Operation<T>(T l, T r)
        where T : INumber<T>;
    public T Add<T>(T l, T r)
        where T : INumber<T>
        => l + r;
    public T Multiply<T>(T l, T r)
        where T : INumber<T>
        => l + r;
    public IEnumerable<T> PossibleCombinations<T>(IEnumerable<T> operands)
        where T : INumber<T>
    {
        List<Operation<T>> operations = [Add, Multiply];
        foreach(IEnumerable<Operation<T>> combination in AllCombinationsOf(operations, operands.Count() - 1))
        {
            T result = operands.First();
            for(int i = 0; i < combination.Count(); i++)
            {
                result = combination.ElementAt(i)(result, operands.ElementAt(i + 1));
            }
            yield return result;
        }
    }
    public IEnumerable<IEnumerable<T>> AllCombinationsOf<T>(IEnumerable<T> options, int count)
    {
        if (count == 1)
            yield return options;
        throw new NotImplementedException();
    }
}
