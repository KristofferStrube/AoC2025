#:package Google.OrTools@9.14.6206
using Google.OrTools.Sat;

string[] lines = File.ReadAllLines("./Input/Day10.txt");

long sum = 0;

foreach(var line in lines)
{
    var splitLine = line.Split(" ");

    var goal = splitLine[^1][1..^1]
        .Split(",")
        .Select(s => int.Parse(s))
        .ToArray();

    var buttonWirings = splitLine[1..^1]
        .Select(
            buttonWiring => buttonWiring[1..^1]
                .Split(",")
                .Select(s => int.Parse(s))
                .ToArray()
        ).ToArray();

    int maxGoal = goal.Max();

    CpModel model = new CpModel();

    IntVar[] x = new IntVar[buttonWirings.Length];

    for (int i = 0; i < buttonWirings.Length; i++)
    {
        x[i] = model.NewIntVar(0, maxGoal, $"x{i+1}");
    }

    for (int i = 0; i < goal.Length; i++)
    {
        List<LinearExpr> terms = new();

        for (int j = 0; j < buttonWirings.Length; j++)
        {
            if (buttonWirings[j].Contains(i))
            {
                terms.Add(x[j]);
            }
        }

        LinearExpr equation = LinearExpr.Sum(terms);

        model.Add(equation == goal[i]);
    }

    List<LinearExpr> minimizeTerms = new();
    
    for (int i = 0; i < buttonWirings.Length; i++)
    {
        minimizeTerms.Add(x[i]);
    }

    model.Minimize(LinearExpr.Sum(minimizeTerms));

    var solver = new CpSolver();
    var status = solver.Solve(model);

    if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
    {
        for (int i = 0; i < buttonWirings.Length; i++)
            sum += solver.Value(x[i]);
    }
}

Console.WriteLine(sum);