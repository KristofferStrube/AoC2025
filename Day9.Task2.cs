string[] lines = File.ReadAllLines("./Input/Day9.txt");

List<(int x, int y)> redTiles = [];

foreach(var line in lines)
{
    var splitLine = line.Split(",");
    redTiles.Add((int.Parse(splitLine[0]), int.Parse(splitLine[1])));
}

int size = 100_000;

List<List<Color>> grid = new(size);
List<List<Color>> gridFilled = new(size);

for(int i = 0; i < size; i++)
{
    grid.Add(new List<Color>(new Color[size]));
    gridFilled.Add(new List<Color>(new Color[size]));
}

for (int i = 0; i < redTiles.Count; i++)
{
    var prev = redTiles[(i - 1 + redTiles.Count) % redTiles.Count];
    var curr = redTiles[i];
    grid[curr.x][curr.y] = Color.Red;
    gridFilled[curr.x][curr.y] = Color.Red;
    if (prev.x == curr.x)
    {
        for (int j = Math.Min(prev.y, curr.y) + 1; j < Math.Max(prev.y, curr.y); j++)
        {
            grid[curr.x][j] = Color.Green;
            gridFilled[curr.x][j] = Color.Green;
        }
    }
    else
    {
        for (int j = Math.Min(prev.x, curr.x) + 1; j < Math.Max(prev.x, curr.x); j++)
        {
            grid[j][curr.y] = Color.Green;
            gridFilled[j][curr.y] = Color.Green;
        }
    }
    Console.WriteLine("Drew: " + i);
}

for (int i = 0; i < redTiles.Count; i++)
{
    var prev = redTiles[(i - 1 + redTiles.Count) % redTiles.Count];
    var curr = redTiles[i];
    if (prev.x == curr.x)
    {
        if (curr.y > prev.y)
        {
            // Going down
            for (int j = prev.y; j <= curr.y; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[curr.x - offset][j] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    gridFilled[curr.x - offset][j] = Color.Green;
                    offset++;
                }
            }
        }
        else
        {
            // Going up
            for (int j = curr.y; j <= prev.y; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[curr.x + offset][j] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    gridFilled[curr.x + offset][j] = Color.Green;
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
            for (int j = prev.x; j <= curr.x; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[j][curr.y + offset] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    gridFilled[j][curr.y + offset] = Color.Green;
                    offset++;
                }
            }
        }
        else
        {
            // Going left
            for (int j = curr.x; j <= prev.x; j++)
            {
                int offset = 1;
                while (true)
                {
                    if (grid[j][curr.y - offset] is Color.Green or Color.Red)
                    {
                        break;
                    }
                    gridFilled[j][curr.y - offset] = Color.Green;
                    offset++;
                }
            }
        }
    }
    Console.WriteLine("Filled: " + i);
}

long largest = 0;

Parallel.For(0, 10, t =>
{
    for (int i = t; i < redTiles.Count; i+=10)
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
                            if (gridFilled[x][y] is Color.Nothing)
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
                            if (gridFilled[x][y] is Color.Nothing)
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
                            if (gridFilled[x][y] is Color.Nothing)
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
                            if (gridFilled[x][y] is Color.Nothing)
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
                    if (area > largest)
                    {
                        largest = area;
                        Console.WriteLine("Largest so far: " + largest);
                    }
                }
            }
        }
        Console.WriteLine($"Checked tile {i} with first {i} tiles.");
    }
});

Console.WriteLine($"Final result: {largest}");

enum Color : byte
{
    Nothing,
    Red,
    Green,
}