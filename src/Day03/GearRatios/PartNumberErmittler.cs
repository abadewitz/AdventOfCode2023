using System.Text.RegularExpressions;
using GearRatios.Models;

namespace GearRatios
{
    public class PartNumberErmittler
    {
        public int Ermittle_Summe_Gears(string[] zeilen)
        {
            var engineSchematic = Ermittle_Zeilen(zeilen);
            List<Symbol> symbole = engineSchematic.Where(li => li is Symbol)
                .Select(li => (Symbol)li)
                .ToList();

            var nummern = FilterPartnumbers(engineSchematic);

            var gears = symbole.Where(li => li.Zeichen == '*').ToList();
            
            List<Gear> result = new();
            foreach (var nummer in nummern)
            {
                var potenzielleGears = gears.Where(li =>
                        li.Zeile == nummer.Zeile
                        || li.Zeile == nummer.Zeile - 1
                        || li.Zeile == nummer.Zeile + 1)
                    .ToList();

                List<Number> potenzielleNummern = nummern.Where(li =>
                        li.Zeile == nummer.Zeile
                        || li.Zeile == nummer.Zeile - 1
                        || li.Zeile == nummer.Zeile + 1)
                    .ToList();

                List<Number> auswahl = new();
                foreach (var potNummer in potenzielleNummern)
                {
                    if (potenzielleGears.Any(li => li.StartPosition == potNummer.StartPosition - 1
                                                   || li.StartPosition == potNummer.EndPosition + 1
                                                   || (li.StartPosition >= potNummer.StartPosition 
                                                       && li.StartPosition <= potNummer.EndPosition)))
                    {
                        auswahl.Add(nummer);
                    }
                }

                if (auswahl.Count == 2)
                {
                    result.Add(new Gear
                    {
                        PartNumber01 = new PartNumber(auswahl.First().Nummer),
                        PartNumber02 = new PartNumber(auswahl.Last().Nummer)
                    });
                }
                else if (auswahl.Count > 2)
                {
                    throw new Exception("Guard");
                }
            }

            return result.Sum(li => li.CalculateRatio);
        }

        public int Ermittle_Summe_Partnumbers(string[] zeilen)
        {
            IEnumerable<EngineSchematic> engineSchematic = Ermittle_Zeilen(zeilen);

            var numbers = FilterPartnumbers(engineSchematic);
            List<PartNumber> partNumbers = numbers.Select(li => new PartNumber(li.Nummer)).ToList();

            return partNumbers.Sum(li => li.Value);
        }

        private IEnumerable<Number> FilterPartnumbers(IEnumerable<EngineSchematic> engineSchematic)
        {
            List<Symbol> symbole = engineSchematic.Where(li => li is Symbol)
                .Select(li => (Symbol)li)
                .ToList();
            var nummern = engineSchematic.Where(li => li is Number)
                .Select(li => (Number)li)
                .ToList();

            List<Number> result = new();
            foreach (Number nummer in nummern)
            {
                var potenzielleSymbole = symbole.Where(li =>
                        li.Zeile == nummer.Zeile
                        || li.Zeile == nummer.Zeile - 1
                        || li.Zeile == nummer.Zeile + 1)
                    .ToList();

                if (potenzielleSymbole.Any(li => li.StartPosition == nummer.StartPosition - 1
                                                 || li.StartPosition == nummer.EndPosition + 1
                                                 || (li.StartPosition >= nummer.StartPosition && li.StartPosition <= nummer.EndPosition)))
                {
                    result.Add(nummer);
                }
            }

            return result;
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

                    if ((index+1 >= charArray.Length) || !_zahl.IsMatch(charArray[index + 1].ToString()))
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
}
