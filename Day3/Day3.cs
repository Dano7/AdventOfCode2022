using System.Text;

const int lowerVal = 'a' - 1;
const int upperVal = 'A' - 27;

const string testInput = "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw";
var day3Input = File.ReadAllText("..\\..\\..\\..\\input\\Day3.txt", Encoding.UTF8);

int SolvePart1(string input)
{
	var rucksacks = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
	var badItems = rucksacks.Select(s => FindCommonItem(Compartments(s)));
	var sumPriorities = badItems.Sum(GetPriority);
	return sumPriorities;
}

Console.WriteLine($"Part1 test sum: {SolvePart1(testInput)}");
Console.WriteLine($"Part1 sum: {SolvePart1(day3Input)}");
Console.WriteLine($"Part2 test sum: {SolvePart2(testInput)}");
Console.WriteLine($"Part2 sum: {SolvePart2(day3Input)}");

HashSet<char> IntersectingAccumulator(HashSet<char> set, string s)
{
	set.IntersectWith(s);
	return set;
}

int SolvePart2(string input)
{
	var rucksacks = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
	var groups = rucksacks.Chunk(3);
	var badgeTypes = groups.Select(strings => strings.Skip(1)
			.Aggregate(new HashSet<char>(strings.First().ToCharArray()), IntersectingAccumulator))
		.Select(set => set.Single());
	return badgeTypes.Sum(GetPriority);
}

char FindCommonItem((string a, string b) compartment)
{
	var item = compartment.a.Intersect(compartment.b).Single();
	return item;
}

int GetPriority(char item)
{
	if (char.IsUpper(item)) return item - upperVal;

	return item - lowerVal;
}

(string, string) Compartments(string rucksack)
{
	var length = rucksack.Length;
	if (length % 2 != 0)
		throw new ArgumentException($"Rucksack has odd lenght: {length} for {rucksack}");

	var first = rucksack.Substring(0, length / 2);
	var second = rucksack.Substring(length / 2, length / 2);

	return (first, second);
}