namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
{
    private static readonly T Two = T.CreateChecked(2);
    private static T _dv(ProgramState<T> state, T operand)
    {
        T numerator = state.Registers.A;
        T denominator = T.Pow(Two, operand);
        return numerator / denominator;
    }
    [Instruction(opcode: 0, OperandType.Combo)]
    internal static ProgramState<T> Adv(ProgramState<T> state, T operand)
        => state.CopyWith(a: _dv(state, operand));
    [Instruction(opcode: 6, OperandType.Combo)]
    internal static ProgramState<T> Bdv(ProgramState<T> state, T operand)
        => state.CopyWith(b: _dv(state, operand));
    [Instruction(opcode: 7, OperandType.Combo)]
    internal static ProgramState<T> Cdv(ProgramState<T> state, T operand)
        => state.CopyWith(c: _dv(state, operand));
}
