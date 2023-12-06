namespace GearRatios.Models;

public sealed record PartNumber(int Value);

public sealed record Gear
{
    public PartNumber PartNumber01 { get; set; }
    public PartNumber PartNumber02 { get; set; }

    public int CalculateRatio => PartNumber01.Value * PartNumber02.Value;
}