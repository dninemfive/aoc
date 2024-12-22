using System.Numerics;

namespace d9.aoc._24.day17;
internal class Program<T>(ProgramState<T> initialState, IEnumerable<T> instructions)
    where T : struct, INumber<T>,
                      IPowerFunctions<T>,
                      IBitwiseOperators<T, T, T>,
                      IModulusOperators<T, T, T>
{
    public ProgramState<T> State = initialState;
    public IList<T> Instructions = instructions.ToList();
    public bool RunNextInstruction()
    {
        if(State.InstructionPointer + 1 < Instructions.Count)
        {
            int opcode = int.CreateChecked(Instructions[State.InstructionPointer]);
        }
        else return false;
    }
}
