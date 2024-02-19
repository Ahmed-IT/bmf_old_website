using ClosedXML.Excel;
using System.Data;
using System.Windows.Forms;

namespace BMF_LicenseApp.Models
{
    public class ExortToExcel
    {
        public static void ExportDataGridViewToExcel(DataGridView dgv)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save as Excel File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    }

                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;

                        }
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                }
                MessageBox.Show("Export Successful", "Export");
            }
        }
    }
}
