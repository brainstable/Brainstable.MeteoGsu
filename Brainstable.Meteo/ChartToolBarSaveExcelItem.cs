using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Brainstable.Meteo.Properties;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.XlsIO;

namespace Brainstable.Meteo
{
    public sealed class ChartToolBarSaveExcelItem : ChartToolBarItem
    {
        private Image save_Image;


        public ChartToolBarSaveExcelItem()
        {
            save_Image = Resources.Save_Excel;

        }

        public override Image Image
        {
            get { return DefaultImage; }
        }

        protected override Image DefaultImage
        {
            get
            {
                return save_Image;
            }
        }

        protected override string DefaultToolTip
        {
            get
            {
                return "Сохранить в EXCEL";
            }
        }

        protected override string DefaultName
        {
            get
            {
                return "SaveExcelItem";
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool Checked
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override bool IsCheckable
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool Equals(object obj)
        {
            return this.DefaultName == ((ChartToolBarItemBase)obj).Name;
        }

        protected override void OnClick()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Книга Excel|*.xls";
            saveFileDialog.FileName = this.Chart.Title.Text;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Excel97to2003;
                    IWorkbook workbook = application.Workbooks.Create(1);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    worksheet.Range["A1"].Value = Chart.Title.Text;
                    worksheet.Range["A2"].Value = Chart.PrimaryXAxis.Title;
                    worksheet.Range["B2"].Value = Chart.PrimaryYAxis.Title;
                    for (int i = 0; i < Chart.Series[0].Points.Count; i++)
                    {
                        worksheet.Range[i + 3, 1].Value = Chart.Series[0].Points[i].Category;
                        worksheet.Range[i + 3, 2].Number = Chart.Series[0].Points[i].YValues[0];
                        worksheet.Range[i + 3, 2].NumberFormat = "0.0";
                    }

                    IChartShape chart = workbook.Worksheets[0].Charts.Add();


                    workbook.SaveAs(saveFileDialog.FileName);
                }


                System.Diagnostics.Process.Start(saveFileDialog.FileName);

            }


            base.OnClick();
        }
    }
}