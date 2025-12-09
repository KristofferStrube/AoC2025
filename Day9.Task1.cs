string[] lines = File.ReadAllLines("./Input/Day9.txt");

List<(long x, long y)> redTiles = [];

foreach(var line in lines)
{
    var splitLine = line.Split(",");
    redTiles.Add((long.Parse(splitLine[0]), long.Parse(splitLine[1])));
}

long largest = 0;

foreach(var tile1 in redTiles)
{
    foreach(var tile2 in redTiles)
    {
        long area = (Math.Abs(tile1.x - tile2.x) + 1) * (Math.Abs(tile1.y - tile2.y) + 1);
        largest = Math.Max(area, largest);
    }
}

Console.WriteLine(largest);