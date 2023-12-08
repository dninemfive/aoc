namespace d9.aoc._23.day8;
public class Tape(string s)
{
    public readonly string Items = s.All(x => x is 'L' or 'R') ? s
        : throw new ArgumentException("String for a Tape must consist only of characters L and R!");
    private int _index = 0;
    public int Index
    {
        get => _index;
        set
        {
            _index = (value < 0, value >= Items.Length) switch
            {
                (true, _) => Items.Length - 1,
                (_, true) => 0,
                _ => value
            };
        }
    }
    public char Advance() => Items[Index++];
}