using System.Diagnostics;
using System.Globalization;
using LogParser;
using File = System.IO.File;

var filePath = Console.ReadLine();
if (string.IsNullOrEmpty(filePath))
    return;

filePath = filePath.Replace("\"", "");

var root = new FolderItem("root");

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

foreach (var line in File.ReadLines(filePath)
             .SkipWhile(l => !l.EndsWith("size:"))
             .Skip(1)
             .TakeWhile(l => !l.EndsWith('-')))
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var size = double.Parse(split[1]) * split[2].Trim() switch
    {
        "kb" => 1,
        "mb" => 1024,
        _ => throw new Exception($"wtf is {split[2]}???")
    };

    var parentFolder = root;
    var path = split[4].Split('/');
    foreach (var folderName in path[..^1])
    {
        var folder = parentFolder.GetItem(folderName);
        if (folder == null)
        {
            folder = new FolderItem(folderName);
            parentFolder.AddItem(folder);
        }

        parentFolder = (FolderItem)folder;
    }

    parentFolder.AddItem(new FileItem(path[^1], size));
}

var output = new StreamWriter("output.txt");

PrintItem(output, root, 0);
output.Close();
Process.Start("notepad.exe", "output.txt");
return;

void PrintItem(StreamWriter stream, IItem item, int depth)
{
    stream.WriteLine(new string(' ', depth * 4) + $"{item.Name} {item.GetSize():F} kb");
    
    if (item is not FolderItem folder) return;
    
    foreach (var child in folder.Items.OrderByDescending(k => k.GetSize()))
    {
        PrintItem(stream, child, depth + 1);
    }
}