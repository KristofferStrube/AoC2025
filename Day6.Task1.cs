string[] lines = File.ReadAllLines("./Input/Day6.txt");

int[][] numbers = lines.SkipLast(1).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

long sum = 0;

string[] operations = lines.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);

for (int i = 0; i < operations.Length; i++)
{
    string operation = operations[i];
    if (operation == "*")
    {
        long accumulator = 1;
        for (int j = 0; j < numbers.Length; j++)
        {
            accumulator *= numbers[j][i];
        }
        sum += accumulator;
    }
    else
    {
        long accumulator = 0;
        for (int j = 0; j < numbers.Length; j++)
        {
            accumulator += numbers[j][i];
        }
        sum += accumulator;
    }
}

Console.WriteLine(sum);