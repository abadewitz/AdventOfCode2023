namespace GearRatios.Models;

public abstract record EngineSchematic
{
    public int Zeile { get; set; }
    public int StartPosition { get; set; }
}