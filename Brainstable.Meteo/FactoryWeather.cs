using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryWeather
    {
        private const string DIR_RP5 = "data\\rp5";

        public List<WeatherDay> WeatherDays { get; set; }
        public List<WeatherDecade> WeatherDecades { get; set; }
        public List<WeatherMonth> WeatherMonths { get; set; }
        public List<WeatherYear> WeatherYears { get; set; }

        public void Create(string stationId)
        {
            string fileName = GetFileNameByStationId(stationId);
            
            FactoryWeatherPoint factoryWeatherPoint = new FactoryWeatherPoint();
            List<WeatherPoint> points = factoryWeatherPoint.Create(fileName);

            FactoryDays factoryDays = new FactoryDays();
            WeatherDays = factoryDays.Create(points);

            FactoryDecades factoryDecades = new FactoryDecades();
            WeatherDecades = factoryDecades.Create(WeatherDays);

            FactoryMonths factoryMonths = new FactoryMonths();
            WeatherMonths = factoryMonths.Create(WeatherDecades);

            FactoryYears factoryYears = new FactoryYears();
            WeatherYears = factoryYears.Create(WeatherMonths);

            points = null;

            //FileMeteo filemeteo = new FileMeteo();
            //WeatherDecades = filemeteo.GetWeatherDecades(stationId);
            //WeatherMonths = filemeteo.GetWeatherMonths(stationId);
            //WeatherDays = filemeteo.GetWeatherDays(stationId);
            //WeatherYears = filemeteo.GetWeatherYears(stationId);
        }


        

        private string GetFileNameByStationId(string stationId)
        {
            string[] files = Directory.GetFiles(DIR_RP5);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains(stationId))
                    return files[i];
            }
            return String.Empty;
        }

        public List<double?[]> GetValuesMonthsByYear(int year)
        {
            int count = 12;
            double?[] t = new double?[count];
            double?[] n = new double?[count];
            double?[] x = new double?[count];
            double?[] r = new double?[count];
            double?[] s = new double?[count];
            double?[] h = new double?[count];
            double?[] d = new double?[count];

            Dictionary<int, WeatherMonth> dict = new Dictionary<int, WeatherMonth>();

            foreach (var wm in WeatherMonths.Where(w => w.Year == year))
            {
                dict[wm.Month] = wm;
            }

            for (int i = 0; i < count; i++)
            {
                if (dict.ContainsKey(i + 1))
                {
                    t[i] = dict[i + 1].Temperature;
                    n[i] = dict[i + 1].MinTemperature;
                    x[i] = dict[i + 1].MaxTemperature;
                    r[i] = dict[i + 1].Rainfall;
                    s[i] = dict[i + 1].SnowHight;
                    h[i] = dict[i + 1].Humidity;
                    d[i] = dict[i + 1].DewPoint;
                }
            }

            double?[] at = new double?[count];
            double?[] an = new double?[count];
            double?[] ax = new double?[count];
            double?[] ar = new double?[count];
            double?[] @as = new double?[count];
            double?[] ah = new double?[count];
            double?[] ad = new double?[count];

            Dictionary<int, List<WeatherMonth>> dict2 = new Dictionary<int, List<WeatherMonth>>();
            foreach (var wm in WeatherMonths.Where(w => w.Year < year))
            {
                if (!dict2.ContainsKey(wm.Month))
                {
                    dict2[wm.Month] = new List<WeatherMonth>();
                }
                dict2[wm.Month].Add(wm);
            }

            for (int i = 0; i < count; i++)
            {
                if (dict2.ContainsKey(i + 1))
                {
                    double? v = dict2[i + 1].Average(w => w.Temperature);
                    if (v.HasValue) 
                        at[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.MinTemperature);
                    if (v.HasValue)
                        an[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.MaxTemperature);
                    if (v.HasValue)
                        ax[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.Rainfall);
                    if (v.HasValue)
                        ar[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.SnowHight);
                    if (v.HasValue)
                        @as[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.Humidity);
                    if (v.HasValue)
                        ah[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.DewPoint);
                    if (v.HasValue)
                        ad[i] = Math.Round(v.Value, 1);
                }
            }

            List<double?[]> list = new List<double?[]>();
            list.AddRange(new[]
            {
                t, n, x, r, s, h, d ,
                at, an, ax, ar, @as, ah, ad
            });

            return list;
        }

        public List<double?[]> GetValuesDecadesByYear(int year)
        {
            int count = 36;
            double?[] t = new double?[count];
            double?[] n = new double?[count];
            double?[] x = new double?[count];
            double?[] r = new double?[count];
            double?[] s = new double?[count];
            double?[] h = new double?[count];
            double?[] d = new double?[count];
            Dictionary<int, WeatherDecade> dict = new Dictionary<int, WeatherDecade>();

            foreach (var wm in WeatherDecades.Where(w => w.Year == year))
            {
                dict[wm.NumberInYear] = wm;
            }

            for (int i = 0; i < count; i++)
            {
                if (dict.ContainsKey(i + 1))
                {
                    t[i] = dict[i + 1].Temperature;
                    n[i] = dict[i + 1].MinTemperature;
                    x[i] = dict[i + 1].MaxTemperature;
                    r[i] = dict[i + 1].Rainfall;
                    s[i] = dict[i + 1].SnowHight;
                    h[i] = dict[i + 1].Humidity;
                    d[i] = dict[i + 1].DewPoint;
                }
            }

            double?[] at = new double?[count];
            double?[] an = new double?[count];
            double?[] ax = new double?[count];
            double?[] ar = new double?[count];
            double?[] @as = new double?[count];
            double?[] ah = new double?[count];
            double?[] ad = new double?[count];

            Dictionary<int, List<WeatherDecade>> dict2 = new Dictionary<int, List<WeatherDecade>>();
            foreach (var wm in WeatherDecades.Where(w => w.Year < year))
            {
                if (!dict2.ContainsKey(wm.NumberInYear))
                {
                    dict2[wm.NumberInYear] = new List<WeatherDecade>();
                }
                dict2[wm.NumberInYear].Add(wm);
            }

            for (int i = 0; i < count; i++)
            {
                if (dict2.ContainsKey(i + 1))
                {
                    double? v = dict2[i + 1].Average(w => w.Temperature);
                    if (v.HasValue)
                        at[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.MinTemperature);
                    if (v.HasValue)
                        an[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.MaxTemperature);
                    if (v.HasValue)
                        ax[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.Rainfall);
                    if (v.HasValue)
                        ar[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.SnowHight);
                    if (v.HasValue)
                        @as[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.Humidity);
                    if (v.HasValue)
                        ah[i] = Math.Round(v.Value, 1);

                    v = dict2[i + 1].Average(w => w.DewPoint);
                    if (v.HasValue)
                        ad[i] = Math.Round(v.Value, 1);
                }
            }

            List<double?[]> list = new List<double?[]>();
            list.AddRange(new[]
            {
                t, n, x, r, s, h, d ,
                at, an, ax, ar, @as, ah, ad
            });
            return list;
        }
    }
}
