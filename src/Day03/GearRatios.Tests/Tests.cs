using FluentAssertions;
using NUnit.Framework;

namespace GearRatios.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
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
            var summe = ermittler.ErmittleSumme(zeilen);

            summe.Should().Be(4361);
        }
    }
}
