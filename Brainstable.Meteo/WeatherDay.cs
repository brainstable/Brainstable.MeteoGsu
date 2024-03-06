using System;

namespace Brainstable.Meteo
{

    public class WeatherDay : IWeatherParameters
    {
        public DateTime DateTime { get; set; }
        public int Year => DateTime.Year;
        public int Month => DateTime.Month;
        public int Decade => DateTime.GetDecade();
        public int Day => DateTime.Day;
        public double? Temperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? Rainfall { get; set; }
        public double? SnowHight { get; set; }
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }

        public string TableName => $"{Day.ToString("D2")}.{Month.ToString("D2")}";

        public override string ToString()
        {
            return DateTime.ToShortDateString();
        }
    }
}
