string[] lines = File.ReadAllLines("./Input/Day5.txt");

bool availableMode = false;

List<(long start, long end)> freshIngredientRanges = [];

int count = 0;

for (int i = 0; i < lines.Length; i++)
{
    if (availableMode)
    {
        long availableIngredient = long.Parse(lines[i]);
        bool fresh = false;
        foreach ((long start, long end) in freshIngredientRanges)
        {
            if (availableIngredient >= start && availableIngredient <= end)
            {
                fresh = true;
                break;
            }
        }
        if (fresh)
        {
            count++;
        }
    }
    else if (lines[i] == "")
    {
        availableMode = true;
    }
    else
    {
        string[] parts = lines[i].Split("-");
        freshIngredientRanges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
    }
}

Console.WriteLine(count);