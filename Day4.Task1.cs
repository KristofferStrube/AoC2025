List<string> lines = [];
while(true)
{
    var input = Console.ReadLine();
    if (input == "")
    {
        break;
    }
    lines.Add(input);
}

int count = 0;

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines.First().Length; j++)
    {
        if (lines[x][y] != '@')
        {
            continue;
        }
        if (RollIsFreeEnough(i, j))
        {
            count++;
        }
    }
}

bool RollIsFreeEnough(int x, int y)
{
    int rollsAsNeighbours = 0;
    foreach(char? neighbour in Neighbours(x, y))
    {
        if (neighbour is '@')
        {
            rollsAsNeighbours++;
        }
    }
    return rollsAsNeighbours < 4;
}

IEnumerable<char?> Neighbours(int x, int y)
{
    yield return GetAtPosition(x - 1, y - 1);
    yield return GetAtPosition(x - 1, y);
    yield return GetAtPosition(x - 1, y + 1);
    yield return GetAtPosition(x, y - 1);
    yield return GetAtPosition(x, y + 1);
    yield return GetAtPosition(x + 1, y - 1);
    yield return GetAtPosition(x + 1, y);
    yield return GetAtPosition(x + 1, y + 1);
}

char? GetAtPosition(int x, int y)
{
    if (x < 0 || x == lines.Count)
    {
        return null;
    }
    if (y < 0 || y == lines.First().Length)
    {
        return null;
    }
    return lines[x][y];
}

Console.WriteLine(count);