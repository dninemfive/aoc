using System.Numerics;

namespace d9.aoc._24.day17;
internal static class Extensions
{
    internal static T Value<T>(this T operand, MemoryState<T> memory, OperandType type = OperandType.Literal)
        where T : struct, INumber<T>
        => type switch
        {
            OperandType.Literal => operand,
            OperandType.Combo => operand switch
            {
                <= 3 => operand,
                4 => memory.A,
                5 => memory.B,
                6 => memory.C,
                7 => throw new ArgumentException("A combo operand can never have the value 7!", nameof(operand)),
                _ => throw new ArgumentOutOfRangeException(nameof(operand), $"Unrecognized combo operand: {operand}")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"{type} is not a recognized operand type!")
        };
}