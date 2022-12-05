using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

(char opponent, char player)[] testRound = new[] { ('A', 'Y'), ('B', 'X'), ('C', 'Z') };
var testsPass = (testRound.Sum(GetScore) == 15 && testRound.Sum(GetScorePart2) == 12);
Console.WriteLine($"tests pass: {testsPass}  (part1: {testRound.Sum(GetScore)}, part2: {testRound.Sum(GetScorePart2)})");

var input = File.ReadAllText("..\\..\\..\\..\\input\\Day2.txt", Encoding.UTF8);
var lines = input.Split("\n");
var rounds = lines.Where( l=> l.Length >2).Select(s => (s[0], s[2])).ToArray();
var score = rounds.Sum(GetScore);
Console.WriteLine($"scorePart1: {score}");

var scorePart2 = rounds.Sum(GetScorePart2);
Console.WriteLine($"scorePart2: {scorePart2}");


int GetOutcome(char o, char p) => (o, p) switch
{
	('A', 'Y') or 
	('B', 'Z') or
	('C', 'X') => 1, // win
	('A', 'X') or 
	('B', 'Y') or 
	('C', 'Z') => 0, //draw
	(_, _) => -1 //loss
};

int ShapeScore(char playerShape) => playerShape switch
{
	'X' or 'A' => 1,
	'Y' or 'B' => 2,
	'Z' or 'C' => 3,
	_ => throw new ArgumentException($"Bad palyer shape: {playerShape}", nameof(playerShape))
};

int OutcomeScore(int outcome) => outcome switch
{
	< 0 => 0,
	0 => 3,
	> 0 => 6
};

int GetScore((char opponent, char player) round)
{
	var result = ShapeScore(round.player);
	int outcome = GetOutcome(round.opponent, round.player);
	var outcomeScore = OutcomeScore(outcome);
	return result + outcomeScore;
}

int GetScorePart2((char opponent, char scriptedOutcome) round)
{
	int desiredOutcome = GetPart2DesiredOutCome(round.scriptedOutcome);

	var shapeToPlay = GetShapeforDesiredOutcome(opponent: round.opponent, desiredOutcome);

	var result = ShapeScore(shapeToPlay);
	var outcomeScore = OutcomeScore(desiredOutcome);
	return result + outcomeScore;
}

char GetShapeforDesiredOutcome(char opponent, int outcome)
{
	if (opponent is not ('A' or 'B' or 'C'))
		throw new ArgumentException($"Bad opponent shape: {opponent}");
	return (outcome) switch
	{
		< 0 => opponent switch //loss
		{
			'A' => 'C',
			'B' => 'A',
			'C' => 'B',
			_ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
		},
		0 => opponent, //draw
		> 0 => opponent switch // win
		{
			'A' => 'B',
			'B' => 'C',
			'C' => 'A',
			_ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
		}
	};
}

int GetPart2DesiredOutCome(char player) => player switch
{
	'X' => -1,
	'Y' => 0,
	'Z' => 1,
	_ => throw new ArgumentException($"Bad palyer shape: {player}", nameof(player))
};




