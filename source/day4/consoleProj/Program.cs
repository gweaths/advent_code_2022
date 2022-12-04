int Part1Result = Run(Mode.Cover);
int Part2Result = Run(Mode.Overlaps);

Console.WriteLine(Part1Result);
Console.WriteLine(Part2Result);

int Run(Mode mode)
{
    var total = 0;
    var lines = System.IO.File.ReadLines("input.txt");
    var shiftPatterns = lines.Select(l => l.Split(",").Select(ParseToRange).ToArray()).ToArray();

    Range ParseToRange(string s)
    {
        var split = s.Split("-");
        return new Range(int.Parse(split[0]), int.Parse(split[1]));
    }

    if (mode == Mode.Cover)
    {
        total = shiftPatterns.Count(r => AlreadyCovered(r[0], r[1]) || AlreadyCovered(r[1], r[0]));
    }
    else
    {
        total = shiftPatterns.Count(r => Overlaps(r[0], r[1]) || Overlaps(r[1], r[0]));
    }

    return total;
}

bool AlreadyCovered(Range a, Range b) => a.Start.Value <= b.Start.Value && a.End.Value >= b.End.Value;

bool Overlaps(Range a, Range b) =>
    (
        a.Start.Value <= b.Start.Value && a.End.Value >= b.Start.Value
        || (a.Start.Value <= b.End.Value && a.End.Value >= b.End.Value)
    );

enum Mode
{
    Cover,
    Overlaps
}
