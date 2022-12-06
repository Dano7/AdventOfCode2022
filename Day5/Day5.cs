using System.Runtime.CompilerServices;
using System.Text;

var input = File.ReadAllText("..\\..\\..\\..\\input\\Day5.txt", Encoding.UTF8);
var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
var stacksLine = lines.First(s => !s.Contains('['));
var stacknums = stacksLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
var stackLines = lines.Take(Array.IndexOf(lines, stacksLine)).Reverse().ToArray();
var stacks = stacknums.ToDictionary(Convert.ToInt32, stacknum => new Stack<char>());

BuildStacks(stackLines, stacks);
void BuildStacks(IEnumerable<string> enumerable, Dictionary<int, Stack<char>> dictionary)
{
	foreach (var line in enumerable)
		for (var i = 1; i < line.Length; i += 4)
		{
			var stacknum = (i + 3) / 4;
			var crate = line[i];
			if (crate != ' ') dictionary[stacknum].Push(crate);
		}
}

var instructionLines = lines.Skip(Array.IndexOf(lines, stacksLine) + 1).ToArray();
var moves = instructionLines.Select(s =>
{
	var x = s.Split(' ').Where((s, i) => i % 2 == 1).ToArray();
	return (count: Convert.ToInt32(x[0]), from: Convert.ToInt32(x[1]), to: Convert.ToInt32(x[2]));
}).ToArray();

ExecuteMoves(stacks, moves);

void ExecuteMoves(Dictionary<int, Stack<char>> s, (int count, int from, int to)[] m)
{
	foreach (var move in m)
		for (var i = 0; i < move.count; i++)
			s[move.to].Push(s[move.from].Pop());
}

Console.WriteLine($"Message: {(string?)stacks.Aggregate(string.Empty, (current, stack) => string.Concat(current, stack.Value.Peek()))}");

//Part 2
// rebuild stacks
var stacks2 = stacknums.ToDictionary(Convert.ToInt32, stacknum => new Stack<char>());
BuildStacks(stackLines, stacks2);
ExecuteMovesPart2(stacks2, moves);

void ExecuteMovesPart2(Dictionary<int, Stack<char>> s, (int count, int from, int to)[] m)
{
	foreach (var move in m)
	{
		var bundle = string.Empty;
		for (var i = 0; i < move.count; i++) 
			bundle = string.Concat(s[move.from].Pop(), bundle);
		foreach (var crate in bundle) 
			s[move.to].Push(crate);
	}
}

Console.WriteLine($"Message Part2: {(string?)stacks2.Aggregate(string.Empty, (current, stack) => string.Concat(current, stack.Value.Peek()))}");