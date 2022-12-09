public record TerminalFile : TerminalFileBase
{
    public override int Size { get; }

    public TerminalFile(string path, int size) : base(path)
    {
        this.Size = size;
    }
}
