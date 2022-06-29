namespace AslMonitor.Forms
{
    partial class Dashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.switchWorkStatus = new MaterialSkin.Controls.MaterialSwitch();
            this.lblUserName = new MaterialSkin.Controls.MaterialLabel();
            this.lblTimeFrom = new MaterialSkin.Controls.MaterialLabel();
            this.lblCurrentStatus = new MaterialSkin.Controls.MaterialLabel();
            this.lblRemarks = new MaterialSkin.Controls.MaterialLabel();
            this.cmbCurrentStatus = new MaterialSkin.Controls.MaterialComboBox();
            this.txtRemarks = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.btnSubmit = new MaterialSkin.Controls.MaterialButton();
            this.btnStart = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.btnReload = new System.Windows.Forms.Button();
            this.gridPreviousStates = new System.Windows.Forms.DataGridView();
            this.materialCard1.SuspendLayout();
            this.materialCard2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviousStates)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // switchWorkStatus
            // 
            this.switchWorkStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.switchWorkStatus.AutoSize = true;
            this.switchWorkStatus.Depth = 0;
            this.switchWorkStatus.Enabled = false;
            this.switchWorkStatus.Location = new System.Drawing.Point(315, 74);
            this.switchWorkStatus.Margin = new System.Windows.Forms.Padding(0);
            this.switchWorkStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.switchWorkStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.switchWorkStatus.Name = "switchWorkStatus";
            this.switchWorkStatus.Ripple = true;
            this.switchWorkStatus.Size = new System.Drawing.Size(66, 37);
            this.switchWorkStatus.TabIndex = 0;
            this.switchWorkStatus.Text = "  ";
            this.switchWorkStatus.UseVisualStyleBackColor = true;
            this.switchWorkStatus.CheckedChanged += new System.EventHandler(this.switchWorkStatus_CheckedChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Depth = 0;
            this.lblUserName.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblUserName.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.lblUserName.Location = new System.Drawing.Point(671, 88);
            this.lblUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(113, 29);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "user name";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTimeFrom
            // 
            this.lblTimeFrom.AutoSize = true;
            this.lblTimeFrom.Depth = 0;
            this.lblTimeFrom.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTimeFrom.Location = new System.Drawing.Point(28, 72);
            this.lblTimeFrom.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTimeFrom.Name = "lblTimeFrom";
            this.lblTimeFrom.Size = new System.Drawing.Size(78, 19);
            this.lblTimeFrom.TabIndex = 2;
            this.lblTimeFrom.Text = "Time From";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Depth = 0;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCurrentStatus.Location = new System.Drawing.Point(28, 29);
            this.lblCurrentStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(102, 19);
            this.lblCurrentStatus.TabIndex = 4;
            this.lblCurrentStatus.Text = "Current Status";
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Depth = 0;
            this.lblRemarks.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblRemarks.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            this.lblRemarks.Location = new System.Drawing.Point(28, 119);
            this.lblRemarks.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(63, 19);
            this.lblRemarks.TabIndex = 5;
            this.lblRemarks.Text = "Remarks";
            // 
            // cmbCurrentStatus
            // 
            this.cmbCurrentStatus.AutoResize = false;
            this.cmbCurrentStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCurrentStatus.Depth = 0;
            this.cmbCurrentStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCurrentStatus.DropDownHeight = 174;
            this.cmbCurrentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentStatus.DropDownWidth = 121;
            this.cmbCurrentStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbCurrentStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbCurrentStatus.FormattingEnabled = true;
            this.cmbCurrentStatus.IntegralHeight = false;
            this.cmbCurrentStatus.ItemHeight = 43;
            this.cmbCurrentStatus.Location = new System.Drawing.Point(6, 6);
            this.cmbCurrentStatus.MaxDropDownItems = 4;
            this.cmbCurrentStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCurrentStatus.Name = "cmbCurrentStatus";
            this.cmbCurrentStatus.Size = new System.Drawing.Size(196, 49);
            this.cmbCurrentStatus.StartIndex = 0;
            this.cmbCurrentStatus.TabIndex = 6;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Depth = 0;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtRemarks.Hint = "Remarks";
            this.txtRemarks.Location = new System.Drawing.Point(214, 7);
            this.txtRemarks.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(263, 45);
            this.txtRemarks.TabIndex = 9;
            this.txtRemarks.Text = "";
            // 
            // btnSubmit
            // 
            this.btnSubmit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSubmit.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSubmit.Depth = 0;
            this.btnSubmit.HighEmphasis = true;
            this.btnSubmit.Icon = null;
            this.btnSubmit.Location = new System.Drawing.Point(328, 60);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSubmit.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSubmit.Size = new System.Drawing.Size(146, 36);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit Changes";
            this.btnSubmit.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSubmit.UseAccentColor = false;
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStart.Depth = 0;
            this.btnStart.HighEmphasis = true;
            this.btnStart.Icon = null;
            this.btnStart.Location = new System.Drawing.Point(372, 73);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStart.Name = "btnStart";
            this.btnStart.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStart.Size = new System.Drawing.Size(67, 36);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start";
            this.btnStart.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStart.UseAccentColor = false;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel1.Location = new System.Drawing.Point(691, 64);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(84, 24);
            this.materialLabel1.TabIndex = 13;
            this.materialLabel1.Text = "Welcome";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel2.Location = new System.Drawing.Point(13, 5);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(64, 24);
            this.materialLabel2.TabIndex = 14;
            this.materialLabel2.Text = "Status:";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel3.Location = new System.Drawing.Point(12, 48);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(102, 24);
            this.materialLabel3.TabIndex = 15;
            this.materialLabel3.Text = "Time From:";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            this.materialLabel5.Location = new System.Drawing.Point(12, 91);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(84, 24);
            this.materialLabel5.TabIndex = 17;
            this.materialLabel5.Text = "Remarks:";
            // 
            // materialCard1
            // 
            this.materialCard1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialCard1.Controls.Add(this.lblCurrentStatus);
            this.materialCard1.Controls.Add(this.materialLabel5);
            this.materialCard1.Controls.Add(this.lblTimeFrom);
            this.materialCard1.Controls.Add(this.materialLabel3);
            this.materialCard1.Controls.Add(this.lblRemarks);
            this.materialCard1.Controls.Add(this.materialLabel2);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(6, 73);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(295, 156);
            this.materialCard1.TabIndex = 18;
            // 
            // materialCard2
            // 
            this.materialCard2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.Controls.Add(this.btnSubmit);
            this.materialCard2.Controls.Add(this.cmbCurrentStatus);
            this.materialCard2.Controls.Add(this.txtRemarks);
            this.materialCard2.Depth = 0;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(305, 125);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(488, 104);
            this.materialCard2.TabIndex = 19;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnReload.Location = new System.Drawing.Point(691, 26);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(103, 35);
            this.btnReload.TabIndex = 20;
            this.btnReload.Text = "Disconnected";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // gridPreviousStates
            // 
            this.gridPreviousStates.AllowUserToAddRows = false;
            this.gridPreviousStates.AllowUserToDeleteRows = false;
            this.gridPreviousStates.AllowUserToOrderColumns = true;
            this.gridPreviousStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPreviousStates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPreviousStates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPreviousStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPreviousStates.Location = new System.Drawing.Point(6, 237);
            this.gridPreviousStates.Name = "gridPreviousStates";
            this.gridPreviousStates.ReadOnly = true;
            this.gridPreviousStates.RowTemplate.Height = 25;
            this.gridPreviousStates.Size = new System.Drawing.Size(787, 206);
            this.gridPreviousStates.TabIndex = 21;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridPreviousStates);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.materialCard2);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.switchWorkStatus);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.Shown += new System.EventHandler(this.Dashboard_Shown);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviousStates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public NotifyIcon notifyIcon1;
        private MaterialSkin.Controls.MaterialSwitch switchWorkStatus;
        private MaterialSkin.Controls.MaterialLabel lblUserName;
        private MaterialSkin.Controls.MaterialLabel lblTimeFrom;
        private MaterialSkin.Controls.MaterialLabel lblCurrentStatus;
        private MaterialSkin.Controls.MaterialLabel lblRemarks;
        private MaterialSkin.Controls.MaterialComboBox cmbCurrentStatus;
        private MaterialSkin.Controls.MaterialButton btnSubmit;
        private MaterialSkin.Controls.MaterialButton btnStart;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private Button btnReload;
        private DataGridView gridPreviousStates;
        public MaterialSkin.Controls.MaterialMultiLineTextBox txtRemarks;
    }
}