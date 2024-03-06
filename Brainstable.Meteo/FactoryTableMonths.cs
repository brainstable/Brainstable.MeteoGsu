using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryTableMonths
    {
        

        private DataSet ds;

        public DataSet DataSet => ds;
        public int MinYear { get; private set; }
        public int MaxYear { get; private set; }

        public FactoryTableMonths()
        {
            
        }

        public void Create(List<WeatherMonth> months)
        {
            ds = new DataSet("WeatherMonths");
            MinYear = months.Min(day => day.Year);
            MaxYear = months.Max(day => day.Year);

            DataTable dtT = new DataTable("Temperatures");
            CreateSchema(dtT);
            DataTable dtMinT = new DataTable("MinTemperatures");
            CreateSchema(dtMinT);
            DataTable dtMaxT = new DataTable("MaxTemperatures");
            CreateSchema(dtMaxT);
            DataTable dtR = new DataTable("Rainfalls");
            CreateSchema(dtR);
            DataTable dtSH = new DataTable("SnowHights");
            CreateSchema(dtSH);
            DataTable dtH = new DataTable("Humidities");
            CreateSchema(dtH);
            DataTable dtDP = new DataTable("DewPoints");
            CreateSchema(dtDP);

            for (int i = MinYear; i <= MaxYear; i++)
            {
                DataRow rowT = dtT.NewRow();
                rowT[0] = i;
                DataRow rowMinT = dtMinT.NewRow();
                rowMinT[0] = i;
                DataRow rowMaxT = dtMaxT.NewRow();
                rowMaxT[0] = i;
                DataRow rowR = dtR.NewRow();
                rowR[0] = i;
                DataRow rowSH = dtSH.NewRow();
                rowSH[0] = i;
                DataRow rowH = dtH.NewRow();
                rowH[0] = i;
                DataRow rowDP = dtDP.NewRow();
                rowDP[0] = i;


                int n = 1;
                int m = 12 + n;
                for (int j = n; j < m; j++)
                {
                    IWeatherParameters weather = months.FirstOrDefault(d => d.Year == i & d.TableName == dtT.Columns[j].ColumnName);
                    if (weather != null)
                    {
                        rowT[j] = weather.Temperature.HasValue ? (object)weather.Temperature : DBNull.Value;
                        rowMinT[j] = weather.MinTemperature.HasValue ? (object)weather.MinTemperature : DBNull.Value;
                        rowMaxT[j] = weather.MaxTemperature.HasValue ? (object)weather.MaxTemperature : DBNull.Value;
                        rowR[j] = weather.Rainfall.HasValue ? (object)weather.Rainfall : DBNull.Value;
                        rowSH[j] = weather.SnowHight.HasValue ? (object)weather.SnowHight : DBNull.Value;
                        rowH[j] = weather.Humidity.HasValue ? (object) weather.Humidity : DBNull.Value;
                        rowDP[j] = weather.DewPoint.HasValue ? (object)weather.DewPoint : DBNull.Value;
                    }
                }

                dtT.Rows.Add(rowT);
                dtMinT.Rows.Add(rowMinT);
                dtMaxT.Rows.Add(rowMaxT);
                dtR.Rows.Add(rowR);
                dtSH.Rows.Add(rowSH);
                dtH.Rows.Add(rowH);
                dtDP.Rows.Add(rowDP);
            }

            ds.Tables.AddRange(new[] { dtT, dtMinT, dtMaxT, dtR, dtSH, dtH, dtDP });
        }


        private void CreateSchema(DataTable dt)
        {
            DataColumn col = new DataColumn("Year", typeof(int));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            for (int i = 0; i < 12; i++)
            {
                col = new DataColumn(HelpMethods.MonthForNamesColumns[i], typeof(double));
                col.ReadOnly = true;
                dt.Columns.Add(col);
            }
        }
    }
}