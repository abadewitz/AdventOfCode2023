using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CubeConundrum.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void SampleReaderTest()
    {
        var sut = new SampleReader();
        var result = sut.Read_Sample("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");
        result.Id.Should().Be(1);
        result.HighestAmountBlue.Should().Be(9);
        result.HighestAmountGreen.Should().Be(4);
        result.HighestAmountRed.Should().Be(5);
    }

    [Test]
    public void GameErmittlerPartOneTest()
    {
        string[] zeilen = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };

        var reader = new SampleReader();
        var samples = reader.Read_Samples(zeilen);

        var sut = new GameErmittler();

        var result = sut.Moegliche_Spiele(samples: samples, maxRed: 12, maxGreen: 13, maxBlue: 14);

        result.Sum(li => li.Id).Should().Be(8);
    }


    [Test]
    public void GameErmittlerPartTwoTest()
    {
        string[] zeilen = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };

        var reader = new SampleReader();
        var samples = reader.Read_Samples(zeilen);

        TestContext.WriteLine(JsonConvert.SerializeObject(samples.Select(li => $"{li.Id} {li.GetPower()}"), Formatting.Indented));
        //TestContext.WriteLine(JsonConvert.SerializeObject(samples, Formatting.Indented));

        samples.Sum(li => li.GetPower()).Should().Be(2286);
    }
}