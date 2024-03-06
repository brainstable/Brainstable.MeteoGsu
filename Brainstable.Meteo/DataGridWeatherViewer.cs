using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brainstable.Meteo
{
    public partial class DataGridWeatherViewer : UserControl
    {
        private DataTable table;
        private DataRow dataRow;
        private DataView view;
        private IChartData chartDataRow;
        private int rowIndex = -1;
        private int colIndex = -1;
        private IChartData chartDataColumn;

        public DataRow SelectedDataRow
        {
            get { return dataRow; }
            set { dataRow = value; }
        }

        public int SelectedColumnIndex => colIndex;

        public int SelectedRowIndex => rowIndex;

        public IChartData ChartDataRow 
        {
            get => chartDataRow;
            set => chartDataRow = value;
        }

        public IChartData ChartDataColumn
        {
            get => chartDataColumn;
            set => chartDataColumn = value;
        }

        public DataTable Table => table;
        public DataView View => view;

        public event EventHandler SelectedChangeRow;
        public event EventHandler SelectedChangeColumn;

        public DataGridWeatherViewer()
        {
            InitializeComponent();
            InitialDataGridView();
            dataGridView1.RowPrePaint += dataGridView1_RowPrePaint;
            dataGridView1.ColumnAdded += dataGridView1_ColumnAdded;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            dataGridView1.RowEnter += DataGridView1_RowEnter;
            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (rowIndex != e.RowIndex)
            {
                rowIndex = e.RowIndex;
                dataRow = view[e.RowIndex].Row;
                chartDataRow = new ChartDataRow(dataRow);
                OnSelectedChange();
            }

            
        }

        public void BindingTableSource(DataTable table)
        {
            rowIndex = -1;
            colIndex = -1;
            this.table = table;
            view = new DataView(table);
            view.Sort = "Year DESC";
            dataGridView1.DataSource = view;
            dataGridView1.Columns[0].HeaderText = "Год";

        }

       
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            object headValue = ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value;
            if (headValue == null || !headValue.Equals((e.RowIndex + 1).ToString()))
            {
                ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.HeaderCell.Style.Font = new Font("Times New Roman", 9, FontStyle.Bold);
            e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            e.Column.DefaultCellStyle.Font = new Font("Times New Roman", 9, FontStyle.Regular);
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;

            if (e.Column.Index == 0)
            {
                e.Column.Width = 50;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.Frozen = true;
            }
            else
            {
                e.Column.Width = 50;
                e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.Column.DefaultCellStyle.Format = "F1";
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.Beige;
            }
            
            if (e.ColumnIndex > 0)
            {
                if (e.Value.ToString() == "" || e.Value == null)
                {
                    e.Value = "";
                }

                double d;
                if (Double.TryParse(e.Value.ToString(), out d))
                {
                    if (d > 300)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                        
                    }
                }

                
            }
        }

       

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dataRow = view[e.RowIndex].Row;
            //OnSelectedChange();
            if (colIndex != e.ColumnIndex)
            {
                colIndex = e.ColumnIndex;
                if (colIndex < 1)
                {
                    chartDataColumn = null;
                }
                else
                {
                    chartDataColumn = new ChartDataColumn(colIndex, Table);
                }
                OnSelectedChangeColumn();
            }
        }

      

        private void InitialDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowDrop = false;

            dataGridView1.ReadOnly = true;

            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.Dock = DockStyle.Fill;
            //dataGridView1.MultiSelect = false;
            
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.TopLeftHeaderCell.Value = "№";
            dataGridView1.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.TopLeftHeaderCell.Style.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Bold);
            dataGridView1.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Regular);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
        }

        protected virtual void OnSelectedChange()
        {
            SelectedChangeRow?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSelectedChangeColumn()
        {
            SelectedChangeColumn?.Invoke(this, EventArgs.Empty);
        }
    }
}
