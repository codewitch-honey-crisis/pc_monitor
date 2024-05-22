
namespace PCMonitor
{
	partial class Main
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
			this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.StartedCheckBox = new System.Windows.Forms.CheckBox();
			this.Log = new System.Windows.Forms.TextBox();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.PortBox = new System.Windows.Forms.ListBox();
			this.NewButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UpdateTimer
			// 
			this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
			// 
			// Notify
			// 
			this.Notify.BalloonTipText = "PCMonitor";
			this.Notify.BalloonTipTitle = "PCMonitor";
			this.Notify.Text = "PCMonitor";
			this.Notify.Click += new System.EventHandler(this.Notify_Click);
			// 
			// StartedCheckBox
			// 
			this.StartedCheckBox.AutoSize = true;
			this.StartedCheckBox.Location = new System.Drawing.Point(12, 7);
			this.StartedCheckBox.Name = "StartedCheckBox";
			this.StartedCheckBox.Size = new System.Drawing.Size(72, 20);
			this.StartedCheckBox.TabIndex = 3;
			this.StartedCheckBox.Text = "Started";
			this.StartedCheckBox.UseVisualStyleBackColor = true;
			this.StartedCheckBox.CheckedChanged += new System.EventHandler(this.StartedCheckBox_CheckedChanged);
			// 
			// Log
			// 
			this.Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Log.Location = new System.Drawing.Point(214, 33);
			this.Log.Multiline = true;
			this.Log.Name = "Log";
			this.Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Log.Size = new System.Drawing.Size(340, 226);
			this.Log.TabIndex = 16;
			this.Log.WordWrap = false;
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshButton.Location = new System.Drawing.Point(478, 3);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(75, 23);
			this.RefreshButton.TabIndex = 17;
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// PortBox
			// 
			this.PortBox.FormattingEnabled = true;
			this.PortBox.ItemHeight = 16;
			this.PortBox.Location = new System.Drawing.Point(12, 33);
			this.PortBox.Name = "PortBox";
			this.PortBox.Size = new System.Drawing.Size(114, 212);
			this.PortBox.TabIndex = 18;
			// 
			// NewButton
			// 
			this.NewButton.Location = new System.Drawing.Point(133, 33);
			this.NewButton.Name = "NewButton";
			this.NewButton.Size = new System.Drawing.Size(75, 23);
			this.NewButton.TabIndex = 19;
			this.NewButton.Text = "&New...";
			this.NewButton.UseVisualStyleBackColor = true;
			this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.Enabled = false;
			this.DeleteButton.Location = new System.Drawing.Point(133, 63);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 20;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 260);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.NewButton);
			this.Controls.Add(this.PortBox);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.Log);
			this.Controls.Add(this.StartedCheckBox);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MinimumSize = new System.Drawing.Size(299, 242);
			this.Name = "Main";
			this.Text = "PCMonitor";
			this.Resize += new System.EventHandler(this.Main_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer UpdateTimer;
		private System.Windows.Forms.NotifyIcon Notify;
		private System.Windows.Forms.CheckBox StartedCheckBox;
		private System.Windows.Forms.TextBox Log;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.ListBox PortBox;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.Button DeleteButton;
	}
}

