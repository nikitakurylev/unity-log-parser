namespace LogParser;

public class FolderItem(string name) : IItem
{
    public string Name { get; } = name;

    public IReadOnlyCollection<IItem> Items => _items.Values;
    
    private readonly Dictionary<string, IItem> _items = new();

    public void AddItem(IItem item)
    {
        _items[item.Name] = item;
    }

    public IItem? GetItem(string name)
    {
        _items.TryGetValue(name, out var item);
        return item;
    }

    public double GetSize()
    {
        return Items.Sum(i => i.GetSize());
    }
}