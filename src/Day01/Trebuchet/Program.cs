using Trebuchet.Ermittler;

Console.WriteLine("Day 1: Trebuchet?!");
//IZahlErmittler zahlErmittler = new ZifferOnlyErmittler();
IZahlErmittler zahlErmittler = new ZiffernUndWoerterErmittler_v2();

var pfadZuInput = Path.Combine(Directory.GetCurrentDirectory(), "PuzzleInput.txt");
string[] alleZeilen = File.ReadAllLines(pfadZuInput);

var summe = zahlErmittler.ErmittleSumme(alleZeilen);

Console.WriteLine($"Ergebnis = {summe:N0}");