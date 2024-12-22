using System.Numerics;

namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
    where T : struct, INumber<T>, IPowerFunctions<T>, IBitwiseOperators<T, T, T>, IModulusOperators<T, T, T>
{
    private static readonly Dictionary<int, (InstructionAttribute attr, Operation<T> op)> _dict = new();
    internal static ProgramState<T> Call(ProgramState<T> state, int opcode, T operand)
    {
        (InstructionAttribute attr, Operation<T> op) = _dict[opcode];
        return op(state, operand.Value(state.Registers, attr.OperandType)).AdvancePointer(attr.PointerIncrement);
    }

}