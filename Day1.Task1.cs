
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
    var direction = instruction[0];
    int distance = int.Parse(instruction[1..]);

    if (direction == 'L')
    {
        dialPosition = (dialPosition + 100 - distance) % 100;
    }
    else
    {
        dialPosition = (dialPosition + distance) % 100;
    }
    if (dialPosition == 0)
    {
        count++;
    }
}

Console.WriteLine(count);