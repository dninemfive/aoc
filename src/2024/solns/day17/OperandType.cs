namespace d9.aoc._24.day17;
/// <summary>
/// The type of operand used by an instruction; this is resolved by
/// <see cref="Instructions{T}">Instructions</see><c>.<see cref="Instructions{T}.Call(ProgramState{T}, int, T)">Call</see>()</c>
/// in conjunction with
/// <c><see cref="Extensions"/>.<see cref="Extensions.Value{T}(T, MemoryState{T}, OperandType)">Value</see>()</c>.
/// </summary>
internal enum OperandType
{
    /// <summary>The value of a <b>literal operand</b> is the operand itself.</summary>
    /// <remarks>For example, the value of the literal operand <c>7</c> is the number <c>7</c>.</remarks>
    Literal,
    /// <summary>
    /// The value of a <b>combo operand</b> can be found as follows:
    /// <br/><br/>
    /// - Combo operands <c>0</c> through <c>3</c> represent literal values <c>0</c> through <c>3</c>.
    /// <br/>- Combo operand <c>4</c> represents the value of register <c>A</c>.
    /// <br/>- Combo operand <c>5</c> represents the value of register <c>B</c>.
    /// <br/>- Combo operand <c>6</c> represents the value of register <c>C</c>.
    /// <br/>- Combo operand <c>7</c> is reserved and will not appear in valid programs.
    /// </summary>
    Combo,
    /// <summary>
    /// Not defined in AOC 2024 Day 17, but used to represent an instruction which ignores its operand.
    /// </summary>
    Ignored
}