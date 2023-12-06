using GearRatios;

Console.WriteLine("Day 3: Gear Ratios");

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var ermittler = new PartNumberErmittler();
var summe = ermittler.ErmittleSumme(alleZeilen);

Console.WriteLine($"Ergebnis = {summe:N0}");