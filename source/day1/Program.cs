//Read input
var input = File.ReadAllText("input.txt");

//get Top Elf's Calories
var topElfCalories = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(
        elf => elf.Split("\n").ToArray()
        .Select(x => int.Parse(x))
        .Sum()).Max();

Console.WriteLine(topElfCalories);