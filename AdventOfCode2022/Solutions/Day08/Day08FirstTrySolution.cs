namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(08)]
public class Day08FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 08;
    public override object SolvePartOne(string[] lines)
    {
        var grid = Parse(lines);

        var visiables = new List<(int, int)>();
        
        for (int i = 0; i < grid.Count; i++)
            for (int j =0; j < grid[i].Count; j++)
            {
                if (i == 0 || i == grid.Count - 1 || j == 0 || j == grid[i].Count - 1)//Edges
                {
                    visiables.Add(new(i, j));
                    continue;
                }

                var current = grid[i][j];
                var top = new List<int>();
                for (int k = i - 1; k >= 0; k--) top.Add(grid[k][j]);

                var right = new List<int>();
                for (int k = j + 1; k < grid[i].Count; k++) right.Add(grid[i][k]);

                var left = new List<int>();
                for (int k = j - 1; k >= 0; k--) left.Add(grid[i][k]);

                var down = new List<int>();
                for (int k = i + 1; k < grid.Count; k++) down.Add(grid[k][j]);

                if (IsVisiable(top, current))
                {
                    visiables.Add(new(i, j));
                    continue;
                }

                if (IsVisiable(left, current))
                {
                    visiables.Add(new(i, j));
                    continue;
                }


                if (IsVisiable(right, current))
                {
                    visiables.Add(new(i, j));
                    continue;
                }


                if (IsVisiable(down, current))
                {
                    visiables.Add(new(i, j));
                    continue;
                }
            }



        return visiables.Count;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var grid = Parse(lines);

        var scores = new List<(int, int, int)>();
        for (int i = 1; i < grid.Count - 1; i++)
            for (int j = 1; j < grid[i].Count - 1; j++)
            {
                var top = new List<int>();
                for (int k = i - 1; k >= 0; k--) top.Add(grid[k][j]);

                var right = new List<int>();
                for (int k = j + 1; k < grid[i].Count; k++) right.Add(grid[i][k]);

                var left = new List<int>();
                for (int k = j - 1; k >= 0; k--) left.Add(grid[i][k]);

                var down = new List<int>();
                for (int k = i + 1; k < grid.Count; k++) down.Add(grid[k][j]);

                var topScore = GetScore(grid[i][j], top);
                var leftScore = GetScore(grid[i][j], left);
                var rightScore = GetScore(grid[i][j], right);
                var downScore = GetScore(grid[i][j], down);

                var score = topScore * leftScore * rightScore * downScore;
                scores.Add(new(i, j, score));
            }

        return scores.Max(x => x.Item3);
    }

    private static List<List<int>> Parse(string[] lines)
    {
        var grid = new List<List<int>>(lines.Length);
        for (int i = 0; i < lines.Length; i++)
        {
            grid.Add(new List<int>(lines[i].Length));
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i].Add(int.Parse(lines[i][j].ToString()));
            }
        }

        return grid;
    }

    private static bool IsVisiable(List<int> view, int tree) => tree > view.Max();

    private static int GetScore(int tree, List<int> view)
    {
        for (int i = 0; i < view.Count; i++)
        {
            if (tree <= view[i])
                return i + 1;
        }

        return view.Count;
    }
}
