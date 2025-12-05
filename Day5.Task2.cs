string[] lines = File.ReadAllLines("./Input/Day5.txt");

List<(long start, long end)> freshIngredientRanges = [];

long count = 0;

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i] == "")
    {
        break;
    }
    else
    {
        string[] parts = lines[i].Split("-");
        List<(long start, long end)> rangesToInsert = [(long.Parse(parts[0]), long.Parse(parts[1]))];

        foreach (var range in freshIngredientRanges)
        {
            List<(long start, long end)> newRangesToInsert = [];
            foreach (var rangeToInsert in rangesToInsert)
            {
                if (range.start >= rangeToInsert.start && range.start <= rangeToInsert.end && range.end >= rangeToInsert.start && range.end <= rangeToInsert.end)
                {
                    if (range.start != rangeToInsert.start)
                    {
                        newRangesToInsert.Add((rangeToInsert.start, range.start - 1));
                    }
                    if (range.end != rangeToInsert.end)
                    {
                        newRangesToInsert.Add((range.end + 1, rangeToInsert.end));
                    }
                }
                else if (range.start <= rangeToInsert.start && range.end >= rangeToInsert.end)
                {
                    // Do nothing as we are already covered.
                }
                else if (range.start > rangeToInsert.start && range.start <= rangeToInsert.end)
                {
                    newRangesToInsert.Add((rangeToInsert.start, range.start - 1));
                }
                else if (range.end >= rangeToInsert.start && range.end < rangeToInsert.end)
                {
                    newRangesToInsert.Add((range.end + 1, rangeToInsert.end));
                }
                else
                {
                    newRangesToInsert.Add((rangeToInsert.start, rangeToInsert.end));
                }
            }
            rangesToInsert = newRangesToInsert;
        }

        foreach (var range in rangesToInsert)
        {
            freshIngredientRanges.Add(range);
            count += range.end - range.start + 1;
        }
    }
}

Console.WriteLine(count);