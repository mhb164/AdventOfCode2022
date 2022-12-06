namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(01)]
public class Day01FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 01; 
    public override string SolvePartOne(in string[] inputLines)
    {
        var ElvesCalories = CreateElvesCalories(in inputLines);

        return ElvesCalories.Max().ToString();
    }

    public override string SolvePartTwo(in string[] inputLines)
    {
        var ElvesCalories = CreateElvesCalories(in inputLines);

        ElvesCalories.Sort();

        return (ElvesCalories[ElvesCalories.Count - 1] +
                ElvesCalories[ElvesCalories.Count - 2] +
                ElvesCalories[ElvesCalories.Count - 3]).ToString();
    }

    private static List<long> CreateElvesCalories(in string[] inputLines)
    {
        var ElvesCalories = new List<long> { 0 };
        for (int i = 0; i < inputLines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(inputLines[i]))
            {
                ElvesCalories.Add(0);
            }
            else
            {
                if (long.TryParse(inputLines[i], out var calories))
                {
                    ElvesCalories[ElvesCalories.Count - 1] += calories;
                }
                else
                {
                    throw new ArgumentException($"Line #{i} isn't integer!! ({inputLines[i]})");
                }
            }
        }
        return ElvesCalories;
    }
}
