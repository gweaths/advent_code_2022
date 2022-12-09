public abstract record TerminalFileBase(string Path)
{
    public abstract int Size { get; }
}
