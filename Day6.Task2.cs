string[] lines = File.ReadAllLines("./Input/Day6.txt");

List<int> numbersInCurrentProblem = [];

long sum = 0;

for (int i = lines.First().Length - 1; i >= 0; i--)
{
    int currentNumber = 0;
    for (int j = 0; j < lines.Length; j++)
    {
        char symbol = lines[j][i];
        if (int.TryParse($"{symbol}", out int parsedSymbol))
        {
            currentNumber = currentNumber * 10 + parsedSymbol;
        }
        if (j == lines.Length - 2 && currentNumber != 0)
        {
            numbersInCurrentProblem.Add(currentNumber);
        }
        else if (symbol == '+')
        {
            long accumulator = 0;
            foreach(var number in numbersInCurrentProblem)
            {
                accumulator += number;
            }
            sum += accumulator;
            numbersInCurrentProblem.Clear();
        }
        else if (symbol == '*')
        {
            long accumulator = 1;
            foreach(var number in numbersInCurrentProblem)
            {
                accumulator *= number;
            }
            sum += accumulator;
            numbersInCurrentProblem.Clear();
        }
    }
}

Console.WriteLine(sum);