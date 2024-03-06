using System;
using System.Collections.Generic;

namespace Brainstable.Meteo
{
    public class WeatherModel
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public double[] Temperatures { get; set; }
        public double[] MinTemperatures { get; set; }
        public double[] MaxTemperatures { get; set; }
        public double[] Rainfalls { get; set; }
        public double[] SnowHights { get; set; }

        public WeatherModel(int year, int month)
        {
            Year = year;
            Month = month;
            Temperatures = new double[DateTime.DaysInMonth(Year, Month)];
            MinTemperatures = new double[DateTime.DaysInMonth(Year, Month)];
            MaxTemperatures = new double[DateTime.DaysInMonth(Year, Month)];
            Rainfalls = new double[DateTime.DaysInMonth(Year, Month)];
            SnowHights = new double[DateTime.DaysInMonth(Year, Month)];
        }


    }

    public class FactoryWeatherModel
    {
        public WeatherModel CretateWeatherModel(DateTime dt)
        {
            return new WeatherModel(dt.Year, dt.Month);
        }

        
    }
}