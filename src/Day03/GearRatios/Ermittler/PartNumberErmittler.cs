using GearRatios.Models;

namespace GearRatios.Ermittler;

public class PartNumberErmittler
{
    public List<Number> GetPartnumbers(EngineSchematic engineSchematic)
    {
        var symbole = engineSchematic.GetSymbols();

        List<Number> result = new();
        foreach (Number nummer in engineSchematic.GetNumbers())
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
}