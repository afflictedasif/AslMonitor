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
            this.lblTimeTo = new MaterialSkin.Controls.MaterialLabel();
            this.lblCurrentStatus = new MaterialSkin.Controls.MaterialLabel();
            this.lblRemarks = new MaterialSkin.Controls.MaterialLabel();
            this.cmbCurrentStatus = new MaterialSkin.Controls.MaterialComboBox();
            this.txtRemarks = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.btnSubmit = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStart = new MaterialSkin.Controls.MaterialButton();
            this.panel1.SuspendLayout();
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
            this.switchWorkStatus.AutoSize = true;
            this.switchWorkStatus.Depth = 0;
            this.switchWorkStatus.Location = new System.Drawing.Point(22, 79);
            this.switchWorkStatus.Margin = new System.Windows.Forms.Padding(0);
            this.switchWorkStatus.MouseLocation = new System.Drawing.Point(-1, -1);
            this.switchWorkStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.switchWorkStatus.Name = "switchWorkStatus";
            this.switchWorkStatus.Ripple = true;
            this.switchWorkStatus.Size = new System.Drawing.Size(144, 37);
            this.switchWorkStatus.TabIndex = 0;
            this.switchWorkStatus.Text = "Work Status";
            this.switchWorkStatus.UseVisualStyleBackColor = true;
            this.switchWorkStatus.CheckedChanged += new System.EventHandler(this.switchWorkStatus_CheckedChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Depth = 0;
            this.lblUserName.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblUserName.Location = new System.Drawing.Point(22, 148);
            this.lblUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(75, 19);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "user name";
            // 
            // lblTimeFrom
            // 
            this.lblTimeFrom.AutoSize = true;
            this.lblTimeFrom.Depth = 0;
            this.lblTimeFrom.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTimeFrom.Location = new System.Drawing.Point(22, 235);
            this.lblTimeFrom.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTimeFrom.Name = "lblTimeFrom";
            this.lblTimeFrom.Size = new System.Drawing.Size(78, 19);
            this.lblTimeFrom.TabIndex = 2;
            this.lblTimeFrom.Text = "Time From";
            // 
            // lblTimeTo
            // 
            this.lblTimeTo.AutoSize = true;
            this.lblTimeTo.Depth = 0;
            this.lblTimeTo.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTimeTo.Location = new System.Drawing.Point(22, 277);
            this.lblTimeTo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTimeTo.Name = "lblTimeTo";
            this.lblTimeTo.Size = new System.Drawing.Size(60, 19);
            this.lblTimeTo.TabIndex = 3;
            this.lblTimeTo.Text = "Time To";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Depth = 0;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCurrentStatus.Location = new System.Drawing.Point(22, 196);
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
            this.lblRemarks.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblRemarks.Location = new System.Drawing.Point(22, 315);
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
            this.cmbCurrentStatus.Location = new System.Drawing.Point(-2, 3);
            this.cmbCurrentStatus.MaxDropDownItems = 4;
            this.cmbCurrentStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCurrentStatus.Name = "cmbCurrentStatus";
            this.cmbCurrentStatus.Size = new System.Drawing.Size(337, 49);
            this.cmbCurrentStatus.StartIndex = 0;
            this.cmbCurrentStatus.TabIndex = 6;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.Depth = 0;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtRemarks.Location = new System.Drawing.Point(-2, 52);
            this.txtRemarks.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(340, 129);
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
            this.btnSubmit.Location = new System.Drawing.Point(104, 190);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCurrentStatus);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.txtRemarks);
            this.panel1.Location = new System.Drawing.Point(433, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 236);
            this.panel1.TabIndex = 11;
            // 
            // btnStart
            // 
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStart.Depth = 0;
            this.btnStart.HighEmphasis = true;
            this.btnStart.Icon = null;
            this.btnStart.Location = new System.Drawing.Point(701, 80);
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
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.lblCurrentStatus);
            this.Controls.Add(this.lblTimeTo);
            this.Controls.Add(this.lblTimeFrom);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.switchWorkStatus);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.Shown += new System.EventHandler(this.Dashboard_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon notifyIcon1;
        private MaterialSkin.Controls.MaterialSwitch switchWorkStatus;
        private MaterialSkin.Controls.MaterialLabel lblUserName;
        private MaterialSkin.Controls.MaterialLabel lblTimeFrom;
        private MaterialSkin.Controls.MaterialLabel lblTimeTo;
        private MaterialSkin.Controls.MaterialLabel lblCurrentStatus;
        private MaterialSkin.Controls.MaterialLabel lblRemarks;
        private MaterialSkin.Controls.MaterialComboBox cmbCurrentStatus;
        private MaterialSkin.Controls.MaterialMultiLineTextBox txtRemarks;
        private MaterialSkin.Controls.MaterialButton btnSubmit;
        private Panel panel1;
        private MaterialSkin.Controls.MaterialButton btnStart;
    }
}