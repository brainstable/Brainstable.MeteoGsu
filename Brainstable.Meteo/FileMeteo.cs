using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstable.Meteo
{
    public class FileMeteo
    {
        private const string DIR_REPOS = "convrepos";
        private const string DIR_RP5 = "data\\rp5";

        public List<MeteoStation> GetMeteoStations()
        {
            List<MeteoStation> list = new List<MeteoStation>();
            string[] files = Directory.GetFiles(DIR_RP5);
            for (int i = 0; i < files.Length; i++)
            {
                RP5.MetaDataRP5 meta = RP5.FastReaderRP5.ReadMetaDataFromCsv(files[i]);
                MeteoStation st = new MeteoStation();
                st.Id = meta.Identificator;
                st.Name = meta.Station;
                st.Country = meta.Country;
                list.Add(st);
            }
            return list;
        }



        public List<WeatherYear> GetWeatherYears(string stationId)
        {
            List<WeatherYear> list = new List<WeatherYear>();
            string fileName = Path.Combine(DIR_REPOS, stationId + ".year");
            string[] arr = File.ReadAllLines(fileName);
            for (int i = 0; i < arr.Length; i++)
            {
                WeatherYear wm = new WeatherYear();
                wm.Year = Convert.ToInt32(arr[i].Substring(6, 4));

                string str = arr[i].Substring(11, 8);
                double d;
                if (Double.TryParse(str, out d))
                {
                    wm.Temperature = d;
                }
                str = arr[i].Substring(20, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MinTemperature = d;
                }
                str = arr[i].Substring(29, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MaxTemperature = d;
                }
                str = arr[i].Substring(38, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Rainfall = d;
                }
                str = arr[i].Substring(47, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.SnowHight = d;
                }
                str = arr[i].Substring(56, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Humidity = d;
                }
                str = arr[i].Substring(65, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.DewPoint = d;
                }
                list.Add(wm);
            }


            return list;
        }

        public List<WeatherMonth> GetWeatherMonths(string stationId)
        {
            List<WeatherMonth> list = new List<WeatherMonth>();
            string fileName = Path.Combine(DIR_REPOS, stationId + ".month");
            string[] arr = File.ReadAllLines(fileName);
            for (int i = 0; i < arr.Length; i++)
            {
                WeatherMonth wm = new WeatherMonth();
                wm.Year = Convert.ToInt32(arr[i].Substring(6, 4));
                wm.Month = Convert.ToInt32(arr[i].Substring(11, 2));
                string str = arr[i].Substring(14, 8);
                double d;
                if (Double.TryParse(str, out d))
                {
                    wm.Temperature = d;
                }
                str = arr[i].Substring(23, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MinTemperature = d;
                }
                str = arr[i].Substring(32, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MaxTemperature = d;
                }
                str = arr[i].Substring(41, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Rainfall = d;
                }
                str = arr[i].Substring(50, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.SnowHight = d;
                }
                str = arr[i].Substring(59, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Humidity = d;
                }
                str = arr[i].Substring(68, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.DewPoint = d;
                }
                list.Add(wm);
            }


            return list;
        }

        public List<WeatherDecade> GetWeatherDecades(string stationId)
        {
            List<WeatherDecade> list = new List<WeatherDecade>();
            string fileName = Path.Combine(DIR_REPOS, stationId + ".decade");
            string[] arr = File.ReadAllLines(fileName);
            for (int i = 0; i < arr.Length; i++)
            {
                WeatherDecade wm = new WeatherDecade();
                wm.Year = Convert.ToInt32(arr[i].Substring(6, 4));
                wm.Month = Convert.ToInt32(arr[i].Substring(11, 2));
                wm.Decade = Convert.ToInt32(arr[i].Substring(14, 2));
                
                string str = arr[i].Substring(17, 8);
                double d;
                if (Double.TryParse(str, out d))
                {
                    wm.Temperature = d;
                }
                str = arr[i].Substring(26, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MinTemperature = d;
                }
                str = arr[i].Substring(35, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MaxTemperature = d;
                }
                str = arr[i].Substring(44, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Rainfall = d;
                }
                str = arr[i].Substring(53, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.SnowHight = d;
                }
                str = arr[i].Substring(62, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Humidity = d;
                }
                str = arr[i].Substring(71, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.DewPoint = d;
                }
                list.Add(wm);
            }


            return list;
        }

        public List<WeatherDay> GetWeatherDays(string stationId)
        {
            List<WeatherDay> list = new List<WeatherDay>();
            string fileName = Path.Combine(DIR_REPOS, stationId + ".day");
            string[] arr = File.ReadAllLines(fileName);
            for (int i = 0; i < arr.Length; i++)
            {
                WeatherDay wm = new WeatherDay();
                int year = Convert.ToInt32(arr[i].Substring(6, 4));
                int month = Convert.ToInt32(arr[i].Substring(11, 2));
                int day = Convert.ToInt32(arr[i].Substring(14, 2));

                wm.DateTime = new DateTime(year, month, day);

                string str = arr[i].Substring(17, 8);
                double d;
                if (Double.TryParse(str, out d))
                {
                    wm.Temperature = d;
                }
                str = arr[i].Substring(26, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MinTemperature = d;
                }
                str = arr[i].Substring(35, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.MaxTemperature = d;
                }
                str = arr[i].Substring(44, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Rainfall = d;
                }
                str = arr[i].Substring(53, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.SnowHight = d;
                }
                str = arr[i].Substring(62, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.Humidity = d;
                }
                str = arr[i].Substring(71, 8);
                if (Double.TryParse(str, out d))
                {
                    wm.DewPoint = d;
                }
                list.Add(wm);
            }


            return list;
        }


       

    }
}
