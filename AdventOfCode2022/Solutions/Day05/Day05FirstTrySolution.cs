namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(05)]
public class Day05FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 05;
    public override string SolvePartOne(in string[] inputLines)
    {
        var stacks = new Dictionary<int, Stack<char>>();
        var i = 0;
        for (i = 0; i < inputLines.Length; i++)
        {
            if (inputLines[i].StartsWith(" 1"))
            {
                var items = inputLines[i].Split("   ");
                foreach (var item in items)
                {
                    stacks.Add(int.Parse(item), new Stack<char>());
                }
                break;
            }
        }

        for (int j = i - 1; j >= 0; j--)
        {
            for (int k = 0; k < stacks.Count; k++)
            {
                var xx = inputLines[j].Substring(k * 4, 3);
                xx = xx.Replace("[", "").Replace("]", "").Trim();
                if (xx.Length > 0)
                    stacks[k + 1].Push(xx[0]);
            }
        }

        i += 2;
        for (; i < inputLines.Length; i++)
        {
            var line = inputLines[i].Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(" ");
            var x1 = int.Parse(line[0]);
            var x2 = int.Parse(line[1]);
            var x3 = int.Parse(line[2]);

            for (int x = 0; x < x1; x++)
            {
                var ch = stacks[x2].Pop();
                stacks[x3].Push(ch);
            }
        }

        var result = string.Empty;
        foreach (var item in stacks)
        {
            result += item.Value.Pop();
        }
        return result;
    }


    public override string SolvePartTwo(in string[] inputLines)
    {
        var stacks = new Dictionary<int, Stack<char>>();
        var i = 0;
        for (i = 0; i < inputLines.Length; i++)
        {
            if (inputLines[i].StartsWith(" 1"))
            {
                var items = inputLines[i].Split("   ");
                foreach (var item in items)
                {
                    stacks.Add(int.Parse(item), new Stack<char>());
                }
                break;
            }
        }

        for (int j = i - 1; j >= 0; j--)
        {
            for (int k = 0; k < stacks.Count; k++)
            {
                var xx = inputLines[j].Substring(k * 4, 3);
                xx = xx.Replace("[", "").Replace("]", "").Trim();
                if (xx.Length > 0)
                    stacks[k + 1].Push(xx[0]);
            }
        }

        i += 2;
        for (; i < inputLines.Length; i++)
        {
            var line = inputLines[i].Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(" ");
            var x1 = int.Parse(line[0]);
            var x2 = int.Parse(line[1]);
            var x3 = int.Parse(line[2]);

            var items = new List<char>();
            for (int x = 0; x < x1; x++)
            {
                var ch = stacks[x2].Pop();
                items.Add(ch);
            }
            items.Reverse();
            foreach (var ch in items)
            {
                stacks[x3].Push(ch);
            }

        }

        var result = string.Empty;
        foreach (var item in stacks)
        {
            result += item.Value.Pop();
        }
        return result;
    }
   
}
