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


//---- Private Methods -----
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
