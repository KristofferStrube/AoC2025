string line = Console.ReadLine();

(ulong First, ulong Last)[] ranges = line
    .Split(",")
    .Select(range =>
    {
        var parts = range.Split("-");
        return (ulong.Parse(parts[0]), ulong.Parse(parts[1]));
    })
    .ToArray();

ulong sum = 0;

foreach((ulong First, ulong Last) in ranges)
{
    for (ulong i = First; i <= Last; i++)
    {
        if (IsInvalid(i.ToString()))
        {
            sum += i;
        }
    }
}

bool IsInvalid(string id)
{
    if (id.Length % 2 != 0)
        return false;

    bool equal = true;

    for (int i = 0; i < id.Length / 2; i++)
    {
        if (id[i] != id[i + id.Length / 2])
        {
            return false;
        }
    }

    return equal;
}

Console.WriteLine(sum);