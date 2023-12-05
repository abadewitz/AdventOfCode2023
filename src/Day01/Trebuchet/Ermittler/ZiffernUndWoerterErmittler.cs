using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Trebuchet.Tests")]
namespace Trebuchet.Ermittler;

public class ZiffernUndWoerterErmittler : IZahlErmittler
{
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

    private int? GetIndexOf(string input, string suchtext)
    {
        int result = input.IndexOf(suchtext, StringComparison.OrdinalIgnoreCase);
        if (result == -1)
        {
            return null;
        }

        return result;
    }


    private int? GetLastIndexOf(string input, string suchtext)
    {
        int result = input.LastIndexOf(suchtext, StringComparison.OrdinalIgnoreCase);
        if (result == -1)
        {
            return null;
        }

        return result;
    }

    internal int ErmittleZweiZiffernZahl(string zeile)
    {
        int ersteZiffer = -1;
        {
            int ersteZifferPos = -1;
            foreach (var elem in _woerter)
            {
                int? ziffer = GetIndexOf(zeile, elem.Key.ToString());
                int? wortZahl = GetIndexOf(zeile, elem.Value);
                if (ziffer is null && wortZahl is null)
                {
                    continue;
                }

                var temp = (ziffer ?? int.MaxValue) < (wortZahl ?? int.MaxValue) ? ziffer : wortZahl;
                if (temp < ersteZifferPos || ersteZifferPos == -1)
                {
                    ersteZifferPos = temp.Value;
                    ersteZiffer = elem.Key;
                }
            }
        }

        int letzteZiffer = 0;
        {
            int letzteZifferPos = -1;
            foreach (var elem in _woerter)
            {
                int? ziffer = GetLastIndexOf(zeile, elem.Key.ToString());
                int? wortZahl = GetLastIndexOf(zeile, elem.Value);
                if (ziffer is null && wortZahl is null)
                {
                    continue;
                }

                var temp = (ziffer ?? int.MinValue) > (wortZahl ?? int.MinValue) ? ziffer : wortZahl;
                if (temp > letzteZifferPos || letzteZifferPos == -1)
                {
                    letzteZifferPos = temp.Value;
                    letzteZiffer = elem.Key;
                }
            }
        }

        var parsed = int.Parse($"{ersteZiffer}{letzteZiffer}");
        return parsed;
    }
}