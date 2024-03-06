using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Brainstable.RP5;

namespace Brainstable.Meteo
{
    public class FactoryWeatherPoint    
    {
        const double LIMIT_RAIN = 150;

        public List<WeatherPoint> Create(string pathFile)
        {
            List<WeatherPoint> list = new List<WeatherPoint>();
            string[] lines = File.ReadAllLines(pathFile);
            //RP5.ReaderRP5Csv reader = new ReaderRP5Csv();
            //reader.Read(pathFile);
            //string[] lines = reader.StringArrayData;

            NameValueCollection collTemp = new NameValueCollection();
            NameValueCollection collTempMin = new NameValueCollection();
            NameValueCollection collTempMax = new NameValueCollection();
            NameValueCollection collRain = new NameValueCollection();
            NameValueCollection collSnow = new NameValueCollection();
            NameValueCollection collDP = new NameValueCollection();
            NameValueCollection collRH = new NameValueCollection();

            for (int i = 7; i < lines.Length; i++)
            {
                string id = lines[i].Substring(1, 10).Trim();
                string[] arrw = lines[i].Split('\"');

                List<string> arr = new List<string>();

                for (int j = 0; j < arrw.Length; j++)
                {
                    if (j != 0)
                    {
                        if (arrw[j].Trim() != ";")
                            arr.Add(arrw[j]);
                    }
                }

                double rh;
                if (!Double.TryParse(arr[5].Replace('\"', ' ').Replace('.', ',').Trim(), out rh))
                {
                    rh = 0.0;
                }

                double dp;
                if (!Double.TryParse(arr[22].Replace('\"', ' ').Replace('.', ',').Trim(), out dp))
                {
                    dp = 0.0;
                }

                double rain;
                if (!Double.TryParse(arr[23].Replace('\"', ' ').Replace('.', ',').Trim(), out rain))
                {
                    rain = 0.0;
                }
                if (rain > LIMIT_RAIN)
                    rain = Math.Round(rain / 100, 1);


                double snow;
                if (!Double.TryParse(arr[28].Replace('\"', ' ').Replace('.', ',').Trim(), out snow))
                {
                    snow = 0.0;
                }

                collTemp.Add(id, arr[1].Replace('\"', ' ').Replace('.', ',').Trim());
                collRH.Add(id, rh.ToString());
                collTempMin.Add(id, arr[14].Replace('\"', ' ').Replace('.', ',').Trim());
                collTempMax.Add(id, arr[15].Replace('\"', ' ').Replace('.', ',').Trim());
                collRain.Add(id, rain.ToString());
                collSnow.Add(id, snow.ToString());
                collDP.Add(id, dp.ToString());

                if (arr.Count != 29)
                    Console.WriteLine(arr.Count);
            }
            
            int n = 0;
            foreach (string key in collTemp.Keys)
            {
                string[] arr = collTemp.GetValues(key);
                WeatherPoint point = new WeatherPoint();
                point.Id = key;
                point.DateTime = Convert.ToDateTime(key);
                double sum = 0;
                double max = -100;
                double min = 100;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arr[i]))
                    {
                        double d = Convert.ToDouble(arr[i]);
                        sum += d;
                        if (d > max)
                            max = d;
                        if (d < min)
                            min = d;
                    }
                }
                point.Temperature = Math.Round(sum / arr.Length, 1);
                point.MaxTemperature = max;
                point.MinTemperature = min;

                arr = collRH.GetValues(key);
                min = 100.0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arr[i]))
                    {
                        double d = Convert.ToDouble(arr[i]);
                        if (d <= min)
                            min = d;
                    }
                }
                point.Humidity = min;

                arr = collRain.GetValues(key);
                sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    double d = Convert.ToDouble(arr[i]);
                    sum += d;
                }
                point.Rainfall = sum;

                arr = collSnow.GetValues(key);
                sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    double d = Convert.ToDouble(arr[i]);
                    sum += d;
                }
                point.SnowHight = sum;

                arr = collDP.GetValues(key);
                sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    double d = Convert.ToDouble(arr[i]);
                    sum += d;
                }
                point.DewPoint = Math.Round(sum / arr.Length, 1);

                list.Add(point);
            }
            ValidateHighSnow(list);
            return list;
        }

        private void ValidateHighSnow(List<WeatherPoint> list)
        {
            for (int i = 1; i < list.Count - 1; i++)
            {
                if (list[i].SnowHight.Value == 0)
                {
                    if (list[i - 1].SnowHight.Value > 0 && list[i + 1].SnowHight.Value > 0)
                    {
                        list[i].SnowHight = list[i - 1].SnowHight;
                    }
                }
            }
        }
    }
}