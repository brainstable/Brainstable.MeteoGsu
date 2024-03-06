using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;

namespace Brainstable.Meteo
{
    public partial class ChartTemperatureRainPeriod : UserControl
    {
        public ChartTemperatureRainPeriod()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chartControl1.Skins = Skins.Metro;

            chartControl1.BorderAppearance.SkinStyle = ChartBorderSkinStyle.None;
            chartControl1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            chartControl1.ChartArea.PrimaryXAxis.HidePartialLabels = true;
            chartControl1.ElementsSpacing = 0;

            chartControl1.PrimaryYAxis.Title = "Температура, С / Осадки, мм";
            chartControl1.PrimaryYAxis.TitleFont = new Font(chartControl1.Font.Name, 10, FontStyle.Regular);
            chartControl1.PrimaryYAxis.ValueType = ChartValueType.Double;
            chartControl1.PrimaryYAxis.MakeBreaks = true;

            //chartControl1.PrimaryXAxis.Title = "Период";
            chartControl1.PrimaryXAxis.ValueType = ChartValueType.Category;
            chartControl1.PrimaryXAxis.TitleFont = new Font(chartControl1.Font.Name, 10, FontStyle.Regular);
            chartControl1.PrimaryXAxis.LabelRotate = true;
            chartControl1.PrimaryXAxis.LabelRotateAngle = 55;

            chartControl1.Indexed = true;
            chartControl1.AllowGapForEmptyPoints = true;
        }

        public virtual void UpdateChart(int year, string plot, List<double?[]> listValues)
        {
            if (chartControl1.Series.Count == 4)
            {
                chartControl1.Series.RemoveAt(3);
                chartControl1.Series.RemoveAt(2);
                chartControl1.Series.RemoveAt(1);
                chartControl1.Series.RemoveAt(0);
            }

            chartControl1.Titles.Clear();
            chartControl1.Title.Text = $"График температуры и осадков на {plot} ГСУ\n май - сентябрь, {year} год";
            chartControl1.Title.Font = new Font(chartControl1.Font.Name, 11, FontStyle.Regular);
            chartControl1.Titles.Add(chartControl1.Title);


            double?[] t = listValues[0];
            double?[] x = listValues[7];
            double?[] r = listValues[3];
            double?[] d = listValues[10];

            CreateSeries("Температура", chartControl1, t, ChartSeriesType.Line);
            CreateSeries("Средняя многолетняя температура", chartControl1, x, ChartSeriesType.Line);
            CreateSeries("Осадки", chartControl1, r, ChartSeriesType.Column);
            CreateSeries("Средние многолетние осадки", chartControl1, d, ChartSeriesType.Column);

            chartControl1.Series[0].Style.TextOrientation = ChartTextOrientation.Down;
            chartControl1.Series[1].Style.TextOrientation = ChartTextOrientation.Up;
            chartControl1.Series[2].Style.TextOrientation = ChartTextOrientation.Up;
            chartControl1.Series[3].Style.TextOrientation = ChartTextOrientation.Down;

            chartControl1.Legend.Visible = true;
            chartControl1.Legend.VisibleCheckBox = true;


            chartControl1.Legend.RepresentationType = ChartLegendRepresentationType.Rectangle;
            for (int i = 0; i < chartControl1.Legend.Items.Length; i++)
            {
                chartControl1.Legend.Items[i].Spacing = 2;
                chartControl1.Legend.ItemsSize = new Size(13, 13);
                chartControl1.Legend.Items[i].TextAligment = VerticalAlignment.Bottom;
                chartControl1.Legend.BackColor = Color.Transparent;
                chartControl1.LegendsPlacement = ChartPlacement.Outside;
                chartControl1.LegendAlignment = ChartAlignment.Center;
                chartControl1.LegendPosition = ChartDock.Bottom;
                chartControl1.Legend.Font = new Font("Segoe UI", 10.25f);
            }

        }

        private void CreateSeries(string name, ChartControl chartControl, double?[] values, ChartSeriesType type)
        {
            ChartSeries series1 = new ChartSeries(name);
            series1.Text = series1.Name;
            series1.SortPoints = false;
            series1.Style.TextFormat = "{0:F1}";
            series1.Style.DisplayText = true;
            series1.Style.Font.Size = 9.0f;
            series1.Style.Border.Width = 4;

            series1.Type = type;
            if (type == ChartSeriesType.Line)
            {
                series1.Style.Symbol.Shape = ChartSymbolShape.Circle;
                series1.Style.Symbol.Border.Color = Color.Black;
            }

            HashSet<int> emptyPointIndexes = new HashSet<int>();

            int start = 0;
            int end = values.Length;

            if (values.Length == 12)
            {
                start = 4;
                end = 9;
            }
            else
            {
                start = 12;
                end = 27;
            }

            for (int i = start; i < end; i++)
            {
                string title = "";
                if (values.Length == 12)
                {
                    title = HelpMethods.MonthForNamesColumns[i];
                }
                else
                {
                    title = HelpMethods.DecadeForNamesColumns[i];
                }

                if (values[i].HasValue)
                {
                    series1.Points.Add(title, values[i].Value);
                }
                else
                {
                    series1.Points.Add(title, 0);
                    emptyPointIndexes.Add(i - start);
                }
            }


            chartControl.Series.Add(series1);
            foreach (int emptyPointsIndex in emptyPointIndexes)
            {
                series1.Points[emptyPointsIndex].IsEmpty = true;
            }
        }
    }
}
