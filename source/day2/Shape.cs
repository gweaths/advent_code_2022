public enum Shape
{
    Rock,
    Paper,
    Scissors
}

public static class ShapeExtensions
{
    public static bool Defeats(this Shape player1, Shape player2)
    {
        return player1 == Shape.Rock && player2 == Shape.Scissors
            || player1 == Shape.Scissors && player2 == Shape.Paper
            || player1 == Shape.Paper && player2 == Shape.Rock;
    }

    public static Shape Defeats(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new Exception("Invalid shape provided")
        };
    }

    public static Shape LosesTo(this Shape shape)
    {
        return shape switch
        {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new Exception("Invalid shape provided")
        };
    }

    public static bool Draws(this Shape player1, Shape player2) => player1 == player2;
}
