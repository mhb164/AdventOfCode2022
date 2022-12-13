using System.Text;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(13)]
public class Day13FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 13;


    class Colect
    {
        public Colect Parent;
        public List<Colect> Colects = new List<Colect>();
        public int? Value;

        public override string ToString() => $"{(Colects.Count > 0 ? $"[{string.Join(",", Colects)}]" : "")}" +
                                             $"{(Value.HasValue ? $"[{Value}]" : "")}".Trim();
        public Colect(Colect parent)
        {
            Parent = parent;
            if (Parent != null) Parent.Colects.Add(this);
        }
    }
    class Pair
    {
        public readonly int Number;
        public readonly string One;
        public readonly Colect ColectOne;
        public readonly List<List<int>> OneSets;
        public readonly string Two;
        public readonly Colect ColectTwo;
        public readonly List<List<int>> TwoSets;

        public Pair(int number, string one, string two)
        {
            Number = number;
            One = Correct(one);
            Two = Correct(two);

            Console.WriteLine($"");
            // Console.WriteLine($"******************************");
            //Console.WriteLine($"{one}   ----------   {two}");
            //Console.WriteLine($"{One}   ----------   {Two}");

            ColectOne = CreateColect(One);
            ColectTwo = CreateColect(Two);

            // Console.WriteLine($"{ColectOne}   ==========   {ColectTwo}");


        }

        public static Colect CreateColect(string value)
        {
            var index = 0;
            var colect = new Colect(null);
            index++;
            while (index < value.Length - 1)
            {
                if (value[index] == '[')
                {
                    colect = new Colect(colect);
                    index++;
                    continue;
                }
                else if (value[index] == ']')
                {
                    colect = colect?.Parent;
                    index++;
                    continue;
                }
                else if (value[index] == ',')
                {
                    index++;
                    continue;
                }
                else if (char.IsDigit(value[index]))
                {
                    var digit = value[index].ToString();
                    while (index < value.Length - 1)
                    {
                        if (char.IsDigit(value[index + 1]))
                        {
                            index++;
                            digit += value[index];
                        }
                        else
                        {
                            index++;
                            break;
                        }
                    }
                    colect.Value = int.Parse(digit);
                }
            }
            return colect;
        }

        public static string Correct(string value)
        {
            var text = new StringBuilder();
            var i = 0;
            while (i < value.Length)
            {
                var hetTest = GetDigit(ref value, ref i);
                if (hetTest.isDigit)
                {
                    //if (value[i + 1] ==',')
                    text.Append($"[{hetTest.digit}]");
                    //else 

                }
                else
                {
                    text.Append(value[i]);
                    i++;
                }
            }
            return text.ToString();
        }
        private static (string digit, bool isDigit) GetDigit(ref string value, ref int i)
        {
            var ttt = "";
            var isDigit = false;
            while (i < value.Length)
            {
                if (char.IsDigit(value[i]))
                {
                    ttt += value[i];
                    i++;
                    isDigit = true;
                }
                else
                {
                    break;
                }
            }

            return new(ttt, isDigit);
        }

        public bool IsRightOrder()
        {
            var result = Compare(ColectOne, ColectTwo);
            //Console.WriteLine($"{Number} > {result}");

            return result < 0;
        }


        public static int Compare(Colect right, Colect left)
        {
            if (right.Value.HasValue && left.Value.HasValue)
            {
                if (right.Value == left.Value)
                    return 0;
                else if (right.Value > left.Value)
                {
                    return 1;
                }
                else return -1;//right order
            }
            else if (!right.Value.HasValue && !left.Value.HasValue)
            {
                var min = Math.Min(right.Colects.Count, left.Colects.Count);
                for (int i = 0; i < min; i++)
                {
                    var result = Compare(right.Colects[i], left.Colects[i]);
                    if (result != 0) return result;
                }

                if (min != right.Colects.Count)
                    return +2;
                if (min != left.Colects.Count)
                    return -2;
            }
            else
            {
                if (right.Value.HasValue)
                {
                    return Compare(ToNewCollect(right), left);
                }
                else if (left.Value.HasValue)
                {
                    return Compare(right, ToNewCollect(left));
                }
                else
                {

                }
            }

            return 0;
        }

        private static Colect ToNewCollect(Colect colect)
        {
            return new Colect(null) { Colects = new List<Colect>() { colect } };
        }

        private static void Init(string o, string t, List<List<int>> oneSets, List<List<int>> twoSets)
        {
            if (o.StartsWith('[') && o.EndsWith(']'))
            {
                o = o[1..(o.Length - 1)];
            }

            if (t.StartsWith('[') && t.EndsWith(']'))
            {
                t = t[1..(t.Length - 1)];
            }


            var oCount = o.Where(x => x == '[').Count();
            var tCount = t.Where(x => x == '[').Count();
            //var oClosingInMiddleCount = GetCount(o, "],");// o.IndexOf("],") >=0;
            //var tClosingInMiddleCount = GetCount(t, "],");// t.IndexOf("],") >= 0;

            if (oCount == tCount)
            {
                if (oCount == 0)
                {
                    Add(oneSets, o);
                    Add(twoSets, t);
                }
                else
                {
                    var iiO = o.IndexOf("],");
                    var iiT = t.IndexOf("],");

                    var oo = o[1..iiO];
                    var tt = t[1..iiO];
                    Add(oneSets, oo);
                    Add(twoSets, tt);

                    Init(o[(iiO + 2)..], t[(iiT + 2)..], oneSets, twoSets);
                    //for (int i = 0; i < oo.Length; i++)
                    //{
                    //    Init(oo[i], tt[i], oneSets, twoSets);
                    //}
                }
            }
            //else if (oCount == tCount && (oClosingInMiddleCount != tClosingInMiddleCount))
            //{
            //    if(oCount == 1)
            //    {
            //        Init($"[{o.Replace("],","],[")}]]", $"[{t.Replace("],", "],[")}]]", oneSets, twoSets);

            //    }
            //    else
            //    {

            //    }
            //}
            else
            {
                if (oCount == 0)
                {
                    Ajib(o, t, oneSets, twoSets);
                }
                else
                {

                    var mincount = Math.Min(oCount, tCount);
                    var iiO = 0;
                    var iiT = 0;
                    for (int i = 0; i < mincount; i++)
                    {
                        var preiiO = iiO;
                        var preiiT = iiT;
                        iiO = o.IndexOf("],", iiO + 1);
                        iiT = t.IndexOf("],", iiT + 1);
                        var oo = o[preiiO..(iiO + 1)];
                        var tt = t[preiiT..(iiT + 1)];

                        Init(oo, tt, oneSets, twoSets);
                    }


                    var tailO = o[(iiO + 2)..];
                    var tailT = t[(iiT + 2)..];
                    if (o == "[]")
                    {
                        Init("", $"[{tailT}", oneSets, twoSets);
                    }
                    else if (t == "[]")
                    {
                        tailO = tailO[..^1];
                        Init(tailO, "", oneSets, twoSets);

                    }
                    else
                    {


                        Ajib(tailO, tailT, oneSets, twoSets);
                    }

                }
            }


        }

        private static void Add(List<List<int>> sets, string value)
        {
            var values = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 0)
            {
                sets.Add(new List<int>());
            }
            else sets.Add(values.Select(x => int.Parse(x)).ToList());
        }

        private static int GetCount(string o, string v)
        {
            var count = 0;
            var lastIndex = o.IndexOf(v);
            while (lastIndex > -1)
            {
                count++;
                lastIndex = o.IndexOf(v, lastIndex + 1);
            }
            return count;
        }

        private static void Ajib(string tailO, string tailT, List<List<int>> oneSets, List<List<int>> twoSets)
        {
            var countOstart = GetCount(tailO, "[");
            var countOEnd = GetCount(tailO, "]");

            var countTstart = GetCount(tailT, "[");
            var countTEnd = GetCount(tailT, "]");

            if (tailO.StartsWith('[') && !tailT.StartsWith('['))
            {
                Init(tailO, $"[{tailT}]", oneSets, twoSets);
            }
            else if (!tailO.StartsWith('[') && tailT.StartsWith('['))
            {
                Init($"[{tailO}]", tailT, oneSets, twoSets);

            }
            else if (!tailO.StartsWith('[') && !tailT.StartsWith('['))
            {

                Init($"[{tailO}]", $"[{tailT}]", oneSets, twoSets);
            }
            else
            {

            }
        }


    }


    public override object SolvePartOne(string[] lines)
    {
        var pairs = new List<Pair>();
        for (int i = 0; i <= lines.Length / 3; i++)
        {
            pairs.Add(new Pair(i + 1, lines[i * 3], lines[i * 3 + 1]));
        }

        return pairs.Where(x => x.IsRightOrder()).Select(x => x.Number).Sum();
    }

    public override object SolvePartTwo(string[] lines)
    {
        var corrected = new List<(string, Colect)>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var correctedLine = Pair.Correct(line);
            corrected.Add(new (correctedLine, Pair.CreateColect(correctedLine)));
        }

        (string, Colect) aa = new("[[2]]", Pair.CreateColect("[[2]]"));
        (string, Colect) bb = new("[[6]]", Pair.CreateColect("[[6]]"));
        corrected.Add(aa);//"[[2]]");
        corrected.Add(bb);//"[[6]]");;


        corrected.Sort((x, y) =>
        {
            return Pair.Compare(x.Item2, y.Item2);
        });

        //foreach (var item in corrected)
        //{
        //    Console.WriteLine(item.Item1);
        //}

        var a = corrected.IndexOf(aa);
        var b = corrected.IndexOf(bb);

        return (a+1)*(b+1);
    }

}
