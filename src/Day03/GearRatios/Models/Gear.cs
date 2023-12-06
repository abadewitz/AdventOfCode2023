namespace GearRatios.Models;

public sealed record Gear
{
    public Symbol GearSymbol { get; set; }
    public Number PartNumber01 { get; set; }
    public Number PartNumber02 { get; set; }

    public int CalculateRatio => PartNumber01.Nummer * PartNumber02.Nummer;
}