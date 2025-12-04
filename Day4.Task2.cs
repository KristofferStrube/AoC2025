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

int[,] neighbourCount = new int[lines.Count, lines.First().Length];

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines.First().Length; j++)
    {
        if (lines[i][j] is not '@')
        {
            neighbourCount[i, j] = -1;
            continue;
        }
        int numberOfNeighbours = 0;
        foreach((int x, int y)? neighbour in Neighbours(i, j))
        {
            if (neighbour is null)
            {
                continue;
            }
            (int x, int y) = neighbour.Value;
            if (lines[x][y] is '@')
            {
                numberOfNeighbours++;
            }
        }
        neighbourCount[i, j] = numberOfNeighbours;
    }
}

bool anyRollRemovedInIteration = false;
int rollsRemoved = 0;

do
{
    anyRollRemovedInIteration = false;
    for (int i = 0; i < neighbourCount.GetLength(0); i++)
    {
        for (int j = 0; j < neighbourCount.GetLength(1); j++)
        {
            if (neighbourCount[i, j] is < 4 and >= 0)
            {
                anyRollRemovedInIteration = true;
                rollsRemoved++;
                neighbourCount[i, j] = -1;
                foreach((int x, int y)? neighbour in Neighbours(i, j))
                {
                    if (neighbour is null)
                    {
                        continue;
                    }
                    (int x, int y) = neighbour.Value;
                    neighbourCount[x, y]--;
                }
            }
        }
    }
} while(anyRollRemovedInIteration);

IEnumerable<(int x, int y)?> Neighbours(int x, int y)
{
    yield return GetPosition(x - 1, y - 1);
    yield return GetPosition(x - 1, y);
    yield return GetPosition(x - 1, y + 1);
    yield return GetPosition(x, y - 1);
    yield return GetPosition(x, y + 1);
    yield return GetPosition(x + 1, y - 1);
    yield return GetPosition(x + 1, y);
    yield return GetPosition(x + 1, y + 1);
}

(int x, int y)? GetPosition(int x, int y)
{
    if (x < 0 || x == lines.Count)
    {
        return null;
    }
    if (y < 0 || y == lines.First().Length)
    {
        return null;
    }
    return (x,y);
}

Console.WriteLine(rollsRemoved);