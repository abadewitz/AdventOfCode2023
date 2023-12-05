using CubeConundrum.Models;

namespace CubeConundrum;

public class GameErmittler
{
    public List<GameSample> Moegliche_Spiele(IEnumerable<GameSample> samples, int maxRed, int maxGreen, int maxBlue)
    {
        return samples.Where(li => li.HighestAmountRed <= maxRed && li.HighestAmountGreen <= maxGreen && li.HighestAmountBlue <= maxBlue)
            .ToList();
    }
}