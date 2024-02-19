using BMF_LicenseApp.Interfaces;
using BMF_LicenseApp.Models;
using BMF_LicenseApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMF_LicenseApp
{
    public partial class Form1 : Form
    {
        private readonly ILicenseService _licenseService;
        private readonly HttpClient _httpClient;

        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _licenseService = new LicenseService(_httpClient);
            LoadLicenseTypesToComboBox();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var licensesType = await _licenseService.GetLicensesTypeAsync();
            
        }

        private async void btnLicenseType_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(comboLincense1.SelectedValue);

                var licenses = await _licenseService.GetLicensesListAsync(userId);

                var final = licenses.Select(a => new
                {
                    a.LicenseType.Type,
                    a.LicenseType.Title,
                    a.LicenseNumber,
                    ExpireDate = a.ExpireDate.ToString("dd MMM yyyy"),
                    a.Profile.CPR,
                    a.Profile.FullName,
                    DateOfBirth = a.Profile.DateOfBirth.ToString("dd MMM yyyy"),
                    a.Profile.PhotoURL
                }).ToList();

                dataGridView1.DataSource = final;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Loading Data: {ex.Message}", "Error");
            }
        }

        private async void btnByCPR_Click(object sender, EventArgs e)
        {
            try
            {
                string cpr = txtCPR.Text;

                var licenses = await _licenseService.GetLicensesByCPRAsync(cpr);

                var final = licenses.Select(a => new
                {
                    a.LicenseType.Type,
                    a.LicenseType.Title,
                    a.LicenseNumber,
                    ExpireDate = a.ExpireDate.ToString("dd MMM yyyy"),
                    a.Profile.CPR,
                    a.Profile.FullName,
                    DateOfBirth = a.Profile.DateOfBirth.ToString("dd MMM yyyy"),
                    a.Profile.PhotoURL
                }).ToList();

                dataGridView1.DataSource = final;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Loading Data: {ex.Message}", "Error");
            }
        }

        private async void btnExpiredList_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(comboLicense2.SelectedValue);

                var licenses = await _licenseService.GetLicensesListAsync(userId);

                var final = licenses.Select(a => new
                {
                    a.LicenseType.Type,
                    a.LicenseType.Title,
                    a.LicenseNumber,
                    ExpireDate = a.ExpireDate.ToString("dd MMM yyyy"),
                    a.Profile.CPR,
                    a.Profile.FullName,
                    DateOfBirth = a.Profile.DateOfBirth.ToString("dd MMM yyyy"),
                    a.Profile.PhotoURL
                }).ToList();

                dataGridView1.DataSource = final;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Loading Data: {ex.Message}", "Error");
            }
        }

        private async void LoadLicenseTypesToComboBox()
        {
            try
            {
                var licenseTypes = await _licenseService.GetLicensesTypeAsync();

                // Assuming comboBoxLicenseType is your ComboBox
                comboLincense1.DataSource = licenseTypes;
                comboLincense1.DisplayMember = "Type"; // The property you want to display
                comboLincense1.ValueMember = "ID"; // The property to use as the actual value


                // Assuming comboBoxLicenseType is your ComboBox
                comboLicense2.DataSource = licenseTypes;
                comboLicense2.DisplayMember = "Type"; // The property you want to display
                comboLicense2.ValueMember = "ID"; // The property to use as the actual value
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as by displaying a message to the user
                MessageBox.Show($"Failed to load license types: {ex.Message}","Error");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ExortToExcel.ExportDataGridViewToExcel(dataGridView1);
            }
            else
            {
                MessageBox.Show("No Data Available","Error");
            }
        }
    }
}
