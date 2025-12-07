string[] lines = File.ReadAllLines("./Input/Day7.txt");


HashSet<int> beams = [];

string firstLine = lines.First();

for (int i = 0; i < firstLine.Length; i++)
{
    if (firstLine[i] == 'S')
    {
        beams.Add(i);
    }
}

long count = 0;

for (int i = 1; i < lines.Length; i++)
{
    string line = lines[i];
    HashSet<int> newBeams = [];
    foreach (int beam in beams)
    {
        if (line[beam] == '^')
        {
            newBeams.Add(beam - 1);
            newBeams.Add(beam + 1);
            count++;
        }
        else
        {
            newBeams.Add(beam);
        }
    }
    beams = newBeams;
}

Console.WriteLine(count);