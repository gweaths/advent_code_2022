public record TerminalDirectory : TerminalFileBase
{
    private List<TerminalFileBase> _children { get; } = new();

    public override int Size => _children.Select(x => x.Size).Sum();

    public TerminalDirectory(string path) : base(path) { }

    public TerminalFileBase AddChild(TerminalFileBase child)
    {
        _children.Add(child);
        return this;
    }

    public List<TerminalFileBase>.Enumerator Children()
    {
        return _children.GetEnumerator();
    }
}
