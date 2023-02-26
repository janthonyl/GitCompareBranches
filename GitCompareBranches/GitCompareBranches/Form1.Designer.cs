namespace GitCompareBranches
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgBranch2 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgBranch1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearFolders = new System.Windows.Forms.Button();
            this.btnFoldersToInclude = new System.Windows.Forms.Button();
            this.cboFoldersToInclude = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboBranches2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBranches1 = new System.Windows.Forms.ComboBox();
            this.btnRepo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRepo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1425, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightSalmon;
            this.tabPage1.Controls.Add(this.dgBranch2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.dgBranch1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1417, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // dgBranch2
            // 
            this.dgBranch2.AllowUserToAddRows = false;
            this.dgBranch2.AllowUserToDeleteRows = false;
            this.dgBranch2.AllowUserToResizeRows = false;
            this.dgBranch2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBranch2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgBranch2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBranch2.Location = new System.Drawing.Point(675, 124);
            this.dgBranch2.Name = "dgBranch2";
            this.dgBranch2.ReadOnly = true;
            this.dgBranch2.RowTemplate.Height = 25;
            this.dgBranch2.Size = new System.Drawing.Size(736, 264);
            this.dgBranch2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1235, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "In Branch-2 But Missing From 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "In Branch-1 But Missing From 2";
            // 
            // dgBranch1
            // 
            this.dgBranch1.AllowUserToAddRows = false;
            this.dgBranch1.AllowUserToDeleteRows = false;
            this.dgBranch1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgBranch1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgBranch1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBranch1.Location = new System.Drawing.Point(6, 124);
            this.dgBranch1.Name = "dgBranch1";
            this.dgBranch1.ReadOnly = true;
            this.dgBranch1.RowTemplate.Height = 25;
            this.dgBranch1.Size = new System.Drawing.Size(666, 264);
            this.dgBranch1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.btnClearFolders);
            this.panel1.Controls.Add(this.btnFoldersToInclude);
            this.panel1.Controls.Add(this.cboFoldersToInclude);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cboBranches2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboBranches1);
            this.panel1.Controls.Add(this.btnRepo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtRepo);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1406, 97);
            this.panel1.TabIndex = 3;
            // 
            // btnClearFolders
            // 
            this.btnClearFolders.AutoSize = true;
            this.btnClearFolders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearFolders.Location = new System.Drawing.Point(1336, 64);
            this.btnClearFolders.Name = "btnClearFolders";
            this.btnClearFolders.Size = new System.Drawing.Size(44, 25);
            this.btnClearFolders.TabIndex = 11;
            this.btnClearFolders.Text = "Clear";
            this.btnClearFolders.UseVisualStyleBackColor = true;
            this.btnClearFolders.Click += new System.EventHandler(this.btnClearFolders_Click);
            // 
            // btnFoldersToInclude
            // 
            this.btnFoldersToInclude.AutoSize = true;
            this.btnFoldersToInclude.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFoldersToInclude.Location = new System.Drawing.Point(3, 62);
            this.btnFoldersToInclude.Name = "btnFoldersToInclude";
            this.btnFoldersToInclude.Size = new System.Drawing.Size(112, 25);
            this.btnFoldersToInclude.TabIndex = 10;
            this.btnFoldersToInclude.Text = "Folders To Include";
            this.btnFoldersToInclude.UseVisualStyleBackColor = true;
            this.btnFoldersToInclude.Click += new System.EventHandler(this.btnFoldersToInclude_Click);
            // 
            // cboFoldersToInclude
            // 
            this.cboFoldersToInclude.FormattingEnabled = true;
            this.cboFoldersToInclude.Location = new System.Drawing.Point(121, 64);
            this.cboFoldersToInclude.Name = "cboFoldersToInclude";
            this.cboFoldersToInclude.Size = new System.Drawing.Size(1209, 23);
            this.cboFoldersToInclude.TabIndex = 9;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Location = new System.Drawing.Point(750, 33);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(56, 25);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboBranches2
            // 
            this.cboBranches2.FormattingEnabled = true;
            this.cboBranches2.Location = new System.Drawing.Point(443, 33);
            this.cboBranches2.Name = "cboBranches2";
            this.cboBranches2.Size = new System.Drawing.Size(216, 23);
            this.cboBranches2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Branch 2";
            // 
            // cboBranches1
            // 
            this.cboBranches1.FormattingEnabled = true;
            this.cboBranches1.Location = new System.Drawing.Point(70, 33);
            this.cboBranches1.Name = "cboBranches1";
            this.cboBranches1.Size = new System.Drawing.Size(246, 23);
            this.cboBranches1.TabIndex = 5;
            // 
            // btnRepo
            // 
            this.btnRepo.AutoSize = true;
            this.btnRepo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRepo.Location = new System.Drawing.Point(3, 3);
            this.btnRepo.Name = "btnRepo";
            this.btnRepo.Size = new System.Drawing.Size(44, 25);
            this.btnRepo.TabIndex = 0;
            this.btnRepo.Text = "Repo";
            this.btnRepo.UseVisualStyleBackColor = true;
            this.btnRepo.Click += new System.EventHandler(this.btnRepo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Branch 1";
            // 
            // txtRepo
            // 
            this.txtRepo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRepo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRepo.Location = new System.Drawing.Point(59, 0);
            this.txtRepo.Name = "txtRepo";
            this.txtRepo.ReadOnly = true;
            this.txtRepo.Size = new System.Drawing.Size(1344, 29);
            this.txtRepo.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1417, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 438);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranch1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox txtRepo;
        private Button btnRepo;
        private TabPage tabPage2;
        private Panel panel1;
        private Label label1;
        private ComboBox cboBranches2;
        private Label label2;
        private ComboBox cboBranches1;
        private Button btnRefresh;
        private Label label3;
        private DataGridView dgBranch1;
        private Button btnFoldersToInclude;
        private ComboBox cboFoldersToInclude;
        private Button btnClearFolders;
        private DataGridView dgBranch2;
        private Label label4;
    }
}