string[] SplitRuckSackCompartments(string rucksack)
{
    var first = rucksack[..(rucksack.Length / 2)];
    var second = rucksack[(rucksack.Length / 2)..(rucksack.Length)];

    return new[] { first, second };
}

int GetScore(char character)
{
    //32 digits between ASCII values
    int position = (int)character % 32;

    //add 26 to whatever character if uppercase (1=a, 27=A)
    return Char.IsUpper(character) ? position + 26 : position;
}

IEnumerable<string[]> GetGroups(string[] input)
{
    int i = 0;
    while (input.Length > 3 * i)
    {
        yield return input.Skip(3 * i).Take(3).ToArray();
        i++;
    }
}

void Part1()
{
    string[] input = System.IO.File.ReadAllLines("input.txt");
    var totalScore = 0;
    foreach (var rucksack in input)
    {
        var split = SplitRuckSackCompartments(rucksack);
        var comp1Char = split[0].ToCharArray();
        var comp2Char = split[1].ToCharArray();

        var duplicates = comp1Char.Intersect(comp2Char).ToArray();
        var scores = duplicates.Select(character => GetScore(character)).ToArray();
        totalScore += scores.Sum();
    }
    Console.WriteLine($"Part 1 Answer : {totalScore}");
}

void Part2()
{
    string[] input = System.IO.File.ReadAllLines("input.txt");

    var totalScore = 0;
    foreach (var group in GetGroups(input))
    {
        var match = group
            .Select(sack => sack.ToHashSet())
            .Aggregate((a, b) => a.Intersect(b).ToHashSet());

        totalScore +=
            match.Count() > 1 ? match.Select(m => GetScore(m)).Sum() : GetScore(match.First());
    }

    Console.WriteLine($"Part 2 Answer : {totalScore}");
}

Part1();
Part2();
