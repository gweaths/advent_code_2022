string[] monkeyStatements = File.ReadAllText("input.txt").Split("\n\n");

void Part1()
{
    List<Monkey> gang = monkeyStatements.Select(Monkey.ParseStatement).ToList();

    long maxRounds = 20;

    for (long i = 0; i < maxRounds; i++)
    {
        OneRound_GO(gang, null);
    }

    var total = gang.Select(x => x.ItemsInspected).OrderByDescending(c => c).Aggregate((total, next) => total * next);

    Console.WriteLine($@"Total monkey business = {total}");
}

void Part2()
{
    List<Monkey> gang = monkeyStatements.Select(Monkey.ParseStatement).ToList();

    //Calculate the modulo value used to divide -- which is the common denominator of all monkeys' divisor.
    int modulo = gang.Select(m => m.Divider).Aggregate(1, (a, b) => a * b);

    for (int i = 0; i < 10_000; i++)
    {
        OneRound_GO(gang, modulo);
    }

    long total = gang.Select(x => x.ItemsInspected).OrderByDescending(count => count).Take(2).Aggregate((total, next) => total * next);
    Console.WriteLine($@"Total monkey business = {total}");
}

Part1();
Part2();

void OneRound_GO(List<Monkey> gang, int? modulo)
{
    foreach (Monkey m in gang)
    {
        while (m.ThinkAndThrow(gang, modulo)) ;
    }
}
