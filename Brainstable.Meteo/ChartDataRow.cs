using System;
using System.Data;

namespace Brainstable.Meteo
{
    public class ChartDataRow : IChartData
    {
        public int CountElements { get; private set; }
        public string[] XNames { get; private set; }
        public double?[] YValues { get; private set; }

        public ChartDataRow(DataRow dataRow)
        {
            CountElements = dataRow.Table.Columns.Count - 1;

            XNames = new string[CountElements];
            int k = 0;
            for (int i = 1; i < CountElements + 1; i++)
            {
                XNames[k] = dataRow.Table.Columns[i].ColumnName;
                k++;
            }

            YValues = new double?[CountElements];
            k = 0;
            for (int i = 1; i < CountElements + 1; i++)
            {
                double d;
                if (Double.TryParse(dataRow[i].ToString(), out d))
                {
                    YValues[k] = d;
                }

                k++;
            }
        }


    }
}
