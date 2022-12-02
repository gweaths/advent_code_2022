internal class Program
{
    private static void Main(string[] args)
    {
        //Read input
        var input = File.ReadAllText("input.txt");

        var elfCalories = GetElfCalories(input);
        var topElfCalories = elfCalories.OrderByDescending(x => x).Take(3);

        Console.WriteLine($"Top 3 Elf Calories: {topElfCalories.Sum()}");
        Console.WriteLine($"Highest Elf Calories: {topElfCalories.First()}");
    }

    public static List<int> GetElfCalories(string input)
    {
        return input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(
                elf => elf.Split("\n").ToArray()
                .Select(x => int.Parse(x))
                .Sum())
                .ToList();


    }
}