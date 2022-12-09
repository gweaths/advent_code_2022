TerminalDirectory root = new TerminalDirectory("/");
Dictionary<string, TerminalDirectory> registry = new();
registry["/"] = root;
Queue<string> input = new(File.ReadAllLines("input.txt").ToArray());

input.Dequeue();
Stack<TerminalDirectory> paths = new Stack<TerminalDirectory>();
paths.Push(root);

while (input.Count > 0)
{
    string command = input.Dequeue();
    Command c = CommandUtils.ParseCommand(command);
    c.ProcessCommand(paths, registry);
}

int totalSpace = 70_000_000;
int currentSpace = totalSpace - root.Size;
int requiredSpace = 30_000_000 - currentSpace;

List<TerminalDirectory> dirs = registry.Values.ToList();
var ordered = dirs.OrderBy(d => d.Size);

//--- Part 1 ---
var sum = registry.Values.Where(x => x.Size < 100_000).Sum(d => d.Size);
Console.WriteLine($" Part1 Sum: {sum}");

//--Part 2 ---
Console.WriteLine($"Space required: {requiredSpace}");
var toDelete = ordered.First(d => d.Size >= requiredSpace);
Console.WriteLine($"Part2 Smallest: {toDelete.Size}");
