using System.Numerics;

namespace d9.aoc._24.day17;
internal static class Extensions
{
    /// <summary>
    /// Determines the ultimate value of an <paramref name="operand"/> as specified by its
    /// <see cref="OperandType"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Type"/> of the <paramref name="operand"/> whose value to find.
    /// </typeparam>
    /// <param name="operand">
    /// The operand whose value to find.
    /// </param>
    /// <param name="memory">
    /// The state of the program's registers when the value of the <paramref name="operand"/> is
    /// evaluated.
    /// </param>
    /// <param name="type">The <see cref="OperandType"/> of the <paramref name="operand"/> whose 
    /// value to find.</param>
    /// <returns>
    /// The final value of the specified <paramref name="operand"/> as specified by its 
    /// <paramref name="type"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="operand"/> is a <see cref="OperandType.Combo">combo operand</see>
    /// and it has the reserved value <c>7</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the <paramref name="type"/> is unrecognized, or if the <paramref name="operand"/>
    /// is a <see cref="OperandType.Combo">combo operand</see> and it has an unrecognized (and not 
    /// reserved) value.
    /// </exception>
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
            OperandType.Ignored => default,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"{type} is not a recognized operand type!")
        };
    internal static T Pow<T>(this T @base, T exponent)
        where T : INumber<T>
    {
        T result = T.One;
        for (T i = T.Zero; i < exponent; i++)
            result *= @base;
        return result;
    }
}