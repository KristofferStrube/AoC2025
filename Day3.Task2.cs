List<int[]> lines = [];

while(true)
{
    var input = Console.ReadLine();
    if (input == "")
    {
        break;
    }
    lines.Add(input!.Select(c => int.Parse($"{c}")).ToArray());
}

long sum = 0;

foreach(var line in lines)
{
    long totalBatteryValue = 0;

    int startOfJoltRange = 0;

    for (int i = 11; i >= 0; i--)
    {
        int largestBatteryJolt = 0;
        int largestBatteryJoltIndex = 0;
        for (int j = startOfJoltRange; j < line.Length - i; j++)
        {
            if (line[j] > largestBatteryJolt)
            {
                largestBatteryJolt = line[j];
                largestBatteryJoltIndex = j;
            }
        }
        totalBatteryValue = totalBatteryValue * 10 + largestBatteryJolt;
        startOfJoltRange = largestBatteryJoltIndex + 1;
    }

    sum += totalBatteryValue;
}

Console.WriteLine(sum);