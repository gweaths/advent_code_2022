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

    public static bool Draws(this Shape player1, Shape player2) => player1 == player2;
}
