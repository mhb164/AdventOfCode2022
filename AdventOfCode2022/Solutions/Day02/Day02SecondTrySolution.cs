using System;
using System.Collections;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(02)]
public class Day02SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 02;

    enum HandShape { Rock = 1, Paper = 2, Scissors = 3 }
    enum Outcome { Lose = 0, Draw = 3, Won = 6 }

    class RockPaperScissors
    {
        public RockPaperScissors()
        {
            Score = 0;
        }

        public int Score { get; private set; }

        internal void AddRound(in HandShape opponentShape, in HandShape myShape)
        {
            Score += (int)myShape + (int)GetOutcome(in myShape, in opponentShape);
        }

        private static Outcome GetOutcome(in HandShape mine, in HandShape opponent)
        {
            if (opponent == mine)
            {
                return Outcome.Draw;
            }

            if (opponent == HandShape.Rock && mine == HandShape.Scissors)
            {
                return Outcome.Lose;
            }

            if (opponent == HandShape.Paper && mine == HandShape.Rock)
            {
                return Outcome.Lose;
            }

            if (opponent == HandShape.Scissors && mine == HandShape.Paper)
            {
                return Outcome.Lose;
            }

            return Outcome.Won;
        }
    }

    public override object SolvePartOne(string[] lines)
    {
        var holder = new RockPaperScissors();

        foreach (var line in lines)
        {
            var opponentShape = ToShapePartOne(in line, 0);
            var myShape = ToShapePartOne(in line, 2);

            holder.AddRound(in opponentShape, in myShape);
        }

        return holder.Score;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var holder = new RockPaperScissors();

        foreach (var line in lines)
        {
            var opponentShape = ToShapePartTwo(in line, 0);
            var myOutcomeMustBe = ToOutcomePartTwo(in line, 2);

            var myShapeMustBe = GetShapeLeadToSpecificOutcome(in opponentShape, in myOutcomeMustBe);

            holder.AddRound(opponentShape, myShapeMustBe);
        }

        return holder.Score;
    }

    private static HandShape ToShapePartOne(in string input, int index)
        => input[index] switch
        {
            'X' or 'A' => HandShape.Rock,
            'Y' or 'B' => HandShape.Paper,
            'Z' or 'C' => HandShape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input)),
        };

    private static HandShape ToShapePartTwo(in string input, int index)
        => input[index] switch
        {
            'A' => HandShape.Rock,
            'B' => HandShape.Paper,
            'C' => HandShape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input)),
        };

    private static Outcome ToOutcomePartTwo(in string input, int index)
        => input[index] switch
        {
            'X' => Outcome.Lose,
            'Y' => Outcome.Draw,
            'Z' => Outcome.Won,
            _ => throw new ArgumentOutOfRangeException(nameof(input)),
        };   

    private static HandShape GetShapeLeadToSpecificOutcome(in HandShape opponentShape, in Outcome goalOutcome)
        => goalOutcome switch
        {
            Outcome.Draw => opponentShape,
            Outcome.Lose => opponentShape switch
            {
                HandShape.Rock => HandShape.Scissors,
                HandShape.Paper => HandShape.Rock,
                HandShape.Scissors => HandShape.Paper,
                _ => throw new NotImplementedException(),
            },
            Outcome.Won => opponentShape switch
            {
                HandShape.Rock => HandShape.Paper,
                HandShape.Paper => HandShape.Scissors,
                HandShape.Scissors => HandShape.Rock,
                _ => throw new NotImplementedException(),
            },
            _ => throw new NotImplementedException(),
        };

}
