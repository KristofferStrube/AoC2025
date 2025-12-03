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

int sum = 0;

foreach(var line in lines)
{
    int largestBatteryJolt = 0;
    int largestBatteryJoltIndex = 0;
    int secondLargestBatteryJolt = 0;
    int secondLargestBatteryJoltIndex = 0;

    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] > largestBatteryJolt)
        {
            secondLargestBatteryJolt = largestBatteryJolt;
            secondLargestBatteryJoltIndex = largestBatteryJoltIndex;
            largestBatteryJolt = line[i];
            largestBatteryJoltIndex = i;
        }
    }

    if (largestBatteryJoltIndex == line.Length - 1)
    {
        sum += secondLargestBatteryJolt * 10 + largestBatteryJolt;
        continue;
    }

    int bestBatteryAfterFirst = 0;

    for (int i = largestBatteryJoltIndex + 1; i < line.Length; i++)
    {
        if (line[i] > bestBatteryAfterFirst)
        {
            bestBatteryAfterFirst = line[i];
        }
    }

    sum += line[largestBatteryJoltIndex] * 10 + bestBatteryAfterFirst;
}

Console.WriteLine(sum);