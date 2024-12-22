namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
{
    private static readonly T Eight = T.CreateChecked(8);
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
    [Instruction(opcode: 5, OperandType.Combo)]
    internal static ProgramState<T> Out(ProgramState<T> state, T operand)
        => state.CopyWith(output: operand);
}
