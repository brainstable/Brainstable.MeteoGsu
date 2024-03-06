using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainstable.Meteo
{
    public class Algorithm
    {
        
        public static DateTime?[] GetDateRun(double t, int year, List<WeatherDecade> decades)
        {
            double?[] valuesByDecades = new double?[36];
            Dictionary<int, WeatherDecade> dict = new Dictionary<int, WeatherDecade>();
            foreach (var wm in decades.Where(w => w.Year == year))
            {

                dict[wm.NumberInYear] = wm;
            }

            for (int i = 0; i < valuesByDecades.Length; i++)
            {
                if (dict.ContainsKey(i + 1))
                {
                    valuesByDecades[i] = dict[i + 1].Temperature;
                }
            }


            DateTime?[] arr = new DateTime?[2];
            double?[] arr1 = new double?[21];
            double?[] arr2 = new double?[21];

            for (int i = 0; i < 21; i++)
            {
                arr1[i] = valuesByDecades[i];
            }

            int last = -1;
            bool flag = false;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i].HasValue)
                {
                    if (arr1[i].Value > t)
                    {
                        if (!flag)
                        {
                            last = i;
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }

            if (last != -1 && last < arr1.Length - 1)
            {
                if (arr1[last + 1].HasValue)
                {
                    int numDec = last;
                    int numMonth = WeatherDecade.GetNumberMonth(numDec);
                    int countDay = WeatherDecade.GetCountDay(year, numMonth, numDec);

                    double t1 = arr1[last - 1].Value;
                    double t2 = arr1[last].Value;

                    double s = (t - t1) / (t2 - t1);
                    double d = s * countDay;
                    arr[0] = WeatherDecade.GetMiddleDay(year, numMonth, numDec).AddDays(d);
                }
            }

            //======================================

            for (int i = 15; i < 36; i++)
            {
                arr2[i - 15] = valuesByDecades[i];
            }

            last = -1;
            flag = false;

            for (int i = 0; i < arr2.Length; i++)
            {
                if (arr2[i].HasValue)
                {
                    if (arr2[i].Value < t)
                    {
                        if (!flag)
                        {
                            last = i;
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }

            if (last != -1)
            {
                if (arr2[last - 1].HasValue)
                {
                    int numDec = last + 15
                        
                        ;
                    int numMonth = WeatherDecade.GetNumberMonth(numDec);
                    int countDay = WeatherDecade.GetCountDay(year, numMonth, numDec);

                    double t1 = arr2[last - 1].Value;
                    double t2 = arr2[last].Value;

                    double s = (t - t1) / (t2 - t1);
                    double d = s * countDay;
                    arr[1] = WeatherDecade.GetMiddleDay(year, numMonth, numDec).AddDays(d);
                }
            }
            return arr;
        }

        public static double[] GetAverageMonthTemperature(List<WeatherDay> days)
        {
            double[] avr = new double[12];

            for (int i = 0; i < avr.Length; i++)
            {
                avr[i] = GetAverageMonth(days, i + 1);
            }
            return avr;
        }

        public static double[] GetAverageDecadeTemperature(List<WeatherDay> days)
        {
            double[] avr = new double[36];

            for (int i = 0; i < avr.Length; i++)
            {
                avr[i] = GetAverageDecade(days, WeatherDecade.GetNumberMonth(i + 1), WeatherDecade.GetNumberDecadeInMonth(i + 1));
            }
            return avr;
        }

        private static double GetAverageMonth(List<WeatherDay> days, int month)
        {
            double value = 0;
            double sum = 0;
            int count = 0;
            foreach (var day in days.Where(d => d.Month == month))  
            {
                if (day.Temperature.HasValue)
                {
                    sum += day.Temperature.Value;
                    count++;
                }
            }
            if (count != 0)
                value = Math.Round(sum / count, 1);
            return value;
        }
        private static double GetAverageDecade(List<WeatherDay> days, int month, int decade)
        {
            double value = 0;
            double sum = 0;
            int count = 0;
            foreach (var day in days.Where(d => d.Month == month && d.Decade == decade))
            {
                if (day.Temperature.HasValue)
                {
                    sum += day.Temperature.Value;
                    count++;
                }
            }
            if (count != 0)
                value = Math.Round(sum / count, 1);
            return value;
        }
    }
}
