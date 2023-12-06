using System.Text.RegularExpressions;
using GearRatios.Models;

namespace GearRatios.Leser;

public class EngineSchematicLeser
{
    public EngineSchematic Ermittle_Zeilen(string[] zeilen)
    {
        var elements = new List<EngineSchematicAbstract>();

        for (var index = 0; index < zeilen.Length; index++)
        {
            var zeile = zeilen[index];
            var temp = Ermittle_Zeile(index + 1, zeile);
            if (temp.Any())
            {
                elements.AddRange(temp);
            }
        }

        return new EngineSchematic(elements);
    }

    private Regex _sonderzeichen = new Regex("[^\\w\\s.]");
    private Regex _zahl = new Regex("\\d");

    private IEnumerable<EngineSchematicAbstract> Ermittle_Zeile(int zeilennummer, string zeile)
    {
        var result = new List<EngineSchematicAbstract>();

        string tempZahl = string.Empty;
        var charArray = zeile.ToCharArray();
        for (var index = 0; index < charArray.Length; index++)
        {
            var zeichen = charArray[index];

            if (zeichen == '.')
            {
                continue;
            }

            if (_sonderzeichen.IsMatch(zeichen.ToString()))
            {
                result.Add(new Symbol
                {
                    Zeile = zeilennummer,
                    StartPosition = index,
                    Zeichen = zeichen
                });
                continue;
            }

            if (_zahl.IsMatch(zeichen.ToString()))
            {
                tempZahl += zeichen;

                if ((index + 1 >= charArray.Length) || !_zahl.IsMatch(charArray[index + 1].ToString()))
                {
                    result.Add(new Number
                    {
                        Zeile = zeilennummer,
                        StartPosition = index - tempZahl.Length + 1,
                        Nummer = int.Parse(tempZahl)
                    });
                    tempZahl = string.Empty;
                }

                continue;
            }

            throw new Exception("Guard - Should not happen");
        }

        return result;
    }
}