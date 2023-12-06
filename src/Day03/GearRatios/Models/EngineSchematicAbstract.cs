namespace GearRatios.Models;

public abstract record EngineSchematicAbstract
{
    public int Id { get; set; }

    public int Zeile { get; set; }
    public int StartPosition { get; set; }
}