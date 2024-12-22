using System.Numerics;

namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
    where T : struct, INumber<T>, IPowerFunctions<T>, IBitwiseOperators<T, T, T>, IModulusOperators<T, T, T>
{
    private static readonly Dictionary<int, (InstructionAttribute attr, Operation<T> op)> _dict = new();
    /// <summary>
    /// Wrapper for calling an instruction, taking into account its 
    /// <see cref="InstructionAttribute">metadata</see>, for example converting the raw 
    /// <paramref name="operand"/> to the 
    /// <see cref="Extensions.Value{T}(T, MemoryState{T}, OperandType)">value</see> appropriate to
    /// its <see cref="OperandType">type</see>.
    /// </summary>
    /// <param name="state">The state of the program when the instruction was reached.</param>
    /// <param name="opcode">The code of the <see cref="Operation{T}">operation</see> to perform.</param>
    /// <param name="operand">The operand the operation may use.</param>
    /// <returns>The state of the program after the operation was performed.</returns>
    internal static ProgramState<T> Call(ProgramState<T> state, int opcode, T operand)
    {
        (InstructionAttribute attr, Operation<T> op) = _dict[opcode];
        return op(state, operand.Value(state.Registers, attr.OperandType)).AdvancePointer(attr.PointerIncrement);
    }

}