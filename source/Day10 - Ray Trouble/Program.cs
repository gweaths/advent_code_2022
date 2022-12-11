string[] input = File.ReadAllLines("input.txt");

int CRTWidth = 40;
int score = 0;
int xRegister = 1;
int cycle = 1;

Console.Clear();

foreach (var line in input)
{
    ProcessLine(line);
}

//Clear after print out make score clearer 
Console.WriteLine();
Console.WriteLine(score);

void ProcessLine(string input)
{
    string[] tokens = input.Split();
    CommsAction o = tokens[0] switch
    {
        "noop" => new CommsAction(() => { }, 1),
        "addx" => new CommsAction(() => AddX(int.Parse(tokens[1])), 2),
        _ => throw new Exception("Unknown command")
    };

    if (!Tick(o.cycles))
    {
        return;
    }

    //Call the action we passed in
    o.endAction.Invoke();
}

bool Tick(int ticks)
{
    //reached the end inform caller to Finish
    if (cycle > 240) return false;

    //nothing to do meaning no command / cycles.
    if (ticks == 0) return true;

    //Increment the score (after requested intervals)
    if (cycle == 20 || (cycle + 20) % 40 == 0)
    {
        score += cycle * xRegister;
    }

    Draw();

    //Animation to print out lettering (comment out if not wanted)
    Thread.Sleep(20);

    //Increment the cycle'
    cycle++;

    //Do it all again - until is 0.
    return Tick(ticks - 1);
}

void AddX(int value)
{
    xRegister += value;
}

void Draw()
{
    int row = (cycle - 1) / CRTWidth;
    int col = (cycle - 1) % CRTWidth;

    //Kinda cheating utilising the cursor here (utilise C# tools I guess.)
    Console.SetCursorPosition(col, row);

    if (ShouldDraw(col))
    {
        Console.WriteLine("#");
    }
}

bool ShouldDraw(int col)
{
    return col >= (xRegister - 1) && col <= (xRegister + 1);
}

record CommsAction(Action endAction, int cycles);