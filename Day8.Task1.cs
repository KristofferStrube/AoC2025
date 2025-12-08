string[] lines = File.ReadAllLines("./Input/Day8.txt");

List<(int x, int y, int z)> boxes = [];

foreach(var line in lines)
{
    var splitLine = line.Split(",");
    boxes.Add((int.Parse(splitLine[0]), int.Parse(splitLine[1]), int.Parse(splitLine[2])));
}

PriorityQueue<Connection, double> closestBoxes = new();

for(int i = 0; i < boxes.Count; i++)
{
    for(int j = 0; j < i; j++)
    {
        Connection connection = new(boxes[i], boxes[j]);
        closestBoxes.Enqueue(connection, Distance(connection));
    }
}

List<List<Connection>> connectedBoxes = [];

for (int i = 0; i < 1000; i++)
{
    Connection connection = closestBoxes.Dequeue();
    var existingClusters = connectedBoxes.Where(cluster =>
            cluster.Any(c => c.From == connection.From
            || c.From == connection.To
            || c.To == connection.From
            || c.To == connection.To)
        ).ToList();
    if (existingClusters.Count() == 1)
    {
        existingClusters.First().Add(connection);
    }
    else if (existingClusters.Count() > 1)
    {
        var firstCluster = existingClusters.First();
        foreach(var existingCluster in existingClusters.Skip(1))
        {
            connectedBoxes.Remove(existingCluster);
            foreach(var existingConnection in existingCluster)
            {
                firstCluster.Add(existingConnection);
            }
        }
        firstCluster.Add(connection);
    }
    else
    {
        connectedBoxes.Add([connection]);
    }
}

int sum = 1;

foreach(List<Connection> cluster in connectedBoxes.OrderByDescending(c => c.Count()).Take(3))
{
    int clusterSize = cluster.SelectMany(c => new List<(int x, int y, int z)>() { c.From, c.To }).Distinct().Count();
    sum *= clusterSize;
}

Console.WriteLine(sum);

double Distance(Connection connection)
{
    return Math.Sqrt(
        Math.Pow(connection.From.x - connection.To.x, 2)
        + Math.Pow(connection.From.y - connection.To.y, 2)
        + Math.Pow(connection.From.z - connection.To.z, 2)
    );
}

record Connection((int x, int y, int z) From, (int x, int y, int z) To)
{
    public List<Connection> ConnectedWith { get; set; } = new();
}