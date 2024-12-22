namespace d9.aoc._24.day17;
internal static partial class Instructions<T>
{
    private static readonly T _two = T.CreateChecked(2);
    private static T _dv(ProgramState<T> state, T operand)
    {
        T numerator = state.Registers.A;
        T denominator = T.Pow(_two, operand);
        return numerator / denominator;
    }
    /// <summary>
    /// The <b><c>adv</c></b> instruction (opcode <b><c>0</c></b>) performs <b>division</b>. 
    /// The numerator is the value in the <c>A</c> register.
    /// The denominator is found by raising 2 to the power of the instruction's 
    /// <see cref="OperandType.Combo">combo</see> <paramref name="operand"/>. 
    /// (So, an operand of <c>2</c> would divide <c>A</c> by <c>4</c> (<c>2^2</c>); an operand of 
    /// <c>5</c> would divide <c>A</c> by <c>2^B</c>.)
    /// The result of the division operation is <b>truncated</b> to an integer and then written to 
    /// the A register.
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks><inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/></remarks>
    [Instruction(opcode: 0, OperandType.Combo)]
    internal static ProgramState<T> Adv(ProgramState<T> state, T operand)
        => state.CopyWith(a: _dv(state, operand));
    /// <summary>
    /// The <b><c>bdv</c></b> instruction (opcode <b><c>6</c></b>) works exactly like the 
    /// <see cref="Adv(ProgramState{T}, T)">adv</see> instruction except that the result 
    /// is stored in the <b><c>B</c> register</b>. 
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks>
    /// (The numerator is still read from the <c>A</c> register.)
    /// <br/><br/>
    /// <inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/>
    /// </remarks>
    [Instruction(opcode: 6, OperandType.Combo)]
    internal static ProgramState<T> Bdv(ProgramState<T> state, T operand)
        => state.CopyWith(b: _dv(state, operand));
    /// <summary>
    /// The <b><c>cdv</c></b> instruction (opcode <b><c>7</c></b>) works exactly like the 
    /// <see cref="Adv(ProgramState{T}, T)">adv</see> instruction except that the result 
    /// is stored in the <b><c>C</c> register</b>. 
    /// </summary>
    /// <param name="state"><inheritdoc cref="Operation{T}" path="/param[@name='state']"/></param>
    /// <param name="operand"><inheritdoc cref="Operation{T}" path="/param[@name='operand']"/></param>
    /// <returns><inheritdoc cref="Operation{T}" path="/returns"/></returns>
    /// <remarks>
    /// (The numerator is still read from the <c>A</c> register.)
    /// <br/><br/>
    /// <inheritdoc cref="Operation{T}" path="/instructionRemarkMsg"/>
    /// </remarks>
    [Instruction(opcode: 7, OperandType.Combo)]
    internal static ProgramState<T> Cdv(ProgramState<T> state, T operand)
        => state.CopyWith(c: _dv(state, operand));
}
