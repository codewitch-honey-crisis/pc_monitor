namespace PCMonitor
{
	partial class EditMonitor
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
			this.PortComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.RefreshPortsButton = new System.Windows.Forms.Button();
			this.EntryList = new System.Windows.Forms.ListBox();
			this.NewButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.UpButton = new System.Windows.Forms.Button();
			this.DownButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// PortComboBox
			// 
			this.PortComboBox.FormattingEnabled = true;
			this.PortComboBox.Location = new System.Drawing.Point(50, 16);
			this.PortComboBox.Name = "PortComboBox";
			this.PortComboBox.Size = new System.Drawing.Size(121, 24);
			this.PortComboBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Port";
			// 
			// RefreshPortsButton
			// 
			this.RefreshPortsButton.Location = new System.Drawing.Point(178, 16);
			this.RefreshPortsButton.Name = "RefreshPortsButton";
			this.RefreshPortsButton.Size = new System.Drawing.Size(75, 23);
			this.RefreshPortsButton.TabIndex = 2;
			this.RefreshPortsButton.Text = "Refresh";
			this.RefreshPortsButton.UseVisualStyleBackColor = true;
			// 
			// EntryList
			// 
			this.EntryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.EntryList.FormattingEnabled = true;
			this.EntryList.ItemHeight = 16;
			this.EntryList.Location = new System.Drawing.Point(50, 59);
			this.EntryList.Name = "EntryList";
			this.EntryList.Size = new System.Drawing.Size(398, 340);
			this.EntryList.TabIndex = 3;
			// 
			// NewButton
			// 
			this.NewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.NewButton.Location = new System.Drawing.Point(455, 59);
			this.NewButton.Name = "NewButton";
			this.NewButton.Size = new System.Drawing.Size(75, 23);
			this.NewButton.TabIndex = 4;
			this.NewButton.Text = "&New...";
			this.NewButton.UseVisualStyleBackColor = true;
			// 
			// DeleteButton
			// 
			this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DeleteButton.Enabled = false;
			this.DeleteButton.Location = new System.Drawing.Point(455, 88);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 5;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			// 
			// UpButton
			// 
			this.UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpButton.Enabled = false;
			this.UpButton.Location = new System.Drawing.Point(455, 118);
			this.UpButton.Name = "UpButton";
			this.UpButton.Size = new System.Drawing.Size(75, 23);
			this.UpButton.TabIndex = 6;
			this.UpButton.Text = "&Up";
			this.UpButton.UseVisualStyleBackColor = true;
			// 
			// DownButton
			// 
			this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DownButton.Enabled = false;
			this.DownButton.Location = new System.Drawing.Point(455, 148);
			this.DownButton.Name = "DownButton";
			this.DownButton.Size = new System.Drawing.Size(75, 23);
			this.DownButton.TabIndex = 7;
			this.DownButton.Text = "&Down";
			this.DownButton.UseVisualStyleBackColor = true;
			// 
			// EditMonitor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 433);
			this.Controls.Add(this.DownButton);
			this.Controls.Add(this.UpButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.NewButton);
			this.Controls.Add(this.EntryList);
			this.Controls.Add(this.RefreshPortsButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PortComboBox);
			this.MinimumSize = new System.Drawing.Size(301, 245);
			this.Name = "EditMonitor";
			this.Text = "Edit Monitor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox PortComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RefreshPortsButton;
		private System.Windows.Forms.ListBox EntryList;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button UpButton;
		private System.Windows.Forms.Button DownButton;
	}
}