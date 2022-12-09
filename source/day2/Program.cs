void Part1()
{
    string[] input = File.ReadAllLines("input.txt");
    // string[] input = File.ReadAllLines("Sample.txt");
    int totalScore = 0;

    foreach (string game in input)
    {
        Shape[] moves = game.Split().Select(s => CharToShape(s[0])).ToArray();
        Shape opponent = moves[0];
        Shape player = moves[1];
        totalScore += GetShapeValue(opponent) + GetResults(opponent, player);
    }

    Console.WriteLine("Total Score: " + totalScore);
}

void Part2()
{
    string[] input = File.ReadAllLines("Input.txt");
    int totalScore = 0;
    foreach (string game in input)
    {
        string[] predicted = game.Split();
        Console.WriteLine(predicted[0][0]);
        Shape opponent = CharToShape(predicted[0][0]);
        Outcome outcome = CharToOutcome(predicted[1][0]);

        Shape player = outcome switch
        {
            Outcome.Win => opponent.LosesTo(),
            Outcome.Draw => opponent,
            Outcome.Lose => opponent.Defeats(),
            _ => throw new Exception("Could not determine outcome")
        };

        totalScore += GetShapeValue(player);
        totalScore += GetResults(player, opponent);
        Console.WriteLine($"Total Score: {totalScore}");
    }
}

Part1();
Part2();

//---- Private Methods -----
Outcome CharToOutcome(char ch)
{
    return ch switch
    {
        'X' => Outcome.Lose,
        'Y' => Outcome.Draw,
        'Z' => Outcome.Win,
        _ => throw new Exception("Invalid character")
    };
}

Shape CharToShape(char ch)
{
    return ch switch
    {
        'A' => Shape.Rock,
        'B' => Shape.Paper,
        'C' => Shape.Scissors,
        'X' => Shape.Rock,
        'Y' => Shape.Paper,
        'Z' => Shape.Scissors,
        _ => throw new Exception($"Invalid character : {ch}")
    };
}

int GetShapeValue(Shape shape)
{
    return shape switch
    {
        Shape.Rock => 1,
        Shape.Paper => 2,
        Shape.Scissors => 3,
        _ => throw new Exception($"Invalid shape provided: {shape}")
    };
}

int GetResults(Shape player, Shape opponent)
{
    if (player.Defeats(opponent))
    {
        return 6;
    }
    else if (player.Draws(opponent))
    {
        return 3;
    }
    else
    {
        return 0;
    }
}

enum Outcome
{
    Win,
    Draw,
    Lose
}
