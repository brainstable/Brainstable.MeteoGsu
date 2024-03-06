using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;

namespace Brainstable.Meteo
{
    public partial class ChartWeather : UserControl
    {
        private ChartSeries series1;

        private ChartSeriesType seriesType = ChartSeriesType.Column;


        public ChartWeather()
        {
            InitializeComponent();
            InitializeChart();
        }

        public ChartSeries Series1
        {
            get => series1;
            set
            {
                series1 = value;
            }
        }

        private void InitializeChart()
        {
            chartControl1.Skins = Skins.Metro;
            chartControl1.Legend.Visible = false;

            chartControl1.ShowLegend = false;
            chartControl1.BorderAppearance.SkinStyle = ChartBorderSkinStyle.None;
            chartControl1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            chartControl1.ChartArea.PrimaryXAxis.HidePartialLabels = true;
            chartControl1.ElementsSpacing = 0;

            //chartControl1.ToolBar.Items.Add(new ChartToolBarSaveExcelItem());


            //series1 = new ChartSeries("chartSerie1");
            //series1.Text = series1.Name;
            //series1.Type = seriesType;
        }


        static int counter = 0;

        public void ClearChart()
        {
            chartControl1.Series.Clear();
            chartControl1.Legends.Clear();
            chartControl1.Titles.Clear();

            chartControl1.PrimaryYAxis.Title = "";
            chartControl1.PrimaryYAxis.Title = "";
        }

        public void UpdateChart(IChartData chartData, string title, string titleXAxis, string titleYAxis)
        {
            try
            {
                chartControl1.Series.Clear();
                chartControl1.Legends.Clear();
                chartControl1.Titles.Clear();

                chartControl1.Title.Font = new Font(chartControl1.Font.Name, 11, FontStyle.Regular);
                chartControl1.Title.Name = "Def_title";
                chartControl1.Title.Text = title;
                chartControl1.Titles.Add(chartControl1.Title);

                chartControl1.PrimaryYAxis.Title = titleYAxis;
                chartControl1.PrimaryYAxis.TitleFont = new Font(chartControl1.Font.Name, 10, FontStyle.Regular);
                chartControl1.PrimaryYAxis.ValueType = ChartValueType.Double;

                chartControl1.PrimaryXAxis.Title = titleXAxis;
                chartControl1.PrimaryXAxis.ValueType = ChartValueType.Category;
                chartControl1.PrimaryXAxis.TitleFont = new Font(chartControl1.Font.Name, 10, FontStyle.Regular);
                chartControl1.PrimaryXAxis.LabelRotate = true;
                chartControl1.PrimaryXAxis.LabelRotateAngle = 55;

                chartControl1.Indexed = true;
                chartControl1.AllowGapForEmptyPoints = true;

                series1.SortPoints = false;
                series1.Style.TextFormat = "{0:F1}";
                series1.Style.DisplayText = true;
                series1.Style.Font.Size = 9.0f;
                series1.Style.Border.Width = 4;
                series1.Style.TextOrientation = ChartTextOrientation.Smart;
               
                if (series1.Points.Count > 0)
                    series1.Points.Clear();

                HashSet<int> emptyPointIndexes = new HashSet<int>();
                for (int i = 0; i < chartData.CountElements; i++)
                {
                    if (chartData.YValues[i].HasValue)
                    {
                        series1.Points.Add(chartData.XNames[i], chartData.YValues[i].Value);
                    }
                    else
                    {
                        series1.Points.Add(chartData.XNames[i], 0);
                        emptyPointIndexes.Add(i);
                    }
                }


                chartControl1.Series.Add(series1);

                if (chartControl1.Series[0].Type != ChartSeriesType.Spline)
                {
                    foreach (int emptyPointsIndex in emptyPointIndexes)
                    {
                        chartControl1.Series[0].Points[emptyPointsIndex].IsEmpty = true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


       
    }
}
