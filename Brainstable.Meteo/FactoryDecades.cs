using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryDecades
    {
        private Dictionary<string, List<double?>> dictT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictMinT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictMaxT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictR = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictSH = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictDP = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictH = new Dictionary<string, List<double?>>();

        public List<WeatherDecade> Create(List<WeatherDay> days)
        {
            foreach (WeatherDay day in days)
            {
                string id = $"{day.Year}{day.Month.ToString("D2")}{day.Decade}";

                if (!dictT.ContainsKey(id))
                {
                    dictT[id] = new List<double?>();
                }

                dictT[id].Add(day.Temperature);

                if (!dictMinT.ContainsKey(id))
                {
                    dictMinT[id] = new List<double?>();
                }

                dictMinT[id].Add(day.MinTemperature);

                if (!dictMaxT.ContainsKey(id))
                {
                    dictMaxT[id] = new List<double?>();
                }

                dictMaxT[id].Add(day.MaxTemperature);

                if (!dictR.ContainsKey(id))
                {
                    dictR[id] = new List<double?>();
                }

                dictR[id].Add(day.Rainfall);

                if (!dictSH.ContainsKey(id))
                {
                    dictSH[id] = new List<double?>();
                }

                dictSH[id].Add(day.SnowHight);

                if (!dictDP.ContainsKey(id))
                {
                    dictDP[id] = new List<double?>();
                }

                dictDP[id].Add(day.DewPoint);

                if (!dictH.ContainsKey(id))
                {
                    dictH[id] = new List<double?>();
                }

                dictH[id].Add(day.Humidity);
            }

            List<WeatherDecade> list = new List<WeatherDecade>();
            foreach (var d in dictT)
            {
                WeatherDecade p = new WeatherDecade();
                p.Year = Convert.ToInt32(d.Key.Substring(0, 4));
                p.Month = Convert.ToInt32(d.Key.Substring(4, 2));
                p.Decade = Convert.ToInt32(d.Key.Substring(6));

                p.Temperature = d.Value.Average();
                p.MinTemperature = dictMinT[d.Key].Min();
                p.MaxTemperature = dictMaxT[d.Key].Max();
                p.Rainfall = dictR[d.Key].Sum();
                p.Humidity = dictH[d.Key].Average();
                p.SnowHight = dictSH[d.Key].Average();
                p.DewPoint = dictDP[d.Key].Average();

                list.Add(p);
            }

            return list;
        }
    }
}