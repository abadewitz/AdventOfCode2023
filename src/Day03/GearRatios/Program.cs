using GearRatios;
using Newtonsoft.Json;

Console.WriteLine("Day 3: Gear Ratios");


var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var ermittler = new PartNumberErmittler();
var summe = ermittler.ErmittleSumme(alleZeilen);

var temp = ermittler.Ermittle_Zeilen(alleZeilen);
Console.WriteLine(JsonConvert.SerializeObject(temp, Formatting.Indented));

Console.WriteLine($"Ergebnis = {summe:N0}");