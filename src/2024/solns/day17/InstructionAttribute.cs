namespace d9.aoc._24.day17;
[AttributeUsage(AttributeTargets.Method)]
internal class InstructionAttribute(int opcode, OperandType operandType, int defaultPtrIncrement = 2)
    : Attribute
{
    internal readonly int Opcode = opcode, PointerIncrement = defaultPtrIncrement;
    internal readonly OperandType OperandType = operandType;
}