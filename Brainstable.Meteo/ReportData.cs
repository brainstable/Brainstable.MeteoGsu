using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.XlsIO;

namespace Brainstable.Meteo
{
    public class ReportData
    {
        private const string FILE_NAME_TEMPLATE = "templates\\meteo.xlsx";

        private string[] arrPrameters =
        {
            "Средняя температура, °С",
            "Средняя температура, °С",
            "Средняя температура, °С",
            "Осадки, мм",
            "Высота снежного покрова, см",
            "Относительная влажность, %",
            "Температура точки росы, °С"
        };

        private string[] transiotionNames =
        {
            "TransitionTemperature0",
            "TransitionTemperature5",
            "TransitionTemperature10",
            "TransitionTemperature15"
        };

        public void Run(string fileNameSave, string plotAblative, DataSet dsDecades, DataSet dsMonth, List<WeatherYear> years)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(FILE_NAME_TEMPLATE);
                IWorksheet worksheet = workbook.Worksheets[0];

                CreateTitul(workbook, plotAblative);
                CreateDecades(workbook, plotAblative, dsDecades);
                CreateMonths(workbook, plotAblative, dsMonth);
                CreateYears(workbook, plotAblative, years);

                //CreateHyperlinks(workbook);
                worksheet.Activate();
                worksheet.Range["a2"].Activate();

                workbook.SaveAs(fileNameSave);
            }
        }

        private void CreateHyperlinks(IWorkbook workbook)
        {
            int countWorksheets = 19;
            
            int n = 1;
            IWorksheet last;
            IWorkbook current;

            for (int i = 1; i <= countWorksheets; i++)
            {
                if (i > 1)
                {
                    IHyperLink hyperlink = workbook.Worksheets[i.ToString()].HyperLinks.Add(workbook.Worksheets[i.ToString()].Range["A3"]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "Назад";
                    hyperlink.Address = workbook.Worksheets[(i - 1).ToString()].Range["A2"].Address;

                    if (i < countWorksheets)
                    {
                        if (i < 8)
                            hyperlink = workbook.Worksheets[i.ToString()].HyperLinks.Add(workbook.Worksheets[i.ToString()].Range["F3"]);
                        else
                        {
                            hyperlink = workbook.Worksheets[i.ToString()].HyperLinks.Add(workbook.Worksheets[i.ToString()].Range["E3"]);
                        }
                        hyperlink.Type = ExcelHyperLinkType.Workbook;
                        hyperlink.ScreenTip = "Вперед";
                        hyperlink.Address = workbook.Worksheets[(i + 1).ToString()].Range["A2"].Address;
                    }
                    else
                    {
                        workbook.Worksheets[i.ToString()].Range["E3"].Value = "";
                    }
                }
                else
                {
                    IHyperLink hyperlink = workbook.Worksheets[i.ToString()].HyperLinks.Add(workbook.Worksheets[i.ToString()].Range["A3"]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "Назад";
                    hyperlink.Address = workbook.Worksheets[0].Range["A2"].Address;

                    hyperlink = workbook.Worksheets[i.ToString()].HyperLinks.Add(workbook.Worksheets[i.ToString()].Range["F3"]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "Вперед";
                    hyperlink.Address = workbook.Worksheets[(i + 1).ToString()].Range["A2"].Address;
                }
            }
        }

        private void CreateYears(IWorkbook workbook, string plotAblative, List<WeatherYear> years)
        {
            IWorksheet ws = workbook.Worksheets["year"];
            ws.Range["A2"].Value = ws.Range["A2"].Value
                .Replace("[plot]", plotAblative);

            int r = 5;
            var list = from y in years
                orderby y.Year descending
                select y;

            int n = 1;
            foreach (var year in list)
            {
                ws.Range[r, 1].Number = n++;
                ws.Range[r, 1].NumberFormat = "0";
                ws.Range[r, 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, 1].BorderAround();

                ws.Range[r, 2].Number = year.Year;
                ws.Range[r, 2].NumberFormat = "0";
                ws.Range[r, 2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, 2].BorderAround();

                int c = 3;
                ws.Range[r, c].Value = year.Temperature.HasValue ? year.Temperature.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 4;
                ws.Range[r, c].Value = year.MinTemperature.HasValue ? year.MinTemperature.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 5;
                ws.Range[r, c].Value = year.MaxTemperature.HasValue ? year.MaxTemperature.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 6;
                ws.Range[r, c].Value = year.Rainfall.HasValue ? year.Rainfall.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 7;
                ws.Range[r, c].Value = year.SnowHight.HasValue ? year.SnowHight.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 8;
                ws.Range[r, c].Value = year.Humidity.HasValue ? year.Humidity.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                c = 9;
                ws.Range[r, c].Value = year.DewPoint.HasValue ? year.DewPoint.Value.ToString() : "";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].BorderAround();

                r++;
            }

            Color color = Color.LightGray;

            int k = list.Count();

            ws.Range[r, 1].BorderAround();
            ws.Range[r, 1].CellStyle.Color = color;
            ws.Range[r, 2].Value = "Средние";
            ws.Range[r, 2].BorderAround();
            ws.Range[r, 2].CellStyle.Color = color;
            for (int c = 3; c <= 9; c++)
            {
                ws.Range[r, c].Formula = $"=AVERAGE({ws.Range[6, c].Address}:{ws.Range[5 + k - 2, c].Address})";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].CellStyle.Color = color;
                ws.Range[r, c].BorderAround();
            }

            r++;
            ws.Range[r, 1].BorderAround();
            ws.Range[r, 1].CellStyle.Color = color;
            ws.Range[r, 2].Value = "Суммы";
            ws.Range[r, 2].CellStyle.Color = color;
            ws.Range[r, 2].BorderAround();
            for (int c = 3; c <= 9; c++)
            {
                ws.Range[r, c].Formula = $"=SUM({ws.Range[5, c].Address}:{ws.Range[5 + k - 2, c].Address})";
                if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                    ws.Range[r, c].NumberFormat = "0.0";
                ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ws.Range[r, c].CellStyle.Color = color;
                ws.Range[r, c].BorderAround();
            }

            ws.Name = "15";

            IHyperLink hyperlink = workbook.Worksheets[0].HyperLinks.Add(ws.Range[23, 6]);
            hyperlink.Type = ExcelHyperLinkType.Workbook;
            hyperlink.ScreenTip = "Перейти к результатам";
            hyperlink.Address = ws.Range["A2"].Address;

            hyperlink = ws.HyperLinks.Add(ws.Range["A3"]);
            hyperlink.Type = ExcelHyperLinkType.Workbook;
            hyperlink.ScreenTip = "К содержанию";
            hyperlink.Address = workbook.Worksheets[0].Range[23, 6].Address;
        }

        private void CreateMonths(IWorkbook workbook, string plotAblative, DataSet dsMonth)
        {
            int n = 8;
            int h = 15;
            foreach (DataTable table in dsMonth.Tables)
            {
                IWorksheet ws = workbook.Worksheets.AddCopyBefore(workbook.Worksheets["month"], workbook.Worksheets["year"]);
                ws.Range["A2"].Value = ws.Range["A2"].Value
                    .Replace("[param]", arrPrameters[n - 8])
                    .Replace("[plot]", plotAblative);

                int r = 5;
                DataView view = new DataView(table);
                view.Sort = "Year DESC";
                ws.ImportDataView(view, false, r, 2);
                ws.Name = n++.ToString();

                int k = 1;
                while (!string.IsNullOrWhiteSpace(ws.Range[r, 2].Value))
                {
                    ws.Range[r, 1].Number = k++;
                    ws.Range[r, 1].NumberFormat = "0";
                    ws.Range[r, 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    ws.Range[r, 1].BorderAround();

                    ws.Range[r, 2].NumberFormat = "0";
                    ws.Range[r, 2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    ws.Range[r, 2].BorderAround();

                    for (int c = 3; c <= 14; c++)
                    {
                        if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                            ws.Range[r, c].NumberFormat = "0.0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();
                    }
                    r++;
                }

                Color color = Color.LightGray;

                ws.Range[r, 1].BorderAround();
                ws.Range[r, 1].CellStyle.Color = color;
                ws.Range[r, 2].Value = "Средние";
                ws.Range[r, 2].BorderAround();
                ws.Range[r, 2].CellStyle.Color = color;
                for (int c = 3; c <= 14; c++)
                {
                    ws.Range[r, c].Formula = $"=AVERAGE({ws.Range[6, c].Address}:{ws.Range[5 + k - 2, c].Address})";
                    if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                        ws.Range[r, c].NumberFormat = "0.0";
                    ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    ws.Range[r, c].CellStyle.Color = color;
                    ws.Range[r, c].BorderAround();
                }

                r++;
                ws.Range[r, 1].BorderAround();
                ws.Range[r, 1].CellStyle.Color = color;
                ws.Range[r, 2].Value = "Суммы";
                ws.Range[r, 2].CellStyle.Color = color;
                ws.Range[r, 2].BorderAround();
                for (int c = 3; c <= 14; c++)
                {
                    ws.Range[r, c].Formula = $"=SUM({ws.Range[5, c].Address}:{ws.Range[5 + k - 2, c].Address})";
                    if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                        ws.Range[r, c].NumberFormat = "0.0";
                    ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    ws.Range[r, c].CellStyle.Color = color;
                    ws.Range[r, c].BorderAround();
                }

                IHyperLink hyperlink = workbook.Worksheets[0].HyperLinks.Add(ws.Range[h, 6]);
                hyperlink.Type = ExcelHyperLinkType.Workbook;
                hyperlink.ScreenTip = "Перейти к результатам";
                hyperlink.Address = ws.Range["A2"].Address;

                hyperlink = ws.HyperLinks.Add(ws.Range["A3"]);
                hyperlink.Type = ExcelHyperLinkType.Workbook;
                hyperlink.ScreenTip = "К содержанию";
                hyperlink.Address = workbook.Worksheets[0].Range[h, 6].Address;

                h++;
            }
            workbook.Worksheets["month"].Remove();
        }

        private void CreateDecades(IWorkbook workbook, string plotAblative, DataSet dsDecades)
        {
            int n = 1;
            int h = 7;

            int n2 = 16;
            int h2 = 25;

            IWorksheet ws = null;
            foreach (DataTable table in dsDecades.Tables)
            {
                bool isTransition = false;
                for (int i = 0; i < transiotionNames.Length; i++)
                {
                    if (table.TableName == transiotionNames[i])
                        isTransition = true;
                }

                if (isTransition)
                {
                    ws = workbook.Worksheets.AddCopyAfter(workbook.Worksheets["transtemp"], workbook.Worksheets[workbook.Worksheets.Count - 1]);
                    int tt = 0;
                    if (table.TableName == transiotionNames[0])
                    {
                        tt = 0;
                    }
                    if (table.TableName == transiotionNames[1])
                    {
                        tt = 5;
                    }
                    if (table.TableName == transiotionNames[2])
                    {
                        tt = 10;
                    }
                    if (table.TableName == transiotionNames[3])
                    {
                        tt = 15;
                    }

                    ws.Range["A2"].Value = ws.Range["A2"].Value
                        .Replace("[transtemp]", tt.ToString())
                        .Replace("[plot]", plotAblative);


                    int r = 5;
                    DataView view = new DataView(table);
                    view.Sort = "Year DESC";
                    ws.ImportDataView(view, false, r, 2);
                    ws.Name = n2++.ToString();

                    
                    int k = 1;

                    foreach (DataRow tableRow in view.Table.Rows)
                    {
                        int c = 1;

                        ws.Range[r, c].Number = k++;
                        ws.Range[r, c].NumberFormat = "0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();

                        c = 2;
                        ws.Range[r, c].NumberFormat = "0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();

                        c = 3;
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].NumberFormat = "dd.mm";
                        ws.Range[r, c].BorderAround();

                        c = 4;
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].NumberFormat = "dd.mm";
                        ws.Range[r, c].BorderAround();

                        c = 5;
                        ws.Range[r, c].NumberFormat = "0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();

                        c = 6;
                        ws.Range[r, c].NumberFormat = "0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();

                        c = 7;
                        ws.Range[r, c].NumberFormat = "0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].BorderAround();

                        r++;
                    }

                    Color color = Color.LightGray;

                    ws.Range[r, 1].BorderAround();
                    ws.Range[r, 1].CellStyle.Color = color;
                    ws.Range[r, 2].Value = "Средние";
                    ws.Range[r, 2].BorderAround();
                    ws.Range[r, 2].CellStyle.Color = color;
                    for (int c = 3; c <= 7; c++)
                    {
                        ws.Range[r, c].Formula = $"=AVERAGE({ws.Range[6, c].Address}:{ws.Range[5 + k - 2, c].Address})";
                        if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                        {
                            if (c <= 4)
                            {
                                ws.Range[r, c].NumberFormat = "dd.mm";
                            }
                            else
                            {
                                ws.Range[r, c].NumberFormat = "0";
                            }
                        }
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].CellStyle.Color = color;
                        ws.Range[r, c].BorderAround();
                    }

                    int rowAvr = r;
                    r = 5;

                    ws.Range["I4"].Value = ws.Range["I4"].Value.Replace("[temp]", tt.ToString());

                    ws.Range["I5"].Value = ws.Range[r, 3].Value;
                    ws.Range["I6"].Value = ws.Range[rowAvr, 3].Value;

                    ws.Range["K5"].Value = ws.Range[r, 5].Value;
                    ws.Range["K6"].Value = ws.Range[rowAvr, 5].Value;

                    ws.Range["M5"].Value = ws.Range[r, 6].Value;
                    ws.Range["M6"].Value = ws.Range[rowAvr, 6].Value;

                    ws.Range["O5"].Value = ws.Range[r, 7].Value;
                    ws.Range["O6"].Value = ws.Range[rowAvr, 7].Value;

                    ws.Range["P5"].Value = ws.Range[r, 4].Value;
                    ws.Range["P6"].Value = ws.Range[rowAvr, 4].Value;

                    IHyperLink hyperlink = workbook.Worksheets[0].HyperLinks.Add(ws.Range[h2, 6]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "Перейти к результатам";
                    hyperlink.Address = ws.Range["A2"].Address;

                    hyperlink = ws.HyperLinks.Add(ws.Range["A3"]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "К содержанию";
                    hyperlink.Address = workbook.Worksheets[0].Range[h2, 6].Address;

                    h2++;
                }
                else
                {
                    ws = workbook.Worksheets.AddCopyBefore(workbook.Worksheets["decade"], workbook.Worksheets["month"]);
                    ws.Range["A2"].Value = ws.Range["A2"].Value
                        .Replace("[param]", arrPrameters[n - 1])
                        .Replace("[plot]", plotAblative);

                    int r = 6;
                    DataView view = new DataView(table);
                    view.Sort = "Year DESC";
                    ws.ImportDataView(view, false, r, 2);
                    ws.Name = n++.ToString();

                    int k = 1;
                    while (!string.IsNullOrWhiteSpace(ws.Range[r, 2].Value))
                    {
                        ws.Range[r, 1].Number = k++;
                        ws.Range[r, 1].NumberFormat = "0";
                        ws.Range[r, 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, 1].BorderAround();

                        ws.Range[r, 2].NumberFormat = "0";
                        ws.Range[r, 2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, 2].BorderAround();

                        for (int c = 3; c <= 38; c++)
                        {
                            if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                                ws.Range[r, c].NumberFormat = "0.0";
                            ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                            ws.Range[r, c].BorderAround();
                        }

                        r++;
                    }

                    Color color = Color.LightGray;

                    ws.Range[r, 1].BorderAround();
                    ws.Range[r, 1].CellStyle.Color = color;
                    ws.Range[r, 2].Value = "Средние";
                    ws.Range[r, 2].BorderAround();
                    ws.Range[r, 2].CellStyle.Color = color;
                    for (int c = 3; c <= 38; c++)
                    {
                        ws.Range[r, c].Formula = $"=AVERAGE({ws.Range[7, c].Address}:{ws.Range[6 + k - 2, c].Address})";
                        if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                            ws.Range[r, c].NumberFormat = "0.0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].CellStyle.Color = color;
                        ws.Range[r, c].BorderAround();
                    }

                    r++;
                    ws.Range[r, 1].BorderAround();
                    ws.Range[r, 1].CellStyle.Color = color;
                    ws.Range[r, 2].Value = "Суммы";
                    ws.Range[r, 2].CellStyle.Color = color;
                    ws.Range[r, 2].BorderAround();
                    for (int c = 3; c <= 38; c++)
                    {
                        ws.Range[r, c].Formula = $"=SUM({ws.Range[6, c].Address}:{ws.Range[6 + k - 2, c].Address})";
                        if (!string.IsNullOrWhiteSpace(ws.Range[r, c].Value))
                            ws.Range[r, c].NumberFormat = "0.0";
                        ws.Range[r, c].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                        ws.Range[r, c].CellStyle.Color = color;
                        ws.Range[r, c].BorderAround();
                    }

                    IHyperLink hyperlink = workbook.Worksheets[0].HyperLinks.Add(ws.Range[h, 6]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "Перейти к результатам";
                    hyperlink.Address = ws.Range["A2"].Address;

                    hyperlink = ws.HyperLinks.Add(ws.Range["A3"]);
                    hyperlink.Type = ExcelHyperLinkType.Workbook;
                    hyperlink.ScreenTip = "К содержанию";
                    hyperlink.Address = workbook.Worksheets[0].Range[h, 6].Address;

                    h++;
                }

                
            }

            workbook.Worksheets["decade"].Remove();
            workbook.Worksheets["transtemp"].Remove();
        }

        private void CreateTitul(IWorkbook workbook, string plotAblative)
        {
            IWorksheet ws = workbook.Worksheets["title"];
            ws.Range["A2"].Value = ws.Range["A2"].Value
                .Replace("[plot]", plotAblative);
            ws.Name = "Титул";
        }
    }
}
