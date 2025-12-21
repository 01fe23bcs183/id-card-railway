using System;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;

namespace RailwayIDCardMaker.Forms
{
    public partial class SettingsForm : Form
    {
        private CardSettings _settings;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadZones();
            LoadPrinters();
            LoadSettings();
        }

        private void LoadZones()
        {
            cmbDefaultZone.Items.Clear();
            var zones = DatabaseService.GetAllZones();
            foreach (var zone in zones)
            {
                cmbDefaultZone.Items.Add(zone);
            }
        }

        private void LoadPrinters()
        {
            cmbPrinter.Items.Clear();
            foreach (string printer in PrintService.GetPrinters())
            {
                cmbPrinter.Items.Add(printer);
            }
        }

        private void LoadSettings()
        {
            _settings = DatabaseService.GetSettings();

            txtIssuingAuthority.Text = _settings.DefaultIssuingAuthority;
            txtAuthorityDesignation.Text = _settings.DefaultIssuingAuthorityDesignation;
            numValidityYears.Value = _settings.DefaultValidityYears;
            txtDefaultUnit.Text = _settings.DefaultUnitCode;
            chkPrintBothSides.Checked = _settings.PrintFrontAndBack;

            // Select default zone
            for (int i = 0; i < cmbDefaultZone.Items.Count; i++)
            {
                var zone = cmbDefaultZone.Items[i] as Zone;
                if (zone != null && zone.Code == _settings.DefaultZoneCode)
                {
                    cmbDefaultZone.SelectedIndex = i;
                    break;
                }
            }

            // Select default printer
            for (int i = 0; i < cmbPrinter.Items.Count; i++)
            {
                if (cmbPrinter.Items[i].ToString() == _settings.DefaultPrinterName)
                {
                    cmbPrinter.SelectedIndex = i;
                    break;
                }
            }

            lblLastSerial.Text = $"Last Serial: {_settings.LastSerialNumber}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.DefaultIssuingAuthority = txtIssuingAuthority.Text.Trim();
            _settings.DefaultIssuingAuthorityDesignation = txtAuthorityDesignation.Text.Trim();
            _settings.DefaultValidityYears = (int)numValidityYears.Value;
            _settings.DefaultUnitCode = txtDefaultUnit.Text.Trim();
            _settings.PrintFrontAndBack = chkPrintBothSides.Checked;

            var selectedZone = cmbDefaultZone.SelectedItem as Zone;
            if (selectedZone != null)
            {
                _settings.DefaultZoneCode = selectedZone.Code;
                _settings.DefaultZoneName = selectedZone.Abbreviation;
            }

            if (cmbPrinter.SelectedIndex >= 0)
            {
                _settings.DefaultPrinterName = cmbPrinter.SelectedItem.ToString();
            }

            DatabaseService.SaveSettings(_settings);

            MessageBox.Show("Settings saved successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
