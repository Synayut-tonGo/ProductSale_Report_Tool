using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using C_Forintern.Dto;
using C_Forintern.Repository;
using C_Forintern.Report;
using DevExpress.XtraReports.UI;

namespace C_Forintern
{
    public partial class Form1 : Form
    {
        private readonly SaleRepository _saleRepo = new SaleRepository();

        public Form1()
        {
            InitializeComponent();

            // Optional: you can attach the Form Load event here too (if not using designer)
            // this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Optional: auto-load last 7 days of data
            // var sales = _saleRepo.GetSales(DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var sales = _saleRepo.GetSales(dtpStart.Value, dtpEnd.Value, txtProductName.Text);

            if (!sales.Any())
            {
                MessageBox.Show("No sales found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var report = new SalesReport();
            report.DataSource = sales;

            if (report.Parameters["startDate"] != null)
                report.Parameters["startDate"].Value = dtpStart.Value;

            if (report.Parameters["endDate"] != null)
                report.Parameters["endDate"].Value = dtpEnd.Value;

            new ReportPrintTool(report).ShowPreview();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            var sales = _saleRepo.GetSales(dtpStart.Value, dtpEnd.Value, txtProductName.Text);

            if (!sales.Any())
            {
                MessageBox.Show("No sales to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var report = new SalesReport();
            report.DataSource = sales;
            report.ExportToPdf("SalesReport.pdf");
            MessageBox.Show("Exported to SalesReport.pdf");
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
