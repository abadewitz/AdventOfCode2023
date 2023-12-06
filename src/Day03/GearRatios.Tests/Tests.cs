using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GearRatios.Tests
{
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

            var ermittler = new PartNumberErmittler();
            var summe = ermittler.Ermittle_Summe_Gears(zeilen);

            summe.Should().Be(467835);
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

            var ermittler = new PartNumberErmittler();
            var summe = ermittler.Ermittle_Summe_Partnumbers(zeilen);

            summe.Should().Be(4361);
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

            var ermittler = new PartNumberErmittler();
            var temp = ermittler.Ermittle_Zeilen(zeilen);

            Console.WriteLine(JsonConvert.SerializeObject(temp, Formatting.Indented));
        }
    }
}
