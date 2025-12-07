using System.Runtime.InteropServices;

string[] lines = File.ReadAllLines("./Input/Day7.txt");

string firstLine = lines.First();

int start = 0;

for (int i = 0; i < firstLine.Length; i++)
{
    if (firstLine[i] == 'S')
    {
        start = i;
    }
}

Dictionary<(int row, int column), long> cache = new();

long NumberOfTimelines(int row, int column)
{
    if (cache.TryGetValue((row, column), out long cachedValue))
    {
        return cachedValue;
    }
    if (row == lines.Length)
    {
        return 1;
    }
    if (lines[row][column] == '^')
    {
        long timelines = NumberOfTimelines(row, column - 1) + NumberOfTimelines(row, column + 1);
        cache.Add((row, column), timelines);
        return timelines;
    }
    return NumberOfTimelines(row + 1, column);
}

Console.WriteLine(NumberOfTimelines(0, start));