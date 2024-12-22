namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
{
    /// <summary>
    /// The <b><c>bxl</c></b> instruction (opcode <c>1</c>) calculates the
    /// <see href="https://en.wikipedia.org/wiki/Bitwise_operation#XOR">bitwise XOR</see> of
    /// register <c>B</c> and the instruction's <b>literal</b> operand, then stores the result in 
    /// register <c>B</c>.
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks><inheritdoc cref="Operation{T}" path="/remarks"/></remarks>
    [Instruction(opcode: 1, OperandType.Literal)]
    internal static ProgramState<T> Bxl(ProgramState<T> state, T operand)
        => state.CopyWith(b: state.Registers.B ^ operand);
    /// <summary>
    /// The <b><c>bxl</c></b> instruction (opcode <c>4</c>) calculates the
    /// <see href="https://en.wikipedia.org/wiki/Bitwise_operation#XOR">bitwise XOR</see> of
    /// register <c>B</c> and register <c>C</c>, then stores the result in register <c>B</c>.
    /// (For legacy reasons, this instruction reads an operand but <b>ignores</b> it.)
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="_">Ignored.</param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks><inheritdoc cref="Operation{T}" path="/remarks"/></remarks>
    [Instruction(opcode: 4, OperandType.Ignored)]
    internal static ProgramState<T> Bxc(ProgramState<T> state, T _)
        => state.CopyWith(b: state.Registers.B ^ state.Registers.C);
}
