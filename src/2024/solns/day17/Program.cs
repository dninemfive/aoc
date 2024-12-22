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
    public (int opcode, T operand)? NextOperation
    {
        get
        {
            if (State.InstructionPointer + 1 < Instructions.Count)
            {
                return (int.CreateChecked(Instructions[State.InstructionPointer]),
                                          Instructions[State.InstructionPointer + 1]);
            }
            else return null;
        }
    }
    public bool RunNextInstruction()
    {
        if (NextOperation is (int opcode, T operand))
        {
            State = Instructions<T>.Call(State, opcode, operand);
            return true;
        }
        else return false;
    }
    public void RunToCompletion()
    {
        while (RunNextInstruction())
            ;
    }
    public string OutputString => State.Output.Select(x => x.ToString()!)
                                              .JoinWithDelimiter(",");
}
