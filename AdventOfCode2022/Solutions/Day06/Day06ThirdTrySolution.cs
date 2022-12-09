using System.Net.Sockets;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(06)]
public class Day06ThirdTrySolution : PuzzleSolution
{
    public override int DayNumber => 06;
    public override object SolvePartOne(in string[] inputLines)
        => ReportStartOfPacket(inputLines[0].AsSpan(), 4);

    public override object SolvePartTwo(in string[] inputLines)
        => ReportStartOfPacket(inputLines[0].AsSpan(), 14);

    private static object ReportStartOfPacket(ReadOnlySpan<char> datastream, int length)
    {
        var set = new HashSet<char>(length);
        for (int i = 0; i < datastream.Length - length; i++)
        {
            set.Clear();
            foreach (var item in datastream[i..(i + length)])
                if (!set.Add(item))
                    break;

            if (set.Count == length)
                return (i + length);
        }
        return string.Empty;
    }
}
