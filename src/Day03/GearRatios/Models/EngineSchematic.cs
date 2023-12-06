namespace GearRatios.Models;

public class EngineSchematic
{
    public List<EngineSchematicAbstract> Elements { get; set; }

    public EngineSchematic(List<EngineSchematicAbstract> elements)
    {
        Elements = elements;
    }

    public List<Number> GetNumbers()
    {
        var nummern = Elements.Where(li => li is Number)
            .Select(li => (Number)li)
            .ToList();
        for (int i = 0; i < nummern.Count; i++)
        {
            nummern[i].Id = i + 1;
        }

        return nummern;
    }

    public List<Symbol> GetGearSymbols()
    {
        return GetSymbols().Where(li => li.Zeichen == '*').ToList();
    }

    public List<Symbol> GetSymbols()
    {
        List<Symbol> symbole = Elements.Where(li => li is Symbol)
            .Select(li => (Symbol)li)
            .ToList();
        for (int i = 0; i < symbole.Count; i++)
        {
            symbole[i].Id = i + 1;
        }

        return symbole;
    }
}