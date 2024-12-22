namespace d9.aoc._24.day17;
/// <summary>
/// Specifies that a particular <see cref="Operation{T}"/> is a valid instruction and associates
/// certain metadata therewith.
/// </summary>
/// <param name="opcode"><inheritdoc cref="Opcode" path="/summary"/></param>
/// <param name="operandType"><inheritdoc cref="OperandType" path="/summary"/></param>
/// <param name="ptrIncrement"><inheritdoc cref="PointerIncrement" path="/summary"/></param>
[AttributeUsage(AttributeTargets.Method)]
internal class InstructionAttribute(int opcode, OperandType operandType, int ptrIncrement = 2)
    : Attribute
{
    /// <summary>
    /// The code which, when found in the program, will cause the operation to be run.
    /// </summary>
    internal readonly int Opcode = opcode;
    /// <summary>
    /// The <see cref="OperandType">type of operand</see> used by the operation.
    /// </summary>
    internal readonly int PointerIncrement = ptrIncrement;
    /// <summary>
    /// How much the program pointer will automatically increase after the operation is run.
    /// </summary>
    internal readonly OperandType OperandType = operandType;
}