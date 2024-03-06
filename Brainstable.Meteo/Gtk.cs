using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public static class Gtk
    {

        public static double[] CreateArrayGtkByDecades(int year, List<WeatherDay> days)
        {
            Dictionary<int, double> dict = new Dictionary<int, double>(36);
            for (int i = 0; i < 36; i++)
            {
                dict[i] = Double.NaN;
            }

            Dictionary<int, BoxSumTemperatureRainfall> temp = new Dictionary<int, BoxSumTemperatureRainfall>();
            foreach (var day in days.Where(x => x.Year == year).OrderBy(x => x.DateTime))
            {
                int numberDecade = WeatherDecade.NumberDecadeInYear(day.DateTime);
                if (!temp.ContainsKey(numberDecade))
                {
                    temp[numberDecade] = new BoxSumTemperatureRainfall();
                }

                if (day.Temperature.HasValue && day.Rainfall.HasValue)
                {
                    temp[numberDecade].AddValues(day.Temperature.Value, day.Rainfall.Value);
                }
            }

            foreach (var tr in temp)
            {
                dict[tr.Key] = tr.Value.Gtk;
            }

            double[] arr = dict.Values.ToArray();
            return arr;
        }


        public static double[] CalculateAverageGtkByDecades(int year, List<WeatherDay> days)
        {
            double[] arr = new double[36];
            for (int i = 0; i < 36; i++)
            {
                arr[i] = double.NaN;
            }
            
            List<double[]> list = new List<double[]>();
            int yearMin = days.Min(x => x.Year);

            for (int i = year - 1; i >= yearMin; i--)
            {
                list.Add(CreateArrayGtkByDecades(i, days));
            }

            for (int i = 0; i < 36; i++)
            {
                double sum = 0;
                int count = 0;
                foreach (var value in list)
                {
                    if (!double.IsNaN(value[i]))
                    {
                        sum += value[i];
                        count++;
                    }

                    if (count != 0)
                    {
                        arr[i] = Math.Round(sum / count, 1);
                    }
                }
            }
            return arr;
        }

        public static double[] CreateArrayGtkByMonth(int year, List<WeatherDay> days)
        {
            Dictionary<int, double> dict = new Dictionary<int, double>(12);
            for (int i = 1; i <= 12; i++)
            {
                dict[i] = Double.NaN;
            }

            Dictionary<int, BoxSumTemperatureRainfall> temp = new Dictionary<int, BoxSumTemperatureRainfall>();
            foreach (var day in days.Where(x => x.Year == year).OrderBy(x => x.DateTime))
            {
                int numberMonth = day.Month;
                if (!temp.ContainsKey(numberMonth))
                {
                    temp[numberMonth] = new BoxSumTemperatureRainfall();
                }

                if (day.Temperature.HasValue && day.Rainfall.HasValue)
                {
                    temp[numberMonth].AddValues(day.Temperature.Value, day.Rainfall.Value);
                }
            }

            foreach (var tr in temp)
            {
                dict[tr.Key] = tr.Value.Gtk;
            }

            double[] arr = dict.Values.ToArray();
            return arr;
        }

        public static double[] CalculateAverageGtkByMonths(int year, List<WeatherDay> days)
        {
            double[] arr = new double[12];
            for (int i = 0; i < 12; i++)
            {
                arr[i] = double.NaN;
            }

            List<double[]> list = new List<double[]>();
            int yearMin = days.Min(x => x.Year);

            for (int i = year - 1; i >= yearMin; i--)
            {
                list.Add(CreateArrayGtkByMonth(i, days));
            }

            for (int i = 0; i < 12; i++)
            {
                double sum = 0;
                int count = 0;
                foreach (var value in list)
                {
                    if (!double.IsNaN(value[i]))
                    {
                        sum += value[i];
                        count++;
                    }

                    if (count != 0)
                    {
                        arr[i] = Math.Round(sum / count, 1);
                    }
                }
            }
            return arr;
        }

        class BoxSumTemperatureRainfall
        {
            public double Temperature { get; private set; }
            public double Rainfall { get; private set; }

            public double Gtk
            {
                get
                {
                    return Math.Round(Rainfall / (0.1 * Temperature), 1);
                }
            }

            public void AddValues(double temperature, double rainfall)
            {
                if (temperature > 10)
                {
                    Temperature += temperature;
                    Rainfall += rainfall;
                }

            }
        }
    }

    
}
