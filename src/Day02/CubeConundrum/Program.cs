using CubeConundrum;
using Newtonsoft.Json;

SampleReader sampleReader = new SampleReader();

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var samples = sampleReader.Read_Samples(alleZeilen);

GameErmittler gameErmittler = new GameErmittler();
var moeglicheSpiele = gameErmittler.Moegliche_Spiele(samples: samples, maxRed: 12, maxGreen: 13, maxBlue: 14);

Console.WriteLine($"Ergebnis = {(moeglicheSpiele.Sum(li => li.Id)):N0}");