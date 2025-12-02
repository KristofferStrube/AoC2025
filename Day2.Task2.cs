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
    for (int i = 1; i <= id.Length / 2; i++)
    {
        if (id.Length % i != 0)
        {
            continue;
        }

        bool repeatingPatternFound = true;

        for (int j = 0; j < i; j++)
        {
            bool allIndeciesAreEqual = true;
            for (int k = 1; k < id.Length / i; k++)
            {
                if (id[j] != id[j + k * i])
                {
                    allIndeciesAreEqual = false;
                    break;
                }
            }
            if (!allIndeciesAreEqual)
            {
                repeatingPatternFound = false;
                break;
            }
        }

        if (repeatingPatternFound)
        {
            return true;
        }
    }

    return false;
}

Console.WriteLine(sum);