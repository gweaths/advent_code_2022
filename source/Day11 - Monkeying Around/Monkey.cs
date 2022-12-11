public record Monkey(List<long> StartItems, Func<long, long> Op, int Divider, int TrueMonkey, int FalseMonkey)
{

    private Queue<long> _items = new Queue<long>(StartItems);

    public long ItemsInspected { get; private set; } = 0;

    public bool ThinkAndThrow(List<Monkey> gang, int? modulo)
    {
        if (_items.Count == 0)
        {
            return false;
        }

        ItemsInspected++;
        long oldValue = this._items.Dequeue();
        long newValue = modulo != null ? Op.Invoke(oldValue) % modulo.Value : Op.Invoke(oldValue) / 3;

        int monkeyIndex = newValue % Divider == 0 ? TrueMonkey : FalseMonkey;

        gang[monkeyIndex]._items.Enqueue(newValue);
        return true;
    }



    public static Monkey ParseStatement(string monkeyInfo)
    {
        string[] rows = monkeyInfo.Split("\n");
        List<long> startItems = rows[1]
                                    .Split(":")[1]
                                    .Split(",")
                                    .Select(long.Parse)
                                    .ToList();

        Func<long, long> mathOp = ParseOperatorRow(rows[2].Split("=")[1].Trim());
        int divisor = int.Parse(rows[3].Split("by")[1]);
        int monkeyIfTrue = int.Parse(rows[4].Split("monkey")[1]);
        int monkeyIfFalse = int.Parse(rows[5].Split("monkey")[1]);

        return new Monkey(startItems, mathOp, divisor, monkeyIfTrue, monkeyIfFalse);
    }

    private static Func<long, long> ParseOperatorRow(string input)
    {
        string[] tokens = input.Split();
        return (tokens[1], tokens[2]) switch
        {
            ("*", "old") => ((old) => old * old),
            ("+", "old") => ((old) => old + old),
            ("*", _) => ((old) => old * int.Parse(tokens[2])),
            ("+", _) => ((old) => old + int.Parse(tokens[2])),
            _ => throw new Exception("Invalid operator")
        };
    }

}



