var input = File.ReadAllText(args[0]);

Console.WriteLine($"Part1: {FindMarker(input, 4)}");
Console.WriteLine($"Part2: {FindMarker(input, 14)}");


int FindMarker(string input, int amount)
{
    if (input.Length == 0)
    {
        throw new Exception("No input found");
    }

    for (int i = 0, count = input.Length; i < count; i++)
    {
        var group = input[i..(i + amount)];
        if (group.ToHashSet().Count() == amount)
        {
            return i + amount;
        }
    }
    return 0;
}
