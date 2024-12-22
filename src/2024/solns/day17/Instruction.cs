using System.Numerics;

namespace d9.aoc._24.day17;
internal enum OperandType
{
    Literal,
    Combo,
    Ignored
}
internal readonly struct MemoryState<T>(T a, T b, T c)
    where T : struct, INumber<T>
{
    internal readonly T A = a, B = b, C = c;
    internal MemoryState<T> CopyWith(T? a = null, T? b = null, T? c = null)
        => (a ?? A,
            b ?? B,
            c ?? C);
    public static implicit operator MemoryState<T>((T a, T b, T c) tuple)
        => new(tuple.a, tuple.b, tuple.c);
    internal void Deconstruct(out T a, out T b, out T c)
    {
        a = A;
        b = B;
        c = C;
    }
}
internal readonly struct ProgramState<T>(MemoryState<T> memory, int instructionPointer, IEnumerable<T>? output = null)
    where T : struct, INumber<T>
{
    public readonly MemoryState<T> Registers = memory;
    public readonly int InstructionPointer = instructionPointer;
    public readonly IEnumerable<T> Output = output ?? [];
    public ProgramState<T> AdvancePointer(int amount = 2)
        => CopyWith(ptr: InstructionPointer + amount);
    public ProgramState<T> CopyWith(T? a = null, T? b = null, T? c = null, int? ptr = null, T? output = null)
        => new(Registers.CopyWith(a, b, c), ptr ?? InstructionPointer, output is T t ? [..Output, t] : Output);
}
internal delegate ProgramState<T> Operation<T>(ProgramState<T> state, T operand)
    where T : struct, INumber<T>;
[AttributeUsage(AttributeTargets.Method)]
internal class InstructionAttribute(int opcode, OperandType operandType, int defaultPtrIncrement = 2)
    : Attribute
{
    internal readonly int Opcode = opcode, PointerIncrement = defaultPtrIncrement;
    internal readonly OperandType OperandType = operandType;
}
internal static class Instructions<T>
    where T : struct, INumber<T>, IPowerFunctions<T>, IBitwiseOperators<T, T, T>, IModulusOperators<T, T, T>
{
    private static readonly Dictionary<int, (InstructionAttribute attr, Operation<T> op)> _dict = new();
    internal static ProgramState<T> Call(ProgramState<T> state, int opcode, T operand)
    {
        (InstructionAttribute attr, Operation<T> op) = _dict[opcode];
        return op(state, operand.Value(state.Registers, attr.OperandType)).AdvancePointer(attr.PointerIncrement);
    }
    private static readonly T Two = T.CreateChecked(2), Eight = T.CreateChecked(8);
    private static T _dv(ProgramState<T> state, T operand)
    {
        T numerator = state.Registers.A;
        T denominator = T.Pow(Two, operand);
        return numerator / denominator;
    }
    [Instruction(opcode: 0, OperandType.Combo)]
    internal static ProgramState<T> Adv(ProgramState<T> state, T operand)
        => state.CopyWith(a: _dv(state, operand));
    [Instruction(opcode: 1, OperandType.Literal)]
    internal static ProgramState<T> Bxl(ProgramState<T> state, T operand)
        => state.CopyWith(b: state.Registers.B ^ operand);
    [Instruction(opcode: 2, OperandType.Combo)]
    internal static ProgramState<T> Bst(ProgramState<T> state, T operand)
        => state.CopyWith(b: operand % Eight);
    [Instruction(opcode: 3, OperandType.Literal, defaultPtrIncrement: 0)]
    internal static ProgramState<T> Jnz(ProgramState<T> state, T operand)
    {
        if (T.IsZero(state.Registers.A))
            return state.AdvancePointer();
        return state.CopyWith(ptr: int.CreateChecked(operand));
    }
    [Instruction(opcode: 4, OperandType.Ignored)]
    internal static ProgramState<T> Bxc(ProgramState<T> state, T _)
        => state.CopyWith(b: state.Registers.B ^ state.Registers.C);
    [Instruction(opcode: 5, OperandType.Combo)]
    internal static ProgramState<T> Out(ProgramState<T> state, T operand)
        => state.CopyWith(output: operand);
    [Instruction(opcode: 6, OperandType.Combo)]
    internal static ProgramState<T> Bdv(ProgramState<T> state, T operand)
        => state.CopyWith(b: _dv(state, operand));
    [Instruction(opcode: 7, OperandType.Combo)]
    internal static ProgramState<T> Cdv(ProgramState<T> state, T operand)
        => state.CopyWith(c: _dv(state, operand));

}
internal static class Extensions
{
    internal static T Value<T>(this T operand, MemoryState<T> memory, OperandType type = OperandType.Literal)
        where T : struct, INumber<T>
        => type switch
        {
            OperandType.Literal => operand,
            OperandType.Combo => operand switch
            {
                <= 3 => operand,
                4 => memory.A,
                5 => memory.B,
                6 => memory.C,
                7 => throw new ArgumentException("A combo operand can never have the value 7!", nameof(operand)),
                _ => throw new ArgumentOutOfRangeException(nameof(operand), $"Unrecognized combo operand: {operand}")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"{type} is not a recognized operand type!")
        };
}