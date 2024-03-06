using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Brainstable.RP5;

namespace Brainstable.Meteo
{
    public class WeatherPoint : IWeatherParameters
    {
        public string Id { get; set; }
        public DateTime Date => new DateTime(Year, Month, Day);
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
        public double? DewPoint { get; set; }
        public double? Humidity { get; set; }
        public override string ToString()
        {
            return DateTime.ToString();
        }
    }

}