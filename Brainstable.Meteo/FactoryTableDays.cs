using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Brainstable.Meteo
{
    public class FactoryTableDays
    {
        private DataSet ds;

        public FactoryTableDays()
        {
            ds = new DataSet("WeatherDays");
        }

        public void Create(List<WeatherDay> days, WeatherParameter parameter)
        {
            int minYear = days.Min(day => day.Year);
            int maxYear = days.Max(day => day.Year);

            DataTable dt = null;
            for (int i = minYear; i <= maxYear; i++)
            {
                dt = new DataTable(i.ToString());
                CreateSchema(i, dt);
                //FillData(decades.Where(d => d.Year == i), dt, parameter);
                ds.Tables.Add(dt);

            }

            ;
        }

        private void FillData(int minYear, int maxYear, IEnumerable<WeatherDay> days, DataTable dt, WeatherParameter parameter)
        {
            for (int i = minYear; i <= maxYear; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    WeatherDay day = days.FirstOrDefault(d => d.Year == i & d.TableName == dt.Columns[j].ColumnName);
                    if (day != null)
                        row[j] = day.Temperature;

                }
                dt.Rows.Add(row);
            }
            
            
        }

        private void CreateSchema(int year, DataTable dt)
        {
            DataColumn col = null;
            for (int i = 0; i < 12; i++)
            {
                int countDays = DateTime.DaysInMonth(year, i + 1);
                int day = 1;
                for (int j = 0; j < countDays; j++)
                {
                    col = new DataColumn($"{day.ToString("D2")}.{(i + 1).ToString("D2")}", typeof(double));
                    col.ReadOnly = true;
                    dt.Columns.Add(col);
                    day++;
                }
            }
        }

    }
}