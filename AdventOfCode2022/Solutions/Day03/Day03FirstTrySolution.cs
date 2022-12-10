namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(03)]
public class Day03FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 03;
    public override object SolvePartOne(string[] lines)
    {
        var sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var first = lines[i].Substring(0, lines[i].Length / 2);
            var second = lines[i].Substring(lines[i].Length / 2);

            var repeted = ' ';
            foreach (var item in first)
            {
                if (second.Contains(item))
                {
                    repeted = item;
                    break;
                }
            }

            if (char.IsLower(repeted))
            {
                sum += (repeted - 'a') + 1;
            }
            else
            {
                sum += (repeted - 'A') + 27;
            }

        }
        return sum;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var sum = 0;
        for (int i = 0; i < lines.Length / 3; i++)
        {
            var first = lines[i * 3 + 0];
            var second = lines[i * 3 + 1];
            var third = lines[i * 3 + 2];

            var repeted = ' ';
            foreach (var item in first)
            {
                if (second.Contains(item) && third.Contains(item))
                {
                    repeted = item;
                    break;
                }
            }

            if (char.IsLower(repeted))
            {
                sum += (repeted - 'a') + 1;
            }
            else
            {
                sum += (repeted - 'A') + 27;
            }

        }
        return sum;
    }

}
