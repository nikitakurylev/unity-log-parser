namespace LogParser;

public interface IItem
{
    public string Name { get; }
    
    public double GetSize();
}