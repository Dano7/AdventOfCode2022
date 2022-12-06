
using System.Text;

var TestInput = "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000";
var testArray = TestInput.Split("\n\n").Select(s => s.Split("\n", StringSplitOptions.RemoveEmptyEntries).Sum(Convert.ToInt32)).ToArray();
Console.WriteLine($"test max: {testArray.Max()}, top3: {testArray.OrderByDescending(i => i).Take(3).Sum()}");

var input = File.ReadAllText("..\\..\\..\\..\\input\\Day1.txt",Encoding.UTF8);
var elfsums = input.Split("\n\n").Select(s => s.Split("\n", StringSplitOptions.RemoveEmptyEntries).Sum(Convert.ToInt32)).ToArray();
var max = elfsums.Max();
var top3 = elfsums.OrderByDescending(i => i).Take(3).Sum();

Console.WriteLine($"max: {max}");
Console.WriteLine($"top3: {top3}");
