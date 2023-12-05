using FluentAssertions;
using NUnit.Framework;
using Trebuchet.Ermittler;

namespace Trebuchet.Tests;

[TestFixture]
public class ZahlErmittlerTests
{
    [Test]
    public void ErmittleZweiZiffernZahlTest()
    {
        ZiffernUndWoerterErmittler sut = new ZiffernUndWoerterErmittler();
        string[] zeilen = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        var zeile = zeilen.First();
        TestContext.WriteLine($"Test {zeile}");
        var summe = sut.ErmittleZweiZiffernZahl(zeile);
        TestContext.WriteLine($"Ergebnis {summe}");

    }

    [Test]
    public void ErmittleSummeZiffernUndWoerterErmittler_v2Test()
    {
        IZahlErmittler sut = new ZiffernUndWoerterErmittler_v2();
        string[] zeilen = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        var summe = sut.ErmittleSumme(zeilen);
        summe.Should().Be(281);
    }

    [Test]
    public void ErmittleSummeZiffernAndWoerterTest()
    {
        IZahlErmittler sut = new ZiffernUndWoerterErmittler();
        string[] zeilen = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };

        var summe = sut.ErmittleSumme(zeilen);
        summe.Should().Be(281);
    }

    [Test]
    public void ErmittleSummeZifferOnlyTest()
    {
        IZahlErmittler sut = new ZifferOnlyErmittler();

        string[] zeilen = new[]
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };

        var summe = sut.ErmittleSumme(zeilen);
        summe.Should().Be(142);
    }
}