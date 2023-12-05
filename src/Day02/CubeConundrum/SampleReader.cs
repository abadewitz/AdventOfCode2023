using System.Runtime.CompilerServices;
using CubeConundrum.Models;

[assembly: InternalsVisibleTo("CubeConundrum.Tests")]
namespace CubeConundrum;

public class SampleReader
{
    public IEnumerable<GameSample> Read_Samples(string[] zeilen)
    {
        return zeilen.Select(Read_Sample);
    }

    internal GameSample Read_Sample(string zeile)
    {
        //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var gameValueSplitted = zeile.Split(':');
        var id = int.Parse(gameValueSplitted.First().Replace("Game ", string.Empty));
        var result = new GameSample(id);

        var setsSplitted = gameValueSplitted.Last().Split(';', StringSplitOptions.RemoveEmptyEntries);
        foreach (var set in setsSplitted)
        {
            //3 blue, 4 red
            var valuesSplitted = set.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var value in valuesSplitted)
            {
                var valueSplitted = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int wert = int.Parse(valueSplitted.First().Trim());

                if (value.EndsWith("blue"))
                {
                    if (wert > result.HighestAmountBlue)
                    {
                        result.HighestAmountBlue = wert;
                    }
                }
                else if (value.EndsWith("red"))
                {
                    if (wert > result.HighestAmountRed)
                    {
                        result.HighestAmountRed = wert;
                    }
                }
                else if (value.EndsWith("green"))
                {
                    if (wert > result.HighestAmountGreen)
                    {
                        result.HighestAmountGreen = wert;
                    }
                }
            }
        }

        return result;
    }
}