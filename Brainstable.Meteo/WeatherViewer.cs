using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Chart;
using System.Diagnostics;
using Process = System.Diagnostics.Process;

namespace Brainstable.Meteo
{
    public partial class WeatherViewer : UserControl
    {
        private WeatherParameter wparameter = WeatherParameter.Temperature;
        private WeatherStyle wstyle = WeatherStyle.ByDecades;
       
        private int currentRowIndex = 0;
        private DataTable dt;
      

        private RadioButton[] rbParameters;
        private FactoryWeather factoryWeather;
        private FactoryTableDecades factoryTableDecades;
        private FactoryTableMonths factoryTableMonths;
        private MeteoStation station;

        private string stationId;
        private readonly string plotName;
        private readonly string plotAblativeName;
        private readonly double distance;

        ToolStripButton[] arrToolStripButtons;
        private DataView viewTransitionTemperature;


        public WeatherViewer(MeteoStation station, string plotName, string plotAblativeName, double distance)
        {
            InitializeComponent();
            radioButton1.Text = $"0{Convert.ToChar(0x00B0)}C";
            radioButton2.Text = $"5{Convert.ToChar(0x00B0)}C";
            radioButton3.Text = $"10{Convert.ToChar(0x00B0)}C";
            radioButton4.Text = $"15{Convert.ToChar(0x00B0)}C";


            this.stationId = station.Id;
            this.plotName = plotName;
            this.plotAblativeName = plotAblativeName;
            this.distance = distance;
            this.station = station;

            factoryWeather = new FactoryWeather();
            factoryWeather.Create(stationId);

            factoryTableDecades = new FactoryTableDecades();
            factoryTableDecades.Create(factoryWeather.WeatherDecades);
            factoryTableDecades.CreateTransitionDataTables(factoryWeather.WeatherDecades, factoryWeather.WeatherDays);

            factoryTableMonths = new FactoryTableMonths();
            factoryTableMonths.Create(factoryWeather.WeatherMonths);

            arrToolStripButtons = new []
            {
                btnColumn, btnSpline
            };

            cbxParameters.Items.AddRange(new []
            {
                "Температура", 
                "Мин. температура",
                "Макс. температура",
                "Осадки",
                "Снежный покров",
                "Влажность",
                "Точка росы"
            });
            cbxParameters.SelectedIndex = 0;

            toolStripEx1.Text = plotName + " ГСУ (Метеостанция: " + 
                                station.Name + " [" + stationId + "], расстояние до метеостанции: " + 
                                distance + " км) ";

            btnUp.Enabled = !splitContainer1.Panel1Collapsed;
            btnDown.Enabled = splitContainer1.Panel1Collapsed;

            radioButton1.Checked = true;
        }



        private void WeatherViewer_Load(object sender, EventArgs e)
        {
            dataGridWeatherViewer1.SelectedChangeRow += DataGridWeatherViewer1_SelectedChangeRow;
            dataGridWeatherViewer1.SelectedChangeColumn += DataGridWeatherViewer1_SelectedChangeColumn;
            rbtByMonths_CheckedChanged(null, null);
        }

        

        /*
         * График по столбцам
         */
        private void DataGridWeatherViewer1_SelectedChangeColumn(object sender, EventArgs e)
        {
            if (dataGridWeatherViewer1.SelectedColumnIndex > 0)
            {
                if (dataGridWeatherViewer1.ChartDataColumn != null)
                {
                    string nameCol = GetNameColumn(dataGridWeatherViewer1.SelectedColumnIndex, dataGridWeatherViewer1.Table, wstyle);
                    chartWeather2.UpdateChart(dataGridWeatherViewer1.ChartDataColumn, GetTitleColumn(wstyle, wparameter, plotAblativeName, nameCol),
                        GetTitleXAxisColumn(), GetTitleYAxisColumn(wparameter));
                }
            }
            else
            {
                chartWeather2.ClearChart();
            }
        }

        /*
         * График по строкам
         */

        private int year;
        private void DataGridWeatherViewer1_SelectedChangeRow(object sender, EventArgs e)
        {
            if (dataGridWeatherViewer1.SelectedDataRow != null)
            {
                IChartData cdr = dataGridWeatherViewer1.ChartDataRow;
                chartWeather1.UpdateChart(dataGridWeatherViewer1.ChartDataRow, 
                    GetTitleRow(wstyle, wparameter, plotAblativeName, dataGridWeatherViewer1.SelectedDataRow[0].ToString()),
                GetTitleXAxisRow(wstyle), GetTitleYAxisRow(wparameter));

                int indexRow = dataGridWeatherViewer1.SelectedRowIndex;
                year = Convert.ToInt32(dataGridWeatherViewer1.View[indexRow][0].ToString());

                List<double?[]> list = new List<double?[]>();
                Dictionary<int, double> dictGtk = new Dictionary<int, double>();
                switch (wstyle)
                {
                    case WeatherStyle.ByDays:
                        break;
                    case WeatherStyle.ByDecades:
                        list = factoryWeather.GetValuesDecadesByYear(year);
                        
                        break;
                    case WeatherStyle.ByMonths:
                        list = factoryWeather.GetValuesMonthsByYear(year);
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                chartTemperatures1.UpdateChart(year, plotAblativeName, list);
                chartTemperatureRain1.UpdateChart(year, plotAblativeName, list);
                chartTemperatureRainPeriod1.UpdateChart(year, plotAblativeName, list);
                chartTemperatureGtk1.UpdateChart(year, plotAblativeName, factoryWeather.WeatherDays, wstyle);
                
                //MessageBox.Show(dt[0].ToString());
            }
        }

        #region Выбор показателя

        

        private void rbtTemperature_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.Temperature;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Temperatures"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Temperatures"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            
            //dataGridView1.DataSource = viewTransitionTemperature;


        }
        private void rbtMinTemperature_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.MinTemperature;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["MinTemperatures"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["MinTemperatures"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtMaxTemperature_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.MaxTemperature;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["MaxTemperatures"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["MaxTemperatures"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtRainfall_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.Rainfall;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Rainfalls"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Rainfalls"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtSnowHight_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.SnowHight;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["SnowHights"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["SnowHights"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtHumidity_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.Humidity;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Humidities"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Humidities"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtDewPoint_CheckedChanged(object sender, EventArgs e)
        {
            wparameter = WeatherParameter.DewPoint;
            switch (wstyle)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["DewPoints"]);
                    break;
                case WeatherStyle.ByMonths:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["DewPoints"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Выборка



        private void rbtByMonths_CheckedChanged(object sender, EventArgs e)
        {
            wstyle = WeatherStyle.ByMonths;
            switch (wparameter)
            {
                case WeatherParameter.Temperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Temperatures"]);
                    break;
                case WeatherParameter.MinTemperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["MinTemperatures"]);
                    break;
                case WeatherParameter.MaxTemperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["MaxTemperatures"]);
                    break;
                case WeatherParameter.Rainfall:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Rainfalls"]);
                    break;
                case WeatherParameter.SnowHight:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["SnowHights"]);
                    break;
                case WeatherParameter.DewPoint:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["DewPoints"]);
                    break;
                case WeatherParameter.Humidity:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableMonths.DataSet.Tables["Humidities"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void rbtByDecades_CheckedChanged(object sender, EventArgs e)
        {
            wstyle = WeatherStyle.ByDecades;
            switch (wparameter)
            {
                case WeatherParameter.Temperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Temperatures"]);
                    break;
                case WeatherParameter.MinTemperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["MinTemperatures"]);
                    break;
                case WeatherParameter.MaxTemperature:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["MaxTemperatures"]);
                    break;
                case WeatherParameter.Rainfall:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Rainfalls"]);
                    break;
                case WeatherParameter.SnowHight:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["SnowHights"]);
                    break;
                case WeatherParameter.DewPoint:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["DewPoints"]);
                    break;
                case WeatherParameter.Humidity:
                    dataGridWeatherViewer1.BindingTableSource(factoryTableDecades.DataSet.Tables["Humidities"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        #endregion

        #region Titles

        private string GetNameColumn(int indexColumn, DataTable dataTable, WeatherStyle style)
        {
            string name = dataTable.Columns[indexColumn].ColumnName;
            string str = "";
            if (name.Contains("Янв"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " января";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "январь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Фев"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " февраля";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "февраль";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Мар"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " марта";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "март";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Апр"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " апреля";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "апрель";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Май"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " мая";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "май";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Июн"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " июня";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "июнь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Июл"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " июля";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "июль";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Авг"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " августа";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "август";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Сен"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " сентября";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "сентябрь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Окт"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " октября";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "октябрь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Ноя"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " ноября";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "ноябрь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }
            if (name.Contains("Дек"))
            {
                switch (style)
                {
                    case WeatherStyle.ByDays:
                        str = name;
                        break;
                    case WeatherStyle.ByDecades:
                        str = GetNumberDecade(name) + " декабря";
                        break;
                    case WeatherStyle.ByMonths:
                        str = "декабрь";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(style), style, null);
                }
            }

            string GetNumberDecade(string nameColumn)
            {
                string strdec = "";
                if (nameColumn.Contains("1"))
                {
                    strdec = "1-ю декаду";
                }
                else if (nameColumn.Contains("2"))
                {
                    strdec = "2-ю декаду";
                }
                else if (nameColumn.Contains("3"))
                {
                    strdec = "3-ю декаду";
                }
                return strdec;
            }

            return str;
        }

        private string GetTitleColumn(WeatherStyle style, WeatherParameter parameter, string plotAblativeName, string nameColumn)
        {
            string str = "";
            str = GetFirstTitle(parameter) + " на " + plotAblativeName + " ГСУ за " + GetLastTitleColumn(style, nameColumn) + " по годам";
            return str;
        }

        private string GetLastTitleColumn(WeatherStyle style, string nameColumn)
        {
            return nameColumn;
        }

        private string GetTitleYAxisColumn(WeatherParameter parameter)
        {
            return GetTitleYAxisRow(parameter);
        }
        private string GetTitleXAxisColumn()
        {
            return "Года";
        }

        private string GetTitleRow(WeatherStyle style, WeatherParameter parameter, string plotAblativeName, string year)
        {
            return GetFirstTitle(parameter) + " на " + plotAblativeName + " ГСУ за " + year + " год " + GetLastTitleRow(style);
        }



        private string GetTitleXAxisRow(WeatherStyle style)
        {
            string str = "";
            switch (style)
            {
                case WeatherStyle.ByDays:
                    str = "Дни";
                    break;
                case WeatherStyle.ByDecades:
                    str = "Декады";
                    break;
                case WeatherStyle.ByMonths:
                    str = "Месяцы";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
            return str;
        }

        private string GetTitleYAxisRow(WeatherParameter parameter)
        {
            return GetFirstTitle(wparameter) + ", " + GetUnit(wparameter);
        }

        private string GetLastTitleRow(WeatherStyle style)
        {
            string str = "";
            switch (style)
            {
                case WeatherStyle.ByDays:
                    break;
                case WeatherStyle.ByDecades:
                    str = "по декадам";
                    break;
                case WeatherStyle.ByMonths:
                    str = "по месяцам";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }

            return str;
        }

        private string GetFirstTitle(WeatherParameter parameter)
        {
            string str = "";
            switch (parameter)
            {
                case WeatherParameter.Temperature:
                    str = "Температура";
                    break;
                case WeatherParameter.MinTemperature:
                    str = "Максимальная температура";
                    break;
                case WeatherParameter.MaxTemperature:
                    str = "Минимальная температура";
                    break;
                case WeatherParameter.Rainfall:
                    str = "Осадки";
                    break;
                case WeatherParameter.SnowHight:
                    str = "Высота снежного покрова";
                    break;
                case WeatherParameter.DewPoint:
                    str = "Температура точки росы";
                    break;
                case WeatherParameter.Humidity:
                    str = "Относительная влажность";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
            }

            return str;
        }

        private string GetUnit(WeatherParameter parameter)
        {
            string str = "";
            switch (parameter)
            {
                case WeatherParameter.Temperature:
                    str = Convert.ToChar(0x00B0).ToString() + "C";
                    break;
                case WeatherParameter.MinTemperature:
                    str = Convert.ToChar(0x00B0).ToString() + "C";
                    break;
                case WeatherParameter.MaxTemperature:
                    str = Convert.ToChar(0x00B0).ToString() + "C";
                    break;
                case WeatherParameter.Rainfall:
                    str = "мм";
                    break;
                case WeatherParameter.SnowHight:
                    str = "см";
                    break;
                case WeatherParameter.DewPoint:
                    str = Convert.ToChar(0x00B0).ToString() + "C";
                    break;
                case WeatherParameter.Humidity:
                    str = "%";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
            }

            return str;
        }


        #endregion

        #region ChartSeriesType

        private ChartSeriesType seriesType = ChartSeriesType.Spline;

        private void btnColumn_Click(object sender, EventArgs e)
        {
            seriesType = ChartSeriesType.Column;
            try
            {
                chartWeather1.Series1.Type = seriesType;
                chartWeather2.Series1.Type = seriesType;
                chartWeather1.Series1.Style.Symbol.Shape = ChartSymbolShape.None;
                chartWeather2.Series1.Style.Symbol.Shape = ChartSymbolShape.None;
            }
            catch { }
            for (int i = 0; i < arrToolStripButtons.Length; i++)
            {
                arrToolStripButtons[i].Checked = false;
            }
            arrToolStripButtons[0].Checked = true;
        }

        private void btnSpline_Click(object sender, EventArgs e)
        {
            seriesType = ChartSeriesType.Line;
            try
            {
                chartWeather1.Series1.Type = seriesType;
                chartWeather2.Series1.Type = seriesType;
                chartWeather1.Series1.Style.Symbol.Shape = ChartSymbolShape.Circle;
                chartWeather2.Series1.Style.Symbol.Shape = ChartSymbolShape.Circle;
            }
            catch { }

            for (int i = 0; i < arrToolStripButtons.Length; i++)
            {
                arrToolStripButtons[i].Checked = false;
            }
            arrToolStripButtons[1].Checked = true;
        }

        
        #endregion

        

        private void cbxParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbxParameters.SelectedIndex;
            if (i != -1)
            {
                switch (i)
                {
                    case 0:
                        rbtTemperature_CheckedChanged(null, null);
                        break;
                    case 1:
                        rbtMinTemperature_CheckedChanged(null, null);
                        break;
                    case 2:
                        rbtMaxTemperature_CheckedChanged(null, null);
                        break;
                    case 3:
                        rbtRainfall_CheckedChanged(null, null);
                        break;
                    case 4:
                        rbtSnowHight_CheckedChanged(null, null);
                        break;
                    case 5:
                        rbtHumidity_CheckedChanged(null, null);
                        break;
                    case 6:
                        rbtDewPoint_CheckedChanged(null, null);
                        break;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DateTime?[] dt = Algorithm.GetDateRun(0, year, factoryWeather.WeatherDecades);
            string str1 = dt[0].HasValue ? dt[0].Value.ToShortDateString() : "";
            string str2 = dt[1].HasValue ? dt[1].Value.ToShortDateString() : "";
            MessageBox.Show(str1 + "\n" + str2); 
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            btnUp.Enabled = !splitContainer1.Panel1Collapsed;
            btnDown.Enabled = splitContainer1.Panel1Collapsed;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = false;
            btnUp.Enabled = !splitContainer1.Panel1Collapsed;
            btnDown.Enabled = splitContainer1.Panel1Collapsed;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ReportData report = new ReportData();
            
            saveFileDialog1.Filter = "Excel files|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "МЕТЕОУСЛОВИЯ - " + plotName + " ГСУ";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                report.Run(saveFileDialog1.FileName,plotName, factoryTableDecades.DataSet, factoryTableMonths.DataSet, factoryWeather.WeatherYears);
                Process.Start(saveFileDialog1.FileName);
            }
        }

        private double currentTransitionTemperature = 0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentTransitionTemperature = 0;
            viewTransitionTemperature = new DataView(factoryTableDecades.DataSet.Tables["TransitionTemperature0"]);
            //view.Sort = "Year DESC";
            dataGridView1.DataSource = viewTransitionTemperature;
            dataGridView1.Columns[0].HeaderText = "Год";
            dataGridView1.Columns[1].HeaderText = "Дата весной";
            dataGridView1.Columns[2].HeaderText = "Дата осенью";
            dataGridView1.Columns[3].HeaderText = "Кол-во дней";
            dataGridView1.Columns[4].HeaderText = "Сумма температур";
            dataGridView1.Columns[5].HeaderText = "Сумма осадков";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentTransitionTemperature = 5;
            viewTransitionTemperature = new DataView(factoryTableDecades.DataSet.Tables["TransitionTemperature5"]);
            //view.Sort = "Year DESC";
            dataGridView1.DataSource = viewTransitionTemperature;
            dataGridView1.Columns[0].HeaderText = "Год";
            dataGridView1.Columns[1].HeaderText = "Дата весной";
            dataGridView1.Columns[2].HeaderText = "Дата осенью";
            dataGridView1.Columns[3].HeaderText = "Кол-во дней";
            dataGridView1.Columns[4].HeaderText = "Сумма температур";
            dataGridView1.Columns[5].HeaderText = "Сумма осадков";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            currentTransitionTemperature = 10;
            viewTransitionTemperature = new DataView(factoryTableDecades.DataSet.Tables["TransitionTemperature10"]);
            //view.Sort = "Year DESC";
            dataGridView1.DataSource = viewTransitionTemperature;
            dataGridView1.Columns[0].HeaderText = "Год";
            dataGridView1.Columns[1].HeaderText = "Дата весной";
            dataGridView1.Columns[2].HeaderText = "Дата осенью";
            dataGridView1.Columns[3].HeaderText = "Кол-во дней";
            dataGridView1.Columns[4].HeaderText = "Сумма температур";
            dataGridView1.Columns[5].HeaderText = "Сумма осадков";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            currentTransitionTemperature = 15;
            viewTransitionTemperature = new DataView(factoryTableDecades.DataSet.Tables["TransitionTemperature15"]);
            //view.Sort = "Year DESC";
            dataGridView1.DataSource = viewTransitionTemperature;
            dataGridView1.Columns[0].HeaderText = "Год";
            dataGridView1.Columns[1].HeaderText = "Дата весной";
            dataGridView1.Columns[2].HeaderText = "Дата осенью";
            dataGridView1.Columns[3].HeaderText = "Кол-во дней";
            dataGridView1.Columns[4].HeaderText = "Сумма температур";
            dataGridView1.Columns[5].HeaderText = "Сумма осадков";
        }

        private void toolStripRadioButtonByMonths_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            rbtByMonths_CheckedChanged(null, null);
            Cursor = Cursors.Default;
        }

        private void toolStripRadioButtonByDecades_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            rbtByDecades_CheckedChanged(null, null);
            Cursor = Cursors.Default;
        }
    }
}
