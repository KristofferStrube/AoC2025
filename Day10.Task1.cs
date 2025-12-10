string[] lines = File.ReadAllLines("./Input/Day10.txt");

int sum = 0;

foreach(var line in lines)
{
    var splitLine = line.Split(" ");
    var goal = splitLine[0][1..^1]
        .Select(c => c == '#')
        .ToArray();
        
    var buttonWirings = splitLine[1..^1]
        .Select(
            buttonWiring => buttonWiring[1..^1]
                .Split(",")
                .Select(s => int.Parse(s))
                .ToArray()
        ).ToArray();

    int minCost = int.MaxValue;
    for (int i = 0; i < Math.Pow(2, buttonWirings.Length); i++)
    {
        int cost = 0;
        bool[] result = new bool[goal.Length];
        for (int j = 0; j < buttonWirings.Length; j++)
        {
            if (((i >> j) & 1) == 1)
            {
                cost++;
                int[] buttonWiring = buttonWirings[j];
                foreach(int wiring in buttonWiring)
                {
                    result[wiring] = !result[wiring];
                }
            } 
        }
        if (result.SequenceEqual(goal) && cost < minCost)
        {
            minCost = cost;
        }
    }
    sum += minCost;
}

Console.WriteLine(sum);