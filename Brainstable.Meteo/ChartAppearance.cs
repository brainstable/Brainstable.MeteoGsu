using Syncfusion.Windows.Forms.Chart;

namespace Brainstable.Meteo
{
    public static class ChartAppearance
    {
        public static void ApplyChartStyles(ChartControl chart, string year, string title)
        {
            #region ApplyCustomPalette

            chart.Skins = Skins.Metro;

            #endregion

            #region Chart Appearance Customization

            chart.Title.Name = "Def_title";
            chart.Title.Text = $"{year} год";
            chart.Titles.Clear();
            chart.Titles.Add(chart.Title);
            chart.ZoomOutIncrement = 1.5D;

            chart.BorderAppearance.SkinStyle = Syncfusion.Windows.Forms.Chart.ChartBorderSkinStyle.None;
            chart.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            chart.ChartArea.PrimaryXAxis.HidePartialLabels = true;
            chart.PrimaryXAxis.LabelPlacement = ChartAxisLabelPlacement.BetweenTicks;
            //chart.PrimaryXAxis.DrawTickLabelGrid = true;
            //chart.PrimaryYAxis.DrawTickLabelGrid = true;
            chart.PrimaryXAxis.ValueType = ChartValueType.Category;

            //ChartAxisGroupingLabel xAxisgroupingLabel = new ChartAxisGroupingLabel(new DoubleRange(-0.5, 2.5), "Квартал 1");
            //xAxisgroupingLabel.Row = 1;

            //ChartAxisGroupingLabel xAxisgroupingLabel1 = new ChartAxisGroupingLabel(new DoubleRange(2.5, 5.5), "Квартал 2");
            //xAxisgroupingLabel1.Row = 1;

            //ChartAxisGroupingLabel xAxisgroupingLabel2 = new ChartAxisGroupingLabel(new DoubleRange(5.5, 8.5), "Квартал 3");
            //xAxisgroupingLabel2.Row = 1;

            //ChartAxisGroupingLabel xAxisgroupingLabel3 = new ChartAxisGroupingLabel(new DoubleRange(8.5, 11.5), "Квартал 4");
            //xAxisgroupingLabel3.Row = 1;

            //ChartAxisGroupingLabel xAxisgroupingLabel4 = new ChartAxisGroupingLabel(new DoubleRange(-0.5, 5.5), "1-ое полугодие");
            //xAxisgroupingLabel4.Row = 2;

            //ChartAxisGroupingLabel xAxisgroupingLabel5 = new ChartAxisGroupingLabel(new DoubleRange(5.5, 11.5), "2-ое полугодие");
            //xAxisgroupingLabel5.Row = 2;

            //ChartAxisGroupingLabel xAxisgroupingLabel6 = new ChartAxisGroupingLabel(new DoubleRange(-0.5, 11.5), year.ToString());
            //xAxisgroupingLabel6.Row = 1;


            chart.PrimaryXAxis.GroupingLabels.Clear();

            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel1);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel2);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel3);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel4);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel5);
            //chart.PrimaryXAxis.GroupingLabels.Add(xAxisgroupingLabel6);

            //ChartAxisGroupingLabel yAxisgroupingLabel = new ChartAxisGroupingLabel(new DoubleRange(0, 13), "Low");

            //ChartAxisGroupingLabel yAxisgroupingLabel1 = new ChartAxisGroupingLabel(new DoubleRange(13, 26), "Medium");

            //ChartAxisGroupingLabel yAxisgroupingLabel2 = new ChartAxisGroupingLabel(new DoubleRange(26, 40), "High");

            //chart.PrimaryYAxis.GroupingLabels.Add(yAxisgroupingLabel);
            //chart.PrimaryYAxis.GroupingLabels.Add(yAxisgroupingLabel1);
            //chart.PrimaryYAxis.GroupingLabels.Add(yAxisgroupingLabel2);

            chart.ElementsSpacing = 0;

            #endregion
        }
    }
}