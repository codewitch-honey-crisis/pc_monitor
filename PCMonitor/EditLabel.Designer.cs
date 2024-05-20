namespace PCMonitor
{
	partial class EditLabel
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
			this.HueStartLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.HueStartBar = new System.Windows.Forms.TrackBar();
			this.HueEndBar = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.HueEndLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TitleText = new System.Windows.Forms.TextBox();
			this.ValueTree = new System.Windows.Forms.TreeView();
			this.label4 = new System.Windows.Forms.Label();
			this.MaxText = new System.Windows.Forms.TextBox();
			this.ValueLabel = new System.Windows.Forms.Label();
			this.ValueTimer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.HueStartBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HueEndBar)).BeginInit();
			this.SuspendLayout();
			// 
			// HueStartLabel
			// 
			this.HueStartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.HueStartLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HueStartLabel.Location = new System.Drawing.Point(15, 338);
			this.HueStartLabel.Name = "HueStartLabel";
			this.HueStartLabel.Size = new System.Drawing.Size(26, 23);
			this.HueStartLabel.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 319);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Hue Start";
			// 
			// HueStartBar
			// 
			this.HueStartBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HueStartBar.AutoSize = false;
			this.HueStartBar.LargeChange = 10;
			this.HueStartBar.Location = new System.Drawing.Point(58, 338);
			this.HueStartBar.Maximum = 500;
			this.HueStartBar.Name = "HueStartBar";
			this.HueStartBar.Size = new System.Drawing.Size(285, 36);
			this.HueStartBar.TabIndex = 2;
			this.HueStartBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.HueStartBar.Scroll += new System.EventHandler(this.HueStartBar_Scroll);
			// 
			// HueEndBar
			// 
			this.HueEndBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HueEndBar.AutoSize = false;
			this.HueEndBar.LargeChange = 10;
			this.HueEndBar.Location = new System.Drawing.Point(57, 396);
			this.HueEndBar.Maximum = 500;
			this.HueEndBar.Name = "HueEndBar";
			this.HueEndBar.Size = new System.Drawing.Size(285, 36);
			this.HueEndBar.TabIndex = 5;
			this.HueEndBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.HueEndBar.Scroll += new System.EventHandler(this.HueEndBar_Scroll);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 377);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Hue End";
			// 
			// HueEndLabel
			// 
			this.HueEndLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.HueEndLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HueEndLabel.Location = new System.Drawing.Point(14, 396);
			this.HueEndLabel.Name = "HueEndLabel";
			this.HueEndLabel.Size = new System.Drawing.Size(26, 23);
			this.HueEndLabel.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 297);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Unit";
			// 
			// TitleText
			// 
			this.TitleText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TitleText.Location = new System.Drawing.Point(52, 294);
			this.TitleText.MaxLength = 1;
			this.TitleText.Name = "TitleText";
			this.TitleText.Size = new System.Drawing.Size(22, 22);
			this.TitleText.TabIndex = 7;
			this.TitleText.Text = "?";
			// 
			// ValueTree
			// 
			this.ValueTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ValueTree.Location = new System.Drawing.Point(12, 12);
			this.ValueTree.Name = "ValueTree";
			this.ValueTree.Size = new System.Drawing.Size(331, 276);
			this.ValueTree.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(203, 300);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Max";
			// 
			// MaxText
			// 
			this.MaxText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MaxText.Location = new System.Drawing.Point(241, 297);
			this.MaxText.MaxLength = 10;
			this.MaxText.Name = "MaxText";
			this.MaxText.Size = new System.Drawing.Size(100, 22);
			this.MaxText.TabIndex = 10;
			// 
			// ValueLabel
			// 
			this.ValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ValueLabel.Location = new System.Drawing.Point(81, 299);
			this.ValueLabel.Name = "ValueLabel";
			this.ValueLabel.Size = new System.Drawing.Size(116, 23);
			this.ValueLabel.TabIndex = 11;
			this.ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ValueTimer
			// 
			this.ValueTimer.Enabled = true;
			this.ValueTimer.Tick += new System.EventHandler(this.ValueTimer_Tick);
			// 
			// EditLabel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(353, 450);
			this.Controls.Add(this.ValueLabel);
			this.Controls.Add(this.MaxText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ValueTree);
			this.Controls.Add(this.TitleText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.HueEndBar);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.HueEndLabel);
			this.Controls.Add(this.HueStartBar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HueStartLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "EditLabel";
			this.Text = "Edit Label";
			this.Load += new System.EventHandler(this.EditLabel_Load);
			((System.ComponentModel.ISupportInitialize)(this.HueStartBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HueEndBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label HueStartLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar HueStartBar;
		private System.Windows.Forms.TrackBar HueEndBar;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label HueEndLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TitleText;
		private System.Windows.Forms.TreeView ValueTree;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox MaxText;
		private System.Windows.Forms.Label ValueLabel;
		private System.Windows.Forms.Timer ValueTimer;
	}
}