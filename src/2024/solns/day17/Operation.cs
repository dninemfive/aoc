using System.Numerics;

namespace d9.aoc._24.day17;
/// <summary>
/// Signature for an operation on a <see cref="ProgramState{T}"/> which modifies the state in some way.
/// </summary>
/// <typeparam name="T">
/// The type of the values in the <paramref name="state"/>, including the <paramref name="operand"/>.
/// </typeparam>
/// <param name="state">The <see cref="ProgramState{T}"/> the operation will modify.</param>
/// <param name="operand">An argument used by the operation to modify the <paramref name="state"/>.</param>
/// <returns>The modified program state.</returns>
/// <remarks>
/// <b>Note</b> that common behaviors like advancing the instruction pointer are expected to be handled
/// by <see cref="Instructions{T}.Call(ProgramState{T}, int, T)"/> in conjunction with the operation's
/// <see cref="InstructionAttribute"/> and should not be implemented manually unless necessary.
/// </remarks>
internal delegate ProgramState<T> Operation<T>(ProgramState<T> state, T operand)
    where T : struct, INumber<T>;
