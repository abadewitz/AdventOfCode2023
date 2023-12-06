using GearRatios;
using GearRatios.Ermittler;
using GearRatios.Leser;

Console.WriteLine("Day 3: Gear Ratios");

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var leser = new EngineSchematicLeser();
var engineSchematic = leser.Ermittle_Zeilen(alleZeilen);

var ermittler = new PartNumberErmittler();
var partnumbers = ermittler.GetPartnumbers(engineSchematic);
var summePartnumbers = partnumbers.Sum(x => x.Nummer);
Console.WriteLine($"Ergebnis (part one) = {summePartnumbers:N0}");


var gearErmittler = new GearErmittler();
var gears = gearErmittler.Ermittle_Gears(engineSchematic, partnumbers);
var summeGears = gears.Sum(li => li.CalculateRatio);

Console.WriteLine($"Ergebnis (part two) = {summeGears:N0}");