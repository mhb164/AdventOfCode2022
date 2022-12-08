using System.Collections;
using System.Diagnostics;
using static AdventOfCode2022.Solutions.Day08SecondTrySolution;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(08)]
public class Day08SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 08;
    public override string SolvePartOne(in string[] inputLines) => Jungle.Parse(inputLines)
                                                                         .Where(tree => tree.VisibleFromOut())
                                                                         .Count()
                                                                         .ToString();

    public override string SolvePartTwo(in string[] inputLines) => Jungle.Parse(inputLines)
                                                                         .Max(tree => tree.GetScenicScore())
                                                                         .ToString();

    class Jungle : IEnumerable<Tree>
    {
        readonly Tree[][] _trees;
        readonly List<Tree> _treeList;

        Jungle(Tree[][] trees)
        {
            _trees = trees;
            _treeList = new List<Tree>();
            foreach (var row in _trees)
                foreach (var tree in row)
                {
                    _treeList.Add(tree);
                    tree.Initial(this);
                }
        }

        public Tree this[int rowIndex, int columnIndex] => GetTree(rowIndex, columnIndex);

        private Tree GetTree(int rowIndex, int columnIndex)
        {
            if (rowIndex == -1 || columnIndex == -1) return null;
            if (columnIndex == -1) return null;
            if (rowIndex >= _trees.Length) return null;
            if (columnIndex >= _trees[rowIndex].Length) return null;
            return _trees[rowIndex][columnIndex];
        }

        public static Jungle Parse(string[] inputLines)
        {
            var trees = new Tree[inputLines.Length][];

            for (int row = 0; row < inputLines.Length; row++)
            {
                trees[row] = new Tree[inputLines[row].Length];
                for (int col = 0; col < inputLines[row].Length; col++)
                {
                    trees[row][col] = new Tree(row, col, inputLines[row][col] - '0');
                }
            }

            return new Jungle(trees);
        }

        IEnumerator<Tree> IEnumerable<Tree>.GetEnumerator() => _treeList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _treeList.GetEnumerator();
    }

    class Tree
    {
        public readonly int RowIndex;
        public readonly int ColumnIndex;
        public readonly int Height;

        public Tree(int rowIndex, int columnIndex, int height)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Height = height;
        }

        public Tree Top { get; private set; }

        public Tree Right { get; private set; }

        public Tree Down { get; private set; }

        public Tree Left { get; private set; }

        public bool OnEdge => Top == null || Right == null || Down == null || Left == null;

        internal void Initial(Jungle jungle)
        {
            Top = jungle[RowIndex - 1, ColumnIndex];
            Right = jungle[RowIndex, ColumnIndex + 1];
            Down = jungle[RowIndex + 1, ColumnIndex];
            Left = jungle[RowIndex, ColumnIndex - 1];
        }

        public override string ToString() => $"Tree[{RowIndex},{ColumnIndex}] {Height}{(OnEdge ? " Edge" : "")}";

        internal bool VisibleFromOut()
        {
            if (OnEdge) return true;
            return VisibleFromTop(in Height) ||
                   VisibleFromRight(in Height) ||
                   VisibleFromDown(in Height) ||
                   VisibleFromLeft(in Height);
        }

        internal bool VisibleFromTop(in int height)
        {
            if (Top == null) return true;

            if (height > Top.Height)
                return Top.VisibleFromTop(in height);

            return false;
        }

        internal bool VisibleFromRight(in int height)
        {
            if (Right == null) return true;

            if (height > Right.Height)
                return Right.VisibleFromRight(in height);

            return false;
        }

        internal bool VisibleFromDown(in int height)
        {
            if (Down == null) return true;

            if (height > Down.Height)
                return Down.VisibleFromDown(in height);
            return false;
        }

        internal bool VisibleFromLeft(in int height)
        {
            if (Left == null) return true;

            if (height > Left.Height)
                return Left.VisibleFromLeft(in height);
            return false;
        }

        internal int GetScenicScore()
        {
            if (OnEdge) return 0;
            return GetScenicScoreToTop(in Height, 0) * 
                   GetScenicScoreToRight(in Height, 0) * 
                   GetScenicScoreToDown(in Height, 0) * 
                   GetScenicScoreToLeft(in Height, 0);
        }

        private int GetScenicScoreToTop(in int height, int previous)
        {
            if (Top == null) return previous;

            if (height > Top.Height)
                return Top.GetScenicScoreToTop(in height,previous + 1);

            return previous + 1;
        }

        private int GetScenicScoreToRight(in int height, int previous)
        {
            if (Right == null) return previous;

            if (height > Right.Height)
                return Right.GetScenicScoreToRight(in height, previous + 1);

            return previous + 1;
        }

        private int GetScenicScoreToDown(in int height, int previous)
        {
            if (Down == null) return previous;

            if (height > Down.Height)
                return Down.GetScenicScoreToDown(in height, previous + 1);

            return previous + 1;
        }

        private int GetScenicScoreToLeft(in int height, int previous)
        {
            if (Left == null) return previous;

            if (height > Left.Height)
                return Left.GetScenicScoreToLeft(in height, previous + 1);

            return previous + 1;
        }
    }
}
