using System.Text.RegularExpressions;
using OneOf;

namespace GearRatios
{
    public sealed record Partnumber : EngineSchematic
    {
        public int Nummer { get; set; }

        public int EndPosition => StartPosition + Nummer.ToString().Length;
    }

    public sealed record Symbol : EngineSchematic
    {
        public char Zeichen { get; set; }
    }

    public abstract record EngineSchematic
    {
        public int Zeile { get; set; }
        public int StartPosition { get; set; }
    }

    public class PartNumberErmittler
    {
        //"467..114..",
        //"...*......",
        //"..35..633.",
        //"......#...",
        //"617*......",
        //".....+.58.",
        //"..592.....",
        //"......755.",
        //"...$.*....",
        //".664.598.."

        public int ErmittleSumme(string[] zeilen)
        {
            int summe = 0;

            var temp = Ermittle_Zeilen(zeilen);

            return summe;
        }

        public IEnumerable<EngineSchematic> Ermittle_Zeilen(string[] zeilen)
        {
            var result = new List<EngineSchematic>();

            for (var index = 0; index < zeilen.Length; index++)
            {
                var zeile = zeilen[index];
                var temp = Ermittle_Zeile(index+1, zeile);
                if (temp.Any())
                {
                    result.AddRange(temp);
                }
            }

            return result;
        }

        private Regex _sonderzeichen = new Regex("[^\\w\\s.]");
        private Regex _zahl = new Regex("\\d");

        private IEnumerable<EngineSchematic> Ermittle_Zeile(int zeilennummer, string zeile)
        {
            var result = new List<EngineSchematic>();

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
            }

            return result;
        }
    }
}
