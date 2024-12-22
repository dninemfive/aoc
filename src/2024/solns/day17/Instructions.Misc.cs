namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
{
    private static readonly T Eight = T.CreateChecked(8);
    /// <summary>
    /// The <b><c>bst</c></b> instruction (opcode <b><c>2</c></b>) calculates the value of its 
    /// <b>combo</b> operand <see href="https://en.wikipedia.org/wiki/Modulo">modulo</see> 8 
    /// (thereby keeping only its lowest 3 bits), then writes that value to the <c>B</c> register.
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks><inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/></remarks>
    [Instruction(opcode: 2, OperandType.Combo)]
    internal static ProgramState<T> Bst(ProgramState<T> state, T operand)
        => state.CopyWith(b: operand % Eight);
    /// <summary>
    /// The <b><c>jnz</c></b> instruction (opcode <b><c>3</c></b>) does nothing if the <c>A</c> 
    /// register is <c>0</c>. However, if the <c>A</c> register is <b>not zero</b>, it <b>jumps</b>
    /// by setting the instruction pointer to the value of its <b>literal</b> operand; if this 
    /// instruction jumps, the instruction pointer is <b>not</b> increased by <c>2</c> after this 
    /// instruction.
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks><inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/></remarks>
    [Instruction(opcode: 3, OperandType.Literal, ptrIncrement: 0)]
    internal static ProgramState<T> Jnz(ProgramState<T> state, T operand)
    {
        if (T.IsZero(state.Registers.A))
            return state.AdvancePointer();
        return state.CopyWith(ptr: int.CreateChecked(operand));
    }
    /// <summary>
    /// The <b><c>out</c></b> instruction (opcode <b><c>5</c></b>) calculates the value of its 
    /// <b>combo</b> operand modulo 8, then <b>outputs</b> that value.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="operand"></param>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks>
    /// (If a program outputs multiple values, they are separated by commas.)
    /// <br/><br/>
    /// <inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/>
    /// </remarks>
    [Instruction(opcode: 5, OperandType.Combo)]
    internal static ProgramState<T> Out(ProgramState<T> state, T operand)
        => state.CopyWith(output: operand % Eight);
}
