namespace AdventOfCode2022
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleDayNumberAttribute : Attribute
    {
        public PuzzleDayNumberAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }

}
