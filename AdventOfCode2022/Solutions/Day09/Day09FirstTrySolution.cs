using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(09)]
public class Day09FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 09;

    class pos
    {
        public int row = 0;
        public int col = 0;

        public override string ToString() => $"({row}, {col})";

        internal void move(pos h)
        {
            if (Math.Abs(h.row - row) > 1)
            {
                row += h.row > row ? 1 : -1;
                if (h.col != col)
                {
                    col += h.col > col ? 1 : -1;
                }
            }
            else if (Math.Abs(h.col - col) > 1)
            {
                col += h.col > col ? 1 : -1;
                if (h.row != row)
                {
                    row += h.row > row ? 1 : -1;
                }
            }
            return;
        }
    }

    public override object SolvePartOne(in string[] inputLines)
    {
        var positions = new List<pos>();
        var H = new pos();
        var T = new pos();
        foreach (var inputLine in inputLines)
        {
            var splited = inputLine.Split(' ');
            var dir = splited[0];
            var count = int.Parse(splited[1]);
            //--------------

            if (dir == "R")
            {
                for (int i = 0; i < count; i++)
                {
                    H.col++;
                    Update1(H, T, positions);
                }
            }
            else if (dir == "L")
            {
                for (int i = 0; i < count; i++)
                {
                    H.col--;
                    Update1(H, T, positions);
                }
            }
            else if (dir == "U")//D
            {
                for (int i = 0; i < count; i++)
                {
                    H.row++;
                    Update1(H, T, positions);
                }
            }
            else if (dir == "D")//U
            {
                for (int i = 0; i < count; i++)
                {
                    H.row--;
                    Update1(H, T, positions);
                }
            }
        }
        return positions.Count;
    }

    private void Update1(pos h, pos t, List<pos> positions)
    {
        var existed = positions.FirstOrDefault(x => x.row == t.row && x.col == t.col);
        if (existed == null)
        {
            positions.Add(new pos() { row = t.row, col = t.col });
        }
        t.move(h);
         existed = positions.FirstOrDefault(x => x.row == t.row && x.col == t.col);
        if (existed == null)
        {
            positions.Add(new pos() { row = t.row, col = t.col });
        }
    }
    
    public override object SolvePartTwo(in string[] inputLines)
    {
        var result = string.Empty;

        var positions = new List<pos>();
        var H = new pos();
        var T = new pos[9];
        for (int i = 0; i < T.Length; i++)
        {
            T[i] = new pos();
        }
        foreach (var inputLine in inputLines)
        {
            var splited = inputLine.Split(' ');
            var dir = splited[0];
            var count = int.Parse(splited[1]);
            //--------------

            if (dir == "R")
            {
                for (int i = 0; i < count; i++)
                {
                    H.col++;
                    Update2(H, T, positions);
                }
            }
            else if (dir == "L")
            {
                for (int i = 0; i < count; i++)
                {
                    H.col--;
                    Update2(H, T, positions);
                }
            }
            else if (dir == "U")//D
            {
                for (int i = 0; i < count; i++)
                {
                    H.row++;
                    Update2(H, T, positions);
                }
            }
            else if (dir == "D")//U
            {
                for (int i = 0; i < count; i++)
                {
                    H.row--;
                    Update2(H, T, positions);
                }
            }
        }
        return positions.Count;
    }

    private void Update2(pos h, pos[] t, List<pos> positions)
    {
        var existed = positions.FirstOrDefault(x => x.row == t[8].row && x.col == t[8].col);
        if (existed == null)
        {
            positions.Add(new pos() { row = t[8].row, col = t[8].col });
        }

        t[0].move(h);
        for (int i = 1; i < t.Length; i++)
        {
            t[i].move(t[i-1]);            
        }

        existed = positions.FirstOrDefault(x => x.row == t[8].row && x.col == t[8].col);
        if (existed == null)
        {
            positions.Add(new pos() { row = t[8].row, col = t[8].col });
        }
    }
}
