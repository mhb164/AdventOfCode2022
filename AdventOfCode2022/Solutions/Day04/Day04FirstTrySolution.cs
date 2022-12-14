namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(04)]
public class Day04FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 04;
    public override object SolvePartOne(string[] lines)
    {
        var result = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var first = lines[i].Split(',')[0];
            var second = lines[i].Split(',')[1];

            var first1 = int.Parse(first.Split('-')[0].ToString());
            var first2 = int.Parse(first.Split('-')[1].ToString());

            var firstdigits = new List<int>();
            for (int x = first1; x <= first2; x++)
            {
                firstdigits.Add(x);
            }

            var second1 = int.Parse(second.Split('-')[0].ToString());
            var second2 = int.Parse(second.Split('-')[1].ToString());

            var seconddigits = new List<int>();
            for (int x = second1; x <= second2; x++)
            {
                seconddigits.Add(x);
            }

            if (firstdigits.Intersect(seconddigits).Count() > 2)
            {
                result++;
            }
          
        }
        return result;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var result = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var first = lines[i].Split(',')[0];
            var second = lines[i].Split(',')[1];

            var first1 = int.Parse(first.Split('-')[0].ToString());
            var first2 = int.Parse(first.Split('-')[1].ToString());

            var firstdigits = new List<int>();
            for (int x = first1; x <= first2; x++)
            {
                firstdigits.Add(x);
            }

            var second1 = int.Parse(second.Split('-')[0].ToString());
            var second2 = int.Parse(second.Split('-')[1].ToString());

            var seconddigits = new List<int>();
            for (int x = second1; x <= second2; x++)
            {
                seconddigits.Add(x);
            }

            if (firstdigits.Intersect(seconddigits).Any())
            {
                result++;
            }
        }
        return result;
    }

}
