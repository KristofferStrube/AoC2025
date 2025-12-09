string[] lines = File.ReadAllLines("./Input/Day9.txt");

List<(int x, int y)> redTiles = [];

foreach(var line in lines)
{
    var splitLine = line.Split(",");
    redTiles.Add((int.Parse(splitLine[0]), int.Parse(splitLine[1])));
}

int size = 100_000;

List<List<Color>> grid = new(size);

for(int i = 0; i < size; i++)
{
    grid.Add(new List<Color>(new Color[size]));
}

for (int i = 0; i < redTiles.Count; i++)
{
    var prev = redTiles[(i - 1 + redTiles.Count) % redTiles.Count];
    var curr = redTiles[i];
    grid[curr.x][curr.y] = Color.Red;
    if (prev.x == curr.x)
    {
        for (int j = Math.Min(prev.y, curr.y) + 1; j < Math.Max(prev.y, curr.y); j++)
        {
            grid[curr.x][j] = Color.Green;
        }
    }
    else
    {
        for (int j = Math.Min(prev.x, curr.x) + 1; j < Math.Max(prev.x, curr.x); j++)
        {
            grid[j][curr.y] = Color.Green;
        }
    }
    Console.WriteLine("Drew: " + i);
}

for (int i = 0; i < redTiles.Count; i++)
{
    var prev = redTiles[(i - 1 + redTiles.Count) % redTiles.Count];
    var curr = redTiles[i];
    grid[curr.x][curr.y] = Color.Red;
    if (prev.x == curr.x)
    {
        if (curr.y > prev.y)
        {
            // Going down
            for (int j = prev.y + 1; j < curr.y; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[curr.x - offset][j] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    grid[curr.x - offset][j] = Color.Green;
                    offset++;
                }
            }
        }
        else
        {
            // Going up
            for (int j = curr.y + 1; j < prev.y; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[curr.x + offset][j] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    grid[curr.x + offset][j] = Color.Green;
                    offset++;
                }
            }
        }
    }
    else
    {
        if (curr.x > prev.x)
        {
            // Going right
            for (int j = prev.x + 1; j < curr.x; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[j][curr.y + offset] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    grid[j][curr.y + offset] = Color.Green;
                    offset++;
                }
            }
        }
        else
        {
            // Going left
            for (int j = curr.x + 1; j < prev.x; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[j][curr.y - offset] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    grid[j][curr.y - offset] = Color.Green;
                    offset++;
                }
            }
        }
    }
    Console.WriteLine("Filled: " + i);
}

long largest = 0;

for (int i = 0; i < redTiles.Count; i++)
{
    var tile1 = redTiles[i];
    for (int j = 0; j < i; j++)
    {
        var tile2 = redTiles[j];
        long area = (long)(Math.Abs(tile1.x - tile2.x) + 1) * (long)(Math.Abs(tile1.y - tile2.y) + 1);
        if (area > largest)
        {
            bool allRedOrGreen = true;
            if (tile1.x < tile2.x && tile1.y < tile2.y)
            {
                for (int x = tile1.x; x <= tile2.x; x++)
                {
                    for (int y = tile1.y; y <= tile2.y; y++)
                    {
                        if (grid[x][y] is Color.Nothing)
                        {
                            allRedOrGreen = false;
                            break;
                        }
                    }
                    if (!allRedOrGreen)
                    {
                        break;
                    }
                }
            }
            else if (tile1.x < tile2.x && tile2.y < tile1.y)
            {
                for (int x = tile1.x; x <= tile2.x; x++)
                {
                    for (int y = tile2.y; y <= tile1.y; y++)
                    {
                        if (grid[x][y] is Color.Nothing)
                        {
                            allRedOrGreen = false;
                            break;
                        }
                    }
                    if (!allRedOrGreen)
                    {
                        break;
                    }
                }
            }
            else if (tile2.x < tile1.x && tile1.y < tile2.y)
            {
                for (int x = tile2.x; x <= tile1.x; x++)
                {
                    for (int y = tile1.y; y <= tile2.y; y++)
                    {
                        if (grid[x][y] is Color.Nothing)
                        {
                            allRedOrGreen = false;
                            break;
                        }
                    }
                    if (!allRedOrGreen)
                    {
                        break;
                    }
                }
            }
            else if (tile2.x < tile1.x && tile2.y < tile1.y)
            {
                for (int x = tile2.x; x <= tile1.x; x++)
                {
                    for (int y = tile2.y; y <= tile1.y; y++)
                    {
                        if (grid[x][y] is Color.Nothing)
                        {
                            allRedOrGreen = false;
                            break;
                        }
                    }
                    if (!allRedOrGreen)
                    {
                        break;
                    }
                }
            }
            if (allRedOrGreen)
            {
                largest = area;
                Console.WriteLine("Largest so far: " + area);
            }
        }
    }
    Console.WriteLine("Checked tile " + i + " with all other tiles.");
}

Console.WriteLine(largest);

enum Color : byte
{
    Nothing,
    Red,
    Green,
}