using System.Reflection.Emit;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(11)]
public class Day11SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 11;
    //TODO: it is first try!! make it better
    class PackItem
    {
        public PackItem(long value) { WorryLevel = value; }
        public long WorryLevel;

        public override string ToString() => WorryLevel.ToString();
    }

    class Monkey
    {
        public int Number;
        public List<PackItem> Items = new List<PackItem>();
        public Func<long, long> Operation;
        public long TestParameter;

        public int TestTrueMonkeyNumber;
        public Monkey TestTrueMonkey;

        public int TestFalseMonkeyNumber;
        public Monkey TestFalseMonkey;

        public long ItemInspectCounter = 0;

        public override string ToString() => $"Monkey[{Number}] (Inspect Count: {ItemInspectCounter}) {string.Join(", ", Items)}";
        
        internal void Play(long worrylevel)
        {
            if (Items.Count == 0) return;

            var items = Items.ToList();
            foreach (var item in items)
            {
                ItemInspectCounter++;
                Items.Remove(item);
                item.WorryLevel = Operation(item.WorryLevel) / worrylevel;

                if (item.WorryLevel % TestParameter == 0)
                {
                    TestTrueMonkey.Items.Add(item);
                }
                else
                {
                    TestFalseMonkey.Items.Add(item);
                }

            }
        }
    }

    private Dictionary<int, Monkey> Parse(string[] lines)
    {
        var monkeys = new Dictionary<int, Monkey>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (!lines[i].StartsWith("Monkey "))
                continue;

            var monkey = new Monkey();
            monkey.Number = int.Parse(lines[i].Replace("Monkey ", "").Replace(":", ""));
            monkey.Items = lines[i + 1].Replace("  Starting items: ", "").Split(',').Select(x => new PackItem(long.Parse(x))).ToList();

            var oplineItems = lines[i + 2].Replace("Operation: new = ", "").Trim().Split(' ');
            monkey.Operation = (long old) =>
            {
                var p1 = (oplineItems[0] == "old") ? old : long.Parse(oplineItems[0]);

                var p2 = (oplineItems[2] == "old") ? old : long.Parse(oplineItems[2]);

                return oplineItems[1] switch
                {
                    "+" => p1 + p2,
                    "-" => p1 - p2,
                    "*" => p1 * p2,
                    "/" => p1 / p2,
                    _ => throw new NotImplementedException(),
                };
            };

            monkey.TestParameter = int.Parse(lines[i + 3].Replace("  Test: divisible by ", ""));
            monkey.TestTrueMonkeyNumber = int.Parse(lines[i + 4].Replace("If true: throw to monkey ", ""));
            monkey.TestFalseMonkeyNumber = int.Parse(lines[i + 5].Replace("If false: throw to monkey ", ""));
            monkeys.Add(monkey.Number, monkey);
        }

        foreach (var monkey in monkeys.Values)
        {
            monkey.TestTrueMonkey = monkeys[monkey.TestTrueMonkeyNumber];
            monkey.TestFalseMonkey = monkeys[monkey.TestFalseMonkeyNumber];
        }
        return monkeys;
    }

    public override object SolvePartOne(string[] lines)
    {
        var monkeys = Parse(lines).Values.ToList();
        for (int i = 0; i < 20; i++)
        {
            Play(monkeys, 3);
        }
        var ItemInspectCounters = monkeys.OrderByDescending(x => x.ItemInspectCounter).Select(x => x.ItemInspectCounter).ToList();
        return ItemInspectCounters[0] * ItemInspectCounters[1];
    }

    private void Play(List<Monkey> monkeys, long worrylevel)
    {
        foreach (var monkey in monkeys)
        {
            monkey.Play(worrylevel);
        }
    }

    public override object SolvePartTwo(string[] lines)
    {
        var monkeys = Parse(lines).Values.ToList();

        var multiplier = (long coefficient, Monkey monkey) => coefficient * monkey.TestParameter;
        var coefficient = monkeys.Aggregate(1L, multiplier);

        for (int i = 0; i < 10_000; i++)
        {
            foreach (var monkey in monkeys)
                foreach (var item in monkey.Items)
                {
                    item.WorryLevel %= coefficient;
                }
            Play(monkeys, 1);
        }
        var ItemInspectCounters = monkeys.OrderByDescending(x => x.ItemInspectCounter).Select(x => x.ItemInspectCounter).ToList();
        return ItemInspectCounters[0] * ItemInspectCounters[1];
    }


}
