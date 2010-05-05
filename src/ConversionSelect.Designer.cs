namespace VS_SQL_TextSchemeMigrator
{
    partial class ConversionSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.optSqlToSql = new System.Windows.Forms.RadioButton();
            this.optVStoSql = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopySettings = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblProcessingStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // optSqlToSql
            // 
            this.optSqlToSql.AutoSize = true;
            this.optSqlToSql.Location = new System.Drawing.Point(12, 35);
            this.optSqlToSql.Name = "optSqlToSql";
            this.optSqlToSql.Size = new System.Drawing.Size(358, 17);
            this.optSqlToSql.TabIndex = 1;
            this.optSqlToSql.TabStop = true;
            this.optSqlToSql.Text = "Copy between two different versions of Sql Server Management Studio";
            this.optSqlToSql.UseVisualStyleBackColor = true;
            this.optSqlToSql.CheckedChanged += new System.EventHandler(this.On_optSqlToSql_CheckedChanged);
            // 
            // optVStoSql
            // 
            this.optVStoSql.AutoSize = true;
            this.optVStoSql.Location = new System.Drawing.Point(12, 12);
            this.optVStoSql.Name = "optVStoSql";
            this.optVStoSql.Size = new System.Drawing.Size(298, 17);
            this.optVStoSql.TabIndex = 0;
            this.optVStoSql.TabStop = true;
            this.optVStoSql.Text = "Copy from Visual Studio to Sql Server Management Studio";
            this.optVStoSql.UseVisualStyleBackColor = true;
            this.optVStoSql.CheckedChanged += new System.EventHandler(this.On_optVStoSql_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(12, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 72);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Version";
            // 
            // cbxTo
            // 
            this.cbxTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTo.FormattingEnabled = true;
            this.cbxTo.Location = new System.Drawing.Point(45, 42);
            this.cbxTo.Name = "cbxTo";
            this.cbxTo.Size = new System.Drawing.Size(144, 21);
            this.cbxTo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "To:";
            // 
            // cbxFrom
            // 
            this.cbxFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFrom.FormattingEnabled = true;
            this.cbxFrom.Location = new System.Drawing.Point(45, 13);
            this.cbxFrom.Name = "cbxFrom";
            this.cbxFrom.Size = new System.Drawing.Size(144, 21);
            this.cbxFrom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "From:";
            // 
            // btnCopySettings
            // 
            this.btnCopySettings.Location = new System.Drawing.Point(248, 78);
            this.btnCopySettings.Name = "btnCopySettings";
            this.btnCopySettings.Size = new System.Drawing.Size(91, 23);
            this.btnCopySettings.TabIndex = 4;
            this.btnCopySettings.Text = "Copy Settings";
            this.btnCopySettings.UseVisualStyleBackColor = true;
            this.btnCopySettings.Click += new System.EventHandler(this.On_btnCopySettings_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(248, 107);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.On_btnClose_Click);
            // 
            // lblProcessingStatus
            // 
            this.lblProcessingStatus.AutoSize = true;
            this.lblProcessingStatus.Location = new System.Drawing.Point(222, 137);
            this.lblProcessingStatus.Name = "lblProcessingStatus";
            this.lblProcessingStatus.Size = new System.Drawing.Size(121, 13);
            this.lblProcessingStatus.TabIndex = 5;
            this.lblProcessingStatus.Text = "Processing, please wait.";
            this.lblProcessingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProcessingStatus.Visible = false;
            // 
            // ConversionSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 157);
            this.Controls.Add(this.lblProcessingStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopySettings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.optVStoSql);
            this.Controls.Add(this.optSqlToSql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConversionSelect";
            this.Text = "ConversionSelect";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optSqlToSql;
        private System.Windows.Forms.RadioButton optVStoSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopySettings;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblProcessingStatus;

    }
}