using System.Text.RegularExpressions;

namespace Trebuchet.Ermittler;

public class ZifferOnlyErmittler : IZahlErmittler
{
    private Regex _zahlenRegex = new Regex("\\d");

    public int ErmittleSumme(string[] zeilen)
    {
        int summe = 0;
        foreach (var zeile in zeilen)
        {
            var zahl = ErmittleZweiZiffernZahl(zeile);
            summe += zahl;
        }

        return summe;
    }

    private int ErmittleZweiZiffernZahl(string zeile)
    {
        MatchCollection matches = _zahlenRegex.Matches(zeile);
        var ersteZiffer = matches.Select(li => li.Value).FirstOrDefault();
        var letzteZiffer = matches.Select(li => li.Value).LastOrDefault();
        var parsed = int.Parse($"{ersteZiffer}{letzteZiffer}");
        return parsed;
    }
}