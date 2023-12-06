namespace GearRatios.Models;

public sealed record Number : EngineSchematicAbstract
{
    public int Nummer { get; set; }

    public int EndPosition => StartPosition + Nummer.ToString().Length - 1;
}