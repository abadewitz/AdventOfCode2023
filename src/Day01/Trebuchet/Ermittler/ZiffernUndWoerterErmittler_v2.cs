using System.Text.RegularExpressions;

namespace Trebuchet.Ermittler;

public class ZiffernUndWoerterErmittler_v2 : IZahlErmittler
{
    private Regex _zahlenRegex = new Regex("(\\d)|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
    private Dictionary<int, string> _woerter = new Dictionary<int, string>()
    {
        {1, "one"},
        {2, "two"},
        {3, "three"},
        {4, "four"},
        {5, "five"},
        {6, "six"},
        {7, "seven"},
        {8, "eight"},
        {9, "nine"}
    };

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

        string? ersteZiffer = matches.Select(li => li.Value).FirstOrDefault();
        if (ersteZiffer.Length > 1)
        {
            ersteZiffer = _woerter.FirstOrDefault(li => li.Value.Equals(ersteZiffer, StringComparison.OrdinalIgnoreCase)).Key.ToString();
        }

        string? letzteZiffer = matches.Select(li => li.Value).LastOrDefault();
        if (letzteZiffer.Length > 1)
        {
            letzteZiffer = _woerter.FirstOrDefault(li => li.Value.Equals(letzteZiffer, StringComparison.OrdinalIgnoreCase)).Key.ToString();
        }


        var parsed = int.Parse($"{ersteZiffer}{letzteZiffer}");
        return parsed;
    }
}