using System;
using System.Data;

namespace Brainstable.Meteo
{
    public class ChartDataColumn : IChartData
    {
        public int CountElements { get; }
        public string[] XNames { get; }
        public double?[] YValues { get; }

        public ChartDataColumn(int columnIndex, DataTable dataTable)
        {
            CountElements = dataTable.Rows.Count;

            XNames = new string[CountElements];
            for (int i = 0; i < CountElements; i++)
            {
                XNames[i] = dataTable.Rows[i][0].ToString();
            }
            YValues = new double?[CountElements];
            for (int i = 0; i < CountElements; i++)
            {
                double d;
                if (Double.TryParse(dataTable.Rows[i][columnIndex].ToString(), out d))
                {
                    YValues[i] = d;
                }
            }
        }
    }
}