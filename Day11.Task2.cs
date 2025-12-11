string[] lines = File.ReadAllLines("./Input/Day11.txt");

int start = 0;
Dictionary<string, int> index = new();

for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var key = line[..3];
    index[key] = i;
    if (key == "svr")
    {
        start = i;
    }
}

Dictionary<(int i, bool passedDac, bool passedFft), long> cache = new();

long PathsToOut(int i, bool passedDac, bool passedFft)
{
    if (cache.TryGetValue((i, passedDac, passedFft), out long cachedValue))
    {
        return cachedValue;
    }
    var line = lines[i];
    var paths = line[5..].Split(" ");
    long outPathCount = 0;
    foreach(var path in paths)
    {
        if (path == "out")
        {
            if (passedDac && passedFft)
            {
                outPathCount++;
            }
        }
        else if (path == "dac")
        {
            outPathCount += PathsToOut(index[path], true, passedFft);
        }
        else if (path == "fft")
        {
            outPathCount += PathsToOut(index[path], passedDac, true);
        }
        else
        {
            outPathCount += PathsToOut(index[path], passedDac, passedFft);
        }
    }
    cache[(i, passedDac, passedFft)] = outPathCount;
    return outPathCount;
}

var result = PathsToOut(start, false, false);

Console.WriteLine(result);