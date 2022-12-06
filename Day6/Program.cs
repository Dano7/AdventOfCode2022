using System.Text;

var input = File.ReadAllText("..\\..\\..\\..\\input\\Day6.txt", Encoding.UTF8);

List<(string a, int i, int j)> testCases = new List<(string a, int i, int j)>()
{
	("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19),
	("bvwbjplbgvbhsrlpgdmjqwftvncz",5,23),
	("nppdvjthqldpwncqszvftbrmjlhg", 6,23),
	("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",10,29),
	("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",11,26),
};

int StartOfPacketIndex(string message)
{
	var checkBuffer = new HashSet<char>();
	List<char> lastFour = new List<char>(message.Take(3));
	
	for (int i = 3; i < message.Length; i++)
	{
		lastFour.Add(message[i]);

		if (lastFour.All(checkBuffer.Add))
		{
			return i+1;
		}
		checkBuffer.Clear();
		lastFour.RemoveAt(0);
		
	}
	return message.Length;
}

foreach ((string a, int i, int ) testCase in testCases)
{
	Console.WriteLine($"{StartOfPacketIndex(testCase.a)} =? {testCase.i}");
}

Console.WriteLine($"Part1: {StartOfPacketIndex(input)}");

int StartOfMessageIndex(string message)
{
	var checkBuffer = new HashSet<char>();
	List<char> lastFour = new List<char>(message.Take(13));

	for (int i = 13; i < message.Length; i++)
	{
		lastFour.Add(message[i]);

		if (lastFour.All(checkBuffer.Add))
		{
			return i + 1;
		}
		checkBuffer.Clear();
		lastFour.RemoveAt(0);

	}
	return message.Length;
}

foreach ((string a, int i, int j) testCase in testCases)
{
	Console.WriteLine($"{StartOfMessageIndex(testCase.a)} =? {testCase.j}");
}

Console.WriteLine($"Part1: {StartOfMessageIndex(input)}");

