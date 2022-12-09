public abstract record Command
{
    public abstract void ProcessCommand(
        Stack<TerminalDirectory> paths,
        Dictionary<string, TerminalDirectory> registry
    );
}

public record ChangeDirectoryCommand(string Name) : Command
{
    public override void ProcessCommand(
        Stack<TerminalDirectory> paths,
        Dictionary<string, TerminalDirectory> registry
    )
    {
        if (Name == "..")
        {
            //We're going back up a level
            paths.Pop();
        }
        else
        {
            //We're in the directory get the full path
            TerminalDirectory cwd = paths.Peek();
            string fullPath = cwd.Path + Name + "/";
            paths.Push(registry[fullPath]);
        }
    }
}

public record AddFileCommand(string Name, int Size) : Command
{
    public override void ProcessCommand(
        Stack<TerminalDirectory> paths,
        Dictionary<string, TerminalDirectory> registry
    )
    {
        TerminalDirectory cwd = paths.Peek();
        string fullPath = cwd.Path + Name;
        cwd.AddChild(new TerminalFile(fullPath, Size));
    }
}

public record AddDirectoryCommand(string Name) : Command
{
    public override void ProcessCommand(
        Stack<TerminalDirectory> paths,
        Dictionary<string, TerminalDirectory> registry
    )
    {
        TerminalDirectory cwd = paths.Peek();

        string fullPath = cwd.Path + Name + "/";
        TerminalDirectory child = new TerminalDirectory(fullPath);
        registry[fullPath] = child;
        cwd.AddChild(child);
    }
}

public record ListCommand() : Command
{
    public override void ProcessCommand(
        Stack<TerminalDirectory> paths,
        Dictionary<string, TerminalDirectory> registry
    ) { }
}

public static class CommandUtils
{
    public static Command ParseCommand(string input)
    {
        string[] tokens = input.Split();

        if (tokens[0] == "$" && tokens[1] == "cd")
        {
            return new ChangeDirectoryCommand(tokens[2]);
        }
        else if (tokens[0] == "$" && tokens[1] == "ls")
        {
            return new ListCommand();
        }
        else if (tokens[0] == "dir")
        {
            return new AddDirectoryCommand(tokens[1]);
        }
        else if (int.TryParse(tokens[0], out int size))
        {
            return new AddFileCommand(tokens[1], size);
        }
        else
        {
            throw new Exception("Invalid command");
        }
    }
}
