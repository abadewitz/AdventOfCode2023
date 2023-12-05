using CubeConundrum;

Console.WriteLine("Day 2: Cube Conundrum");
SampleReader sampleReader = new SampleReader();

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var samples = sampleReader.Read_Samples(alleZeilen);

GameErmittler gameErmittler = new GameErmittler();
var moeglicheSpiele = gameErmittler.Moegliche_Spiele(samples: samples, maxRed: 12, maxGreen: 13, maxBlue: 14);

Console.WriteLine($"Ergebnis (part one) = {(moeglicheSpiele.Sum(li => li.Id)):N0}");
Console.WriteLine($"Ergebnis (part two) = {(samples.Sum(li => li.GetPower())):N0}");