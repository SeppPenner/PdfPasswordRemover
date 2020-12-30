namespace PdfPasswordRemover
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonRemovePassword = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelSelectedLanguage = new System.Windows.Forms.Label();
            this.comboBoxSelectLanguage = new System.Windows.Forms.ComboBox();
            this.buttonShowPassword = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelTopLeft = new System.Windows.Forms.TableLayoutPanel();
            this.labelTypePassword = new System.Windows.Forms.Label();
            this.tableLayoutPanelTopRight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelBottomLeft = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelTopLeft.SuspendLayout();
            this.tableLayoutPanelTopRight.SuspendLayout();
            this.tableLayoutPanelBottomLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRemovePassword
            // 
            this.buttonRemovePassword.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonRemovePassword.Location = new System.Drawing.Point(3, 108);
            this.buttonRemovePassword.Name = "buttonRemovePassword";
            this.buttonRemovePassword.Size = new System.Drawing.Size(200, 24);
            this.buttonRemovePassword.TabIndex = 0;
            this.buttonRemovePassword.UseVisualStyleBackColor = true;
            this.buttonRemovePassword.Click += new System.EventHandler(this.TryLoadPdfFile);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Location = new System.Drawing.Point(2, 32);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(299, 20);
            this.textBoxPassword.TabIndex = 1;
            // 
            // labelSelectedLanguage
            // 
            this.labelSelectedLanguage.AutoSize = true;
            this.labelSelectedLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSelectedLanguage.Location = new System.Drawing.Point(3, 0);
            this.labelSelectedLanguage.Name = "labelSelectedLanguage";
            this.labelSelectedLanguage.Size = new System.Drawing.Size(296, 30);
            this.labelSelectedLanguage.TabIndex = 2;
            this.labelSelectedLanguage.Text = "Select language:";
            // 
            // comboBoxSelectLanguage
            // 
            this.comboBoxSelectLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSelectLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectLanguage.FormattingEnabled = true;
            this.comboBoxSelectLanguage.Location = new System.Drawing.Point(3, 33);
            this.comboBoxSelectLanguage.Name = "comboBoxSelectLanguage";
            this.comboBoxSelectLanguage.Size = new System.Drawing.Size(296, 21);
            this.comboBoxSelectLanguage.TabIndex = 3;
            this.comboBoxSelectLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectLanguageSelectedIndexChanged);
            // 
            // buttonShowPassword
            // 
            this.buttonShowPassword.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonShowPassword.Location = new System.Drawing.Point(3, 63);
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.Size = new System.Drawing.Size(200, 24);
            this.buttonShowPassword.TabIndex = 4;
            this.buttonShowPassword.UseVisualStyleBackColor = true;
            this.buttonShowPassword.Click += new System.EventHandler(this.ButtonShowPasswordClick);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelTopLeft, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelTopRight, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelBottomLeft, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(617, 282);
            this.tableLayoutPanelMain.TabIndex = 5;
            // 
            // tableLayoutPanelTopLeft
            // 
            this.tableLayoutPanelTopLeft.ColumnCount = 1;
            this.tableLayoutPanelTopLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTopLeft.Controls.Add(this.labelSelectedLanguage, 0, 0);
            this.tableLayoutPanelTopLeft.Controls.Add(this.comboBoxSelectLanguage, 0, 1);
            this.tableLayoutPanelTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTopLeft.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTopLeft.Name = "tableLayoutPanelTopLeft";
            this.tableLayoutPanelTopLeft.RowCount = 3;
            this.tableLayoutPanelTopLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTopLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTopLeft.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTopLeft.Size = new System.Drawing.Size(302, 135);
            this.tableLayoutPanelTopLeft.TabIndex = 6;
            // 
            // labelTypePassword
            // 
            this.labelTypePassword.AutoSize = true;
            this.labelTypePassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTypePassword.Location = new System.Drawing.Point(3, 0);
            this.labelTypePassword.Name = "labelTypePassword";
            this.labelTypePassword.Size = new System.Drawing.Size(297, 30);
            this.labelTypePassword.TabIndex = 6;
            this.labelTypePassword.Text = "Type in password:";
            // 
            // tableLayoutPanelTopRight
            // 
            this.tableLayoutPanelTopRight.ColumnCount = 1;
            this.tableLayoutPanelTopRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTopRight.Controls.Add(this.labelTypePassword, 0, 0);
            this.tableLayoutPanelTopRight.Controls.Add(this.buttonShowPassword, 0, 2);
            this.tableLayoutPanelTopRight.Controls.Add(this.textBoxPassword, 0, 1);
            this.tableLayoutPanelTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTopRight.Location = new System.Drawing.Point(311, 3);
            this.tableLayoutPanelTopRight.Name = "tableLayoutPanelTopRight";
            this.tableLayoutPanelTopRight.RowCount = 4;
            this.tableLayoutPanelTopRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTopRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTopRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTopRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTopRight.Size = new System.Drawing.Size(303, 135);
            this.tableLayoutPanelTopRight.TabIndex = 7;
            // 
            // tableLayoutPanelBottomLeft
            // 
            this.tableLayoutPanelBottomLeft.ColumnCount = 1;
            this.tableLayoutPanelBottomLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBottomLeft.Controls.Add(this.buttonRemovePassword, 0, 1);
            this.tableLayoutPanelBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBottomLeft.Location = new System.Drawing.Point(3, 144);
            this.tableLayoutPanelBottomLeft.Name = "tableLayoutPanelBottomLeft";
            this.tableLayoutPanelBottomLeft.RowCount = 2;
            this.tableLayoutPanelBottomLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBottomLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelBottomLeft.Size = new System.Drawing.Size(302, 135);
            this.tableLayoutPanelBottomLeft.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 282);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelTopLeft.ResumeLayout(false);
            this.tableLayoutPanelTopLeft.PerformLayout();
            this.tableLayoutPanelTopRight.ResumeLayout(false);
            this.tableLayoutPanelTopRight.PerformLayout();
            this.tableLayoutPanelBottomLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRemovePassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelSelectedLanguage;
        private System.Windows.Forms.ComboBox comboBoxSelectLanguage;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTopLeft;
        private System.Windows.Forms.Label labelTypePassword;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTopRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottomLeft;
    }
}

