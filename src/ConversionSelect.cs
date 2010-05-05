using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace VS_SQL_TextSchemeMigrator
{
    public partial class ConversionSelect : Form
    {
        public ConversionSelect()
        {
            InitializeComponent();
        }

        private void On_optVStoSql_CheckedChanged(object sender, EventArgs e)
        {
            if (optVStoSql.Checked)
                LoadVStoSqlComboBoxes();
        }

        private void On_optSqlToSql_CheckedChanged(object sender, EventArgs e)
        {
            if (optSqlToSql.Checked)
                LoadSqlToSqlComboBoxes();
        }

        private void ResetComboBoxes()
        {
            cbxFrom.Items.Clear();
            cbxTo.Items.Clear();
        }

        private void LoadVStoSqlComboBoxes()
        {
            string[] vsVersions = Enum.GetNames(typeof(VisualStudioVersion));
            string[] sqlVersions = Enum.GetNames(typeof(SqlStudioVersion));

            ResetComboBoxes();
            cbxFrom.Items.AddRange(vsVersions);
            cbxTo.Items.AddRange(sqlVersions);
        }

        private void LoadSqlToSqlComboBoxes()
        { 
            string[] sqlVersions = Enum.GetNames(typeof(SqlStudioVersion));

            ResetComboBoxes();
            cbxFrom.Items.AddRange(sqlVersions);
            cbxTo.Items.AddRange(sqlVersions);
        }

        private void On_btnCopySettings_Click(object sender, EventArgs e)
        {
            if (!IsDataValid())
                return;

            if (optSqlToSql.Checked)
            {
                SqlStudioVersion source = (SqlStudioVersion)Enum.Parse(typeof(SqlStudioVersion), cbxFrom.SelectedItem.ToString());
                SqlStudioVersion destination = (SqlStudioVersion)Enum.Parse(typeof(SqlStudioVersion), cbxTo.SelectedItem.ToString());
                StartImportProcess(source, destination);
            }

            if (optVStoSql.Checked)
            {
                VisualStudioVersion source = (VisualStudioVersion)Enum.Parse(typeof(VisualStudioVersion), cbxFrom.SelectedItem.ToString());
                SqlStudioVersion destination = (SqlStudioVersion)Enum.Parse(typeof(SqlStudioVersion), cbxTo.SelectedItem.ToString());
                StartImportProcess(source, destination);
            }
        }

        private bool IsDataValid()
        {
            bool returnValue = true;

            if (!optSqlToSql.Checked && !optVStoSql.Checked)
            {
                MessageBox.Show("Please select the type of settings you want to copy.");
                returnValue = false;
            }

            if (cbxFrom.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the source of the settings you want to copy.");
                cbxFrom.Focus();
                returnValue = false;
            }

            if (cbxTo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the destination of the settings you want to copy.");
                cbxTo.Focus();
                returnValue = false;
            }

            return returnValue;
        }

        private void StartImportProcess(VisualStudioVersion vsVersion, SqlStudioVersion sqlVersion)
        {
            lblProcessingStatus.Visible = true;
            this.Enabled = false;
            var importer = new ThemeImporter();
            importer.CopyVSToSql(vsVersion, sqlVersion);
            lblProcessingStatus.Visible = false;
            this.Enabled = true;
            MessageBox.Show("The process is done.");
        }

        private void StartImportProcess(SqlStudioVersion sqlSource, SqlStudioVersion sqlDestination)
        {
            lblProcessingStatus.Visible = true;
            this.Enabled = false;
            var importer = new ThemeImporter();
            importer.CopySqlToSql(sqlSource, sqlDestination);
            lblProcessingStatus.Visible = false;
            this.Enabled = true;
            MessageBox.Show("The process is done.");
        }
    }
}
