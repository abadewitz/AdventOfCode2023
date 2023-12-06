using FluentAssertions;
using GearRatios.Ermittler;
using GearRatios.Leser;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GearRatios.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void Ermittle_Summe_GearsTests()
    {
        string[] zeilen = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        var leser = new EngineSchematicLeser();
        var engineSchematic = leser.Ermittle_Zeilen(zeilen);

        var partNumberErmittler = new PartNumberErmittler();
        var partnumbers = partNumberErmittler.GetPartnumbers(engineSchematic);

        var ermittler = new GearErmittler();
        var gears = ermittler.Ermittle_Gears(engineSchematic, partnumbers);
        Console.WriteLine(JsonConvert.SerializeObject(gears, Formatting.Indented));

        gears.Sum(li => li.CalculateRatio).Should().Be(467835);
    }

    [Test]
    public void Ermittle_Summe_PartnumbersTests()
    {
        string[] zeilen = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        var leser = new EngineSchematicLeser();
        var engineSchematic = leser.Ermittle_Zeilen(zeilen);

        var ermittler = new PartNumberErmittler();
        var partnumbers = ermittler.GetPartnumbers(engineSchematic);

        partnumbers.Sum(li => li.Nummer).Should().Be(4361);
    }

    [Explicit]
    [Test]
    public void TempTest()
    {
        string[] zeilen = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };

        var leser = new EngineSchematicLeser();
        var engineSchematic = leser.Ermittle_Zeilen(zeilen);

        Console.WriteLine(JsonConvert.SerializeObject(engineSchematic, Formatting.Indented));
    }
}