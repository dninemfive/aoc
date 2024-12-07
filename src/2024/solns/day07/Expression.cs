using System.Numerics;

namespace d9.aoc._24.day07;
internal class Expression<T>
    where T : INumber<T>
{
    private readonly IReadOnlyList<T> _operands;
    private readonly IReadOnlyList<Operator<T>> _operators;
    public int OperandCount => _operands.Count;
    private static string _discrepancyMessage(IEnumerable<T> operands, IEnumerable<Operator<T>> operators)
    {
        string result = "There must be exactly one fewer operation than the number of operands in an equation!";
        result += $"\nGot {operands.ListNotation()} ({operands.Count()}) and {operators.ListNotation()} ({operands.Count()}) instead.";
        return result;
    }
    public Expression(IEnumerable<T> operands, IEnumerable<Operator<T>> operators)
    {
        if (operands.Count() - 1 != operators.Count())
            throw new ArgumentOutOfRangeException(nameof(operators),
                                                  _discrepancyMessage(operands, operators));
        _operands = operands.ToList();
        _operators = operators.ToList();
    }
    public Expression(T firstOperand, params (string opName, T operand)[] remainder)
    {
        _operands = [firstOperand, .. remainder.Select(x => x.operand)];
        _operators = remainder.Select(x => Operators<T>
                               .ByName(x.opName))
                               .ToList();
    }
    public override string ToString()
    {
        string result = $"{_operands[0]}";
        for(int i = 1; i < OperandCount; i++)
            result += $" {_operators[i - 1]} {_operands[i]}";
        return result;
    }
    public T Evaluate()
    {
        T result = _operands.First();
        for (int i = 1; i < OperandCount; i++)
            result = ((OperatorDelegate<T>)_operators[i - 1])(result, _operands[i]);
        return result;
    }
    public static implicit operator T(Expression<T> expr)
        => expr.Evaluate();
}
