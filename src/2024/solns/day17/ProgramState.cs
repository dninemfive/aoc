using System.Numerics;

namespace d9.aoc._24.day17;
/// <summary>
/// Represents the state of a program at any given point.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="memory"></param>
/// <param name="instructionPointer"></param>
/// <param name="output"></param>
internal readonly struct ProgramState<T>(MemoryState<T> memory, int instructionPointer, IEnumerable<T>? output = null)
    where T : struct, INumber<T>
{
    /// <summary>
    /// The program's working memory.
    /// </summary>
    public readonly MemoryState<T> Registers = memory;
    /// <summary>
    /// The index of the next instruction to be read.
    /// </summary>
    public readonly int InstructionPointer = instructionPointer;
    /// <summary>
    /// All of the output which has been performed up to this point in the program's state.
    /// </summary>
    public readonly IEnumerable<T> Output = output ?? [];
    /// <summary>
    /// Advances the <see cref="InstructionPointer"/> by the specified <paramref name="amount"/>.
    /// </summary>
    /// <param name="amount">How much to increment the instruction pointer.</param>
    /// <returns>The new state after advancing the pointer.</returns>
    public ProgramState<T> AdvancePointer(int amount = 2)
        => CopyWith(ptr: InstructionPointer + amount);
    /// <summary>
    /// Copies the state with the specified changes.
    /// </summary>
    /// <param name="a"><inheritdoc cref="MemoryState{T}.CopyWith(T?, T?, T?)" path="/param[@name='a']"/></param>
    /// <param name="b"><inheritdoc cref="MemoryState{T}.CopyWith(T?, T?, T?)" path="/param[@name='b']"/></param>
    /// <param name="c"><inheritdoc cref="MemoryState{T}.CopyWith(T?, T?, T?)" path="/param[@name='c']"/></param>
    /// <param name="ptr">The new value for the <see cref="InstructionPointer">instruction pointer</see>, if any.</param>
    /// <param name="output">
    /// If non-<see langword="null"/>, the specified value will be appended to the preceding 
    /// <see cref="Output">output</see>.
    /// </param>
    /// <returns>A new state with the specified changes.</returns>
    public ProgramState<T> CopyWith(T? a = null, T? b = null, T? c = null, int? ptr = null, T? output = null)
        => new(Registers.CopyWith(a, b, c), ptr ?? InstructionPointer, output is T t ? [.. Output, t] : Output);
}