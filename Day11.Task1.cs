string[] lines = File.ReadAllLines("./Input/Day11.txt");

int start = 0;
Dictionary<string, int> index = new();

for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var key = line[..3];
    index[key] = i;
    if (key == "you")
    {
        start = i;
    }
}

Dictionary<int, int> cache = new();

int PathsToOut(int i)
{
    if (cache.TryGetValue(i, out int cachedValue))
    {
        return cachedValue;
    }
    var line = lines[i];
    var paths = line[5..].Split(" ");
    int outPathCount = 0;
    foreach(var path in paths)
    {
        if (path == "out")
        {
            outPathCount++;
        }
        else
        {
            outPathCount += PathsToOut(index[path]);
        }
    }
    cache[i] = outPathCount;
    return outPathCount;
}

var result = PathsToOut(start);

Console.WriteLine(result);