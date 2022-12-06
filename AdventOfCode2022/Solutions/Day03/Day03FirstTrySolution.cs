namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(03)]
public class Day03FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 03;
    public override string SolvePartOne(in string[] inputLines)
    {
        var sum = 0;
        for (int i = 0; i < inputLines.Length; i++)
        {
            var first = inputLines[i].Substring(0, inputLines[i].Length / 2);
            var second = inputLines[i].Substring(inputLines[i].Length / 2);

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
        return sum.ToString();
    }

    public override string SolvePartTwo(in string[] inputLines)
    {
        var sum = 0;
        //foreach (var inputLine in inputLines)
        for (int i = 0; i < inputLines.Length / 3; i++)
        {
            var first = inputLines[i * 3 + 0];
            var second = inputLines[i * 3 + 1];
            var third = inputLines[i * 3 + 2];

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
        return sum.ToString();
    }

}
