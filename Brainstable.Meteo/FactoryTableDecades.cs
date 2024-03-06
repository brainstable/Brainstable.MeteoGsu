using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryTableDecades
    {
        private DataSet ds;
        public int MinYear { get; private set; }
        public int MaxYear { get; private set; }
        public DataSet DataSet => ds;

        public FactoryTableDecades()
        {
            ds = new DataSet("WeatherDecades");
            

        }

        public void CreateTransitionDataTables(List<WeatherDecade> decades, List<WeatherDay> days)
        {
            MinYear = decades.Min(day => day.Year);
            MaxYear = decades.Max(day => day.Year);

            CreateDatePeriod(0, decades, days);
            CreateDatePeriod(5, decades, days);
            CreateDatePeriod(10, decades, days);
            CreateDatePeriod(15, decades, days);
        }


        private void CreateDatePeriod(double temperature, List<WeatherDecade> decades, List<WeatherDay> days)
        { 
            DataTable dt = new DataTable("TransitionTemperature" + temperature);
            CreateSchemaPeriod(dt);
            List<TransitionTemperature> periods0 = CreatePeriods(decades, days, temperature).OrderByDescending(p => p.Year).ToList();

            DataRow rowT = dt.NewRow();
            //rowT[0] = "Средние";
            //rowT[1] = periods0.Average(p => p.FirstDate.Value).Value;
            //rowT[2] = period.LastDateString;
            //rowT[3] = period.CountDays;
            //rowT[4] = period.SumTemperatures;
            //rowT[5] = period.SumRainfalls;
            //dt.Rows.Add(rowT);

            foreach (var period in periods0)
            {
                rowT = dt.NewRow();
                rowT[0] = period.Year;
                rowT[1] = period.FirstDate.HasValue ? period.FirstDate.Value : DateTime.Now;
                rowT[2] = period.LastDate.HasValue ? period.LastDate.Value : DateTime.Now;
                rowT[3] = period.CountDays;
                rowT[4] = period.SumTemperatures;
                rowT[5] = period.SumRainfalls;
                dt.Rows.Add(rowT);
            }
            ds.Tables.AddRange(new[] { dt });
        }

        void CreateDataTable(DataTable table, string nameDataTable)
        {

        }

        private List<TransitionTemperature> CreatePeriods(List<WeatherDecade> decades, List<WeatherDay> days, double temperature)
        {
            List<TransitionTemperature> periods = new List<TransitionTemperature>();
            MinYear = decades.Min(day => day.Year);
            MaxYear = decades.Max(day => day.Year);

            for (int i = MinYear; i <= MaxYear; i++)
            {
                periods.Add(TransitionTemperature.CreateTransitionTemperature(i, decades, days, temperature));
            }

            return periods;
        }


        public void Create(List<WeatherDecade> decades)
        {
            //ds = new DataSet("WeatherDecades");

            MinYear = decades.Min(day => day.Year);
            MaxYear = decades.Max(day => day.Year);

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
                int m = 36 + n;
                for (int j = n; j < m; j++)
                {
                    IWeatherParameters weather = decades.FirstOrDefault(d => d.Year == i & d.TableName == dtT.Columns[j].ColumnName);
                    if (weather != null)
                    {
                        rowT[j] = weather.Temperature.HasValue ? (object)weather.Temperature : DBNull.Value;
                        rowMinT[j] = weather.MinTemperature.HasValue ? (object)weather.MinTemperature : DBNull.Value;
                        rowMaxT[j] = weather.MaxTemperature.HasValue ? (object)weather.MaxTemperature : DBNull.Value;
                        rowR[j] = weather.Rainfall.HasValue ? (object)weather.Rainfall : DBNull.Value;
                        rowSH[j] = weather.SnowHight.HasValue ? (object) weather.SnowHight : DBNull.Value;
                        rowH[j] = weather.Humidity.HasValue ? (object)weather.Humidity : DBNull.Value;
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

            ds.Tables.AddRange(new []{dtT, dtMinT, dtMaxT, dtR, dtSH, dtH, dtDP});
        }

        private void CreateSchema(DataTable dt)
        {
            DataColumn col = new DataColumn("Year", typeof(int));
            col.ReadOnly = true;
            dt.Columns.Add(col);
            
            for (int i = 0; i < 36; i++)
            {
                col = new DataColumn(HelpMethods.DecadeForNamesColumns[i], typeof(double));
                col.ReadOnly = true;
                dt.Columns.Add(col);
            }
        }

        private void CreateSchemaPeriod(DataTable dt)
        {
            DataColumn col = new DataColumn("Year", typeof(string));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn("FirstDate", typeof(DateTime));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn("LastDate", typeof(DateTime));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn("TransitionTemperature", typeof(string));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn("SumTemperatures", typeof(string));
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn("SumRainfalls", typeof(string));
            col.ReadOnly = true;
            dt.Columns.Add(col);
        }

    }
}