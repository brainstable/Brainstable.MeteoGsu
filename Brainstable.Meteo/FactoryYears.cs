using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryYears
    {
        private Dictionary<string, List<double?>> dictT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictMinT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictMaxT = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictR = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictSH = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictDP = new Dictionary<string, List<double?>>();
        private Dictionary<string, List<double?>> dictH = new Dictionary<string, List<double?>>();

        public List<WeatherYear> Create(List<WeatherMonth> months)
        {
            foreach (WeatherMonth decade in months)
            {
                string id = $"{decade.Year}";

                if (!dictT.ContainsKey(id))
                {
                    dictT[id] = new List<double?>();
                }

                dictT[id].Add(decade.Temperature);

                if (!dictMinT.ContainsKey(id))
                {
                    dictMinT[id] = new List<double?>();
                }

                dictMinT[id].Add(decade.MinTemperature);

                if (!dictMaxT.ContainsKey(id))
                {
                    dictMaxT[id] = new List<double?>();
                }

                dictMaxT[id].Add(decade.MaxTemperature);

                if (!dictR.ContainsKey(id))
                {
                    dictR[id] = new List<double?>();
                }

                dictR[id].Add(decade.Rainfall);

                if (!dictSH.ContainsKey(id))
                {
                    dictSH[id] = new List<double?>();
                }

                dictSH[id].Add(decade.SnowHight);

                if (!dictDP.ContainsKey(id))
                {
                    dictDP[id] = new List<double?>();
                }

                dictDP[id].Add(decade.DewPoint);

                if (!dictH.ContainsKey(id))
                {
                    dictH[id] = new List<double?>();
                }

                dictH[id].Add(decade.Humidity);
            }

            List<WeatherYear> list = new List<WeatherYear>();
            foreach (var d in dictT)
            {
                WeatherYear p = new WeatherYear();
                p.Year = Convert.ToInt32(d.Key.Substring(0, 4));

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