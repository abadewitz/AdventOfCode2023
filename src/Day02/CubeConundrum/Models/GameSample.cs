namespace CubeConundrum.Models;

public sealed record GameSample
{
    public int Id { get; set; }

    public int HighestAmountRed { get; set; } = 0;
    public int HighestAmountGreen { get; set; } = 0;
    public int HighestAmountBlue { get; set; } = 0;

    public GameSample(int id)
    {
        Id = id;
    }

    public double GetPower()
    {
        return HighestAmountBlue * HighestAmountGreen * HighestAmountRed;
    }
}