using System.Text.RegularExpressions;

void Run(string filename)
{
    string[] rows = File.ReadAllLines(filename);
    var stacksOfLetters = GetCraneStacks(rows);
    var instructions = GetInstructions(rows);

    foreach (var instruction in instructions)
    {
        //minus 1 on from and to to account for 0indexing
        MoveBoxes(instruction[0], instruction[1] - 1, instruction[2] - 1, stacksOfLetters);
    }

    Console.WriteLine(string.Join("", stacksOfLetters.Select(s => s[0])));
}

Run(args[0]);

#region Methods
void MoveBoxes(int numberOfBoxes, int from, int to, List<char>[] stacks)
{
    //keep getting the most top box
    while (numberOfBoxes-- > 0)
    {
        // can only always get the top box
        char toMove = stacks[from][0];

        //move the box
        stacks[to].Insert(0, toMove);

        //remove it at the original location
        stacks[from].RemoveAt(0);
    }
}

void ConvertRowToStack(string row, List<char>[] stacks)
{
    for (int i = 0; i < stacks.Length; i++)
    {
        //move along the row (there are spaces between characters + bracket)
        int charIndex = (i * 4) + 1;

        //access the character
        char boxLetter = row[charIndex];

        //we don't want no whitespace clogging up our stacks
        if (boxLetter != ' ')
        {
            stacks[i].Add(boxLetter);
        }
    }
}

List<char>[] GetCraneStacks(string[] rows)
{
    //get the first row as all rows wil be of same stack length
    int numberOfStacks = (rows[0].Length + 1) / 4;
    List<char>[] stacks = InitialiseAllStacks(numberOfStacks);

    //loop over the rows and only get the stack data (not instructions)
    foreach (string row in rows)
    {
        //not a stack row, we've finished get the hell outta there.
        if (!IsStackRow(row))
            break;

        ConvertRowToStack(row, stacks);
    }
    return stacks;
}

List<int[]> GetInstructions(string[] rows)
{
    List<int[]> instructions = new();
    foreach (var row in rows)
    {
        List<int> directions = new();
        if (!row.Contains("move"))
            continue;
        string[] numbers = Regex.Split(row, @"\D+");

        foreach (string value in numbers)
        {
            if (string.IsNullOrEmpty(value))
                continue;

            directions.Add(int.Parse(value));
        }

        instructions.Add(directions.ToArray());
    }
    return instructions;
}

bool IsStackRow(string row) => row.Contains('[');

#endregion

#region Helper Methods
List<char>[] InitialiseAllStacks(int numberOfStacks)
{
    List<char>[] stacks = new List<char>[numberOfStacks];

    for (int i = 0; i < stacks.Length; i++)
    {
        stacks[i] = new List<char>();
    }

    return stacks;
}
#endregion
