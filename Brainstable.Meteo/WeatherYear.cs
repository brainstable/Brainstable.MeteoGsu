﻿namespace Brainstable.Meteo
{
    public class WeatherYear : IWeatherParameters
    {
        public int Year { get; set; }
        public double? Temperature { get; set; }
        public double? MinTemperature { get; set; }
        public double? MaxTemperature { get; set; }
        public double? Rainfall { get; set; }
        public double? SnowHight { get; set; }
        public double? Humidity { get; set; }
        public double? DewPoint { get; set; }

        public string TableName => Year.ToString();

        public override string ToString()
        {
            return $"{Year}";
        }
    }
}