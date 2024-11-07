namespace LogParser;

public class FileItem(string name, double size) : IItem
{
    public string Name { get; } = name;

    public double GetSize()
    {
        return size;
    }
}