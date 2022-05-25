using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace appLeerExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = dgvCeldas.DataSource as DataView;
                if (dv != null)
                { dv.RowFilter = txtFiltrar.Text; }
            }
            catch (Exception ex)
            { }
        }
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Woorkbook |*.xlsx;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = new DataTable();
                XLWorkbook wb = new XLWorkbook(openFileDialog.FileName);
                bool isFirstRow = true;
                var rows = wb.Worksheet(1).RowsUsed();
                foreach (var row in rows)
                {
                    if (isFirstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                            dataTable.Columns.Add(cell.Value.ToString());
                        isFirstRow = false;
                    }
                    else
                    {
                        dataTable.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        { dataTable.Rows[dataTable.Rows.Count - 1][i++] = cell.Value.ToString(); }
                    }
                }
                dgvCeldas.DataSource = dataTable.DefaultView;
            }
        }
        private void txtFiltrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { btnFiltrar.PerformClick(); }
        }
    }
}
