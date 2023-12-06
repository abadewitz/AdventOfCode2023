using GearRatios;

Console.WriteLine("Day 3: Gear Ratios");

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var ermittler = new PartNumberErmittler();
var summePartnumbers = ermittler.Ermittle_Summe_Partnumbers(alleZeilen);
var summeGears = ermittler.Ermittle_Summe_Gears(alleZeilen);

Console.WriteLine($"Ergebnis (part one) = {summePartnumbers:N0}");
Console.WriteLine($"Ergebnis (part two) = {summeGears:N0}");