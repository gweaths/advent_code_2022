
string input = System.IO.File.ReadAllText(args[0]);
char[] chars = input.ToCharArray();

var part1 = FindMarker(chars, 4);
var part2 = FindMarker(chars, 14);

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");


int FindMarker(char[] chars, int markerSize)
{
    for (int i = 0; i < chars.Length; i++)
    {
        var group = chars[i..(i + markerSize)];
        if (group.ToHashSet().Count() == markerSize)
        {
            return i + markerSize;
        }

    }
    return 0;
}