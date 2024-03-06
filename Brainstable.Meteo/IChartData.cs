namespace Brainstable.Meteo
{
    public interface IChartData
    {
        int CountElements { get; }
        string[] XNames { get; }
        double?[] YValues { get; }
    }
}