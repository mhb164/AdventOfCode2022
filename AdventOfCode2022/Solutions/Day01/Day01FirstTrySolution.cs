namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(01)]
public class Day01FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 01;
    public override object SolvePartOne(string[] lines)
    {
        var ElvesCalories = CreateElvesCalories(lines);

        return ElvesCalories.Max();
    }

    public override object SolvePartTwo(string[] lines)
    {
        var ElvesCalories = CreateElvesCalories(lines);

        ElvesCalories.Sort();

        return (ElvesCalories[ElvesCalories.Count - 1] +
                ElvesCalories[ElvesCalories.Count - 2] +
                ElvesCalories[ElvesCalories.Count - 3]);
    }

    private static List<long> CreateElvesCalories(string[] lines)
    {
        var ElvesCalories = new List<long> { 0 };
        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                ElvesCalories.Add(0);
            }
            else
            {
                if (long.TryParse(lines[i], out var calories))
                {
                    ElvesCalories[ElvesCalories.Count - 1] += calories;
                }
                else
                {
                    throw new ArgumentException($"Line #{i} isn't integer!! ({lines[i]})");
                }
            }
        }
        return ElvesCalories;
    }
}
