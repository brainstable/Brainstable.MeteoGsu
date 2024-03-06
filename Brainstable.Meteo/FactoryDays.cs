using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryDays
    {
        private Dictionary<DateTime, List<double?>> dictT = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictMinT = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictMaxT = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictR = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictSH = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictDP = new Dictionary<DateTime, List<double?>>();
        private Dictionary<DateTime, List<double?>> dictH = new Dictionary<DateTime, List<double?>>();

        public List<WeatherDay> Create(List<WeatherPoint> points)
        {
            foreach (WeatherPoint point in points)
            {
                if (!dictT.ContainsKey(point.Date))
                {
                    dictT[point.Date] = new List<double?>();
                }
                dictT[point.Date].Add(point.Temperature);

                if (!dictMinT.ContainsKey(point.Date))
                {
                    dictMinT[point.Date] = new List<double?>();
                }
                dictMinT[point.Date].Add(point.MinTemperature);

                if (!dictMaxT.ContainsKey(point.Date))
                {
                    dictMaxT[point.Date] = new List<double?>();
                }
                dictMaxT[point.Date].Add(point.MaxTemperature);

                if (!dictR.ContainsKey(point.Date))
                {
                    dictR[point.Date] = new List<double?>();
                }
                dictR[point.Date].Add(point.Rainfall);

                if (!dictSH.ContainsKey(point.Date))
                {
                    dictSH[point.Date] = new List<double?>();
                }
                dictSH[point.Date].Add(point.SnowHight);

                if (!dictDP.ContainsKey(point.Date))
                {
                    dictDP[point.Date] = new List<double?>();
                }
                dictDP[point.Date].Add(point.DewPoint);

                if (!dictH.ContainsKey(point.Date))
                {
                    dictH[point.Date] = new List<double?>();
                }
                dictH[point.Date].Add(point.Humidity);

            }

            List<WeatherDay> list = new List<WeatherDay>();
            foreach (var d in dictT)
            {
                WeatherDay p = new WeatherDay();
                p.DateTime = d.Key;
                p.Temperature = d.Value.Average();

                double? min = dictMinT[d.Key].Min();
                p.MinTemperature = min.HasValue ? min : d.Value.Min();

                double? max = dictMaxT[d.Key].Max();
                p.MaxTemperature = max.HasValue ? max : d.Value.Max();

                double? rainfall = dictR[d.Key].Sum();
                p.Rainfall = rainfall;

                double? hum = dictH[d.Key].Average();
                p.Humidity = hum;

                double? sh = dictSH[d.Key].Average();
                p.SnowHight = sh;

                double? dp = dictDP[d.Key].Average();
                p.DewPoint = dp;

                list.Add(p);
            }
            return list;
        }
    }
}