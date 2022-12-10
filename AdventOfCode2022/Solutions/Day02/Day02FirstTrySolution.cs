namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(02)]
public class Day02FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 02;
    public override object SolvePartOne(string[] lines)
    {
        var scores = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var opponentChoice = lines[i][0];
            var myChoice = lines[i][2];

            var myShapeOutcome = GetMyShapeOutcome(myChoice);
            var myRoundOutcome = GetMyRoundOutcome(opponentChoice, myChoice);

            scores += myShapeOutcome + myRoundOutcome;
        }

        return scores;
    }

    public override object SolvePartTwo(string[] lines)
    {
        var scores = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var opponentChoice = lines[i][0];
            var resultMustBe = lines[i][2];

            var myChoice = '?';
            if (resultMustBe == 'X')//lose
            {
                if (opponentChoice == 'A') myChoice = 'Z';
                else if (opponentChoice == 'B') myChoice = 'X';
                else if (opponentChoice == 'C') myChoice = 'Y';
            }
            else if (resultMustBe == 'Y')//draw
            {
                if (opponentChoice == 'A') myChoice = 'X';
                else if (opponentChoice == 'B') myChoice = 'Y';
                else if (opponentChoice == 'C') myChoice = 'Z';
            }
            else// if (resultMustBe == 'Z')//win
            {
                if (opponentChoice == 'A') myChoice = 'Y';
                else if (opponentChoice == 'B') myChoice = 'Z';
                else if (opponentChoice == 'C') myChoice = 'X';
            }

            var myShapeOutcome = GetMyShapeOutcome(myChoice);
            var myRoundOutcome = GetMyRoundOutcome(opponentChoice, myChoice);

            scores += myShapeOutcome + myRoundOutcome;
        }

        return scores;
    }

    private int GetMyShapeOutcome(char choice)
    {
        return choice switch
        {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(choice)),
        };
    }
    private int GetMyRoundOutcome(char opponentChoice, char myChoice)
    {
        if ((opponentChoice == 'A' && myChoice == 'Z') ||
            (opponentChoice == 'B' && myChoice == 'X') ||
            (opponentChoice == 'C' && myChoice == 'Y'))//lost
        {
            return 0;
        }
        else if ((opponentChoice == 'A' && myChoice == 'X') ||
                 (opponentChoice == 'B' && myChoice == 'Y') ||
                 (opponentChoice == 'C' && myChoice == 'Z'))//draw
        {
            return 3;
        }
        else //won
        {
            return 6;
        }
    }



}
