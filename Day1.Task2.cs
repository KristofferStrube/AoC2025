
List<string> lines = [];

while(true)
{
    var input = Console.ReadLine();
    if (input == "")
    {
        break;
    }
    lines.Add(input!);
}

int count = 0;

int dialPosition = 50;

foreach(string instruction in lines)
{
    int previousDialPosition = dialPosition;

    var direction = instruction[0];
    int distance = int.Parse(instruction[1..]);

    if (direction == 'L')
    {
        for (int i = 0; i < distance; i++)
        {
            dialPosition = (dialPosition + 100 - 1) % 100;
            if (dialPosition == 0)
            {
                count++;
            }
        }
    }
    else
    {
        for (int i = 0; i < distance; i++)
        {
            dialPosition = (dialPosition + 1) % 100;
            if (dialPosition == 0)
            {
                count++;
            }
        }
    }
}

Console.WriteLine(count);