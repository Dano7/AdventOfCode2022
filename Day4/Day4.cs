using System.Diagnostics;
using System.Text;

var input = File.ReadAllText("..\\..\\..\\..\\input\\Day4.txt", Encoding.UTF8);

(Range a, Range b)[] GetRanges(string[] strings)
{
	return strings.Select(s =>
	{
		var pairs = s.Split(',');
		var ranges = pairs.Select(s1 =>
		{
			var range = s1.Split('-');
			return new Range(Convert.ToInt32(range[0]), Convert.ToInt32(range[1]));
		}).ToArray();

		return (a: ranges[0], b: ranges[1]);
	}).ToArray();
}

static bool IsOneRangeFullyContainedByOther((Range a, Range b) ranges)
{
	var start1 = ranges.a.Start.Value;
	var start2 = ranges.b.Start.Value;
	var end1 = ranges.a.End.Value;
	var end2 = ranges.b.End.Value;

	var bContainsA = start1 >= start2
					 && start1 <= end2
					 && end1 <= end2;
	var AContainsB = start2 >= start1
					 && start2 <= end1
					 && end1 >= end2;
	return bContainsA
		   || AContainsB;
}

static bool IsAnyOverlapInRanges((Range a, Range b) ranges)
{
	var start1 = ranges.a.Start.Value;
	var start2 = ranges.b.Start.Value;
	var end1 = ranges.a.End.Value;
	var end2 = ranges.b.End.Value;

	var Overlap = start1 <= end2 && end1 >= start2;

	var DontOverlap = end1 < start2 || start1 > end2;
	Debug.Assert(Overlap != DontOverlap);
	return Overlap;
}

var assignmentPairs = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
var Ranges = GetRanges(assignmentPairs);
var Count1 = Ranges.Count(IsOneRangeFullyContainedByOther);
var Count2 = Ranges.Count(IsAnyOverlapInRanges);
Console.WriteLine($"Part1 count: {Count1}");
Console.WriteLine($"Part2 count: {Count2}");