namespace Brainstable.Meteo
{
    public interface IWeatherParameters
    {
        double? Temperature { get; set; }
        double? MinTemperature { get; set; }
        double? MaxTemperature { get; set; }
        double? Rainfall { get; set; }
        double? SnowHight { get; set; }
        double? Humidity { get; set; }
        double? DewPoint { get; set; }
    }
}