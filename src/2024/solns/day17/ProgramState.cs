using System.Numerics;

namespace d9.aoc._24.day17;
internal readonly struct ProgramState<T>(MemoryState<T> memory, int instructionPointer, IEnumerable<T>? output = null)
    where T : struct, INumber<T>
{
    public readonly MemoryState<T> Registers = memory;
    public readonly int InstructionPointer = instructionPointer;
    public readonly IEnumerable<T> Output = output ?? [];
    public ProgramState<T> AdvancePointer(int amount = 2)
        => CopyWith(ptr: InstructionPointer + amount);
    public ProgramState<T> CopyWith(T? a = null, T? b = null, T? c = null, int? ptr = null, T? output = null)
        => new(Registers.CopyWith(a, b, c), ptr ?? InstructionPointer, output is T t ? [.. Output, t] : Output);
}