namespace LogParser;

public class FolderItem(string name) : IItem
{
    public string Name { get; } = name;

    public IReadOnlyCollection<IItem> Items => _items.Values;
    
    private readonly Dictionary<string, IItem> _items = new();

    private double _size;
    private bool _isSizeCalculated;

    public void AddItem(IItem item)
    {
        _items[item.Name] = item;
        _isSizeCalculated = false;
    }

    public IItem? GetItem(string name)
    {
        _items.TryGetValue(name, out var item);
        return item;
    }

    public double GetSize()
    {
        if(_isSizeCalculated)
            return _size;
        
        return _size = Items.Sum(i => i.GetSize());
    }
}