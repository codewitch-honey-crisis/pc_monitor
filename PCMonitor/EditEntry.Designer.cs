namespace PCMonitor
{
	partial class EditEntry
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
			this.HueStartLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.HueEndLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.UnitText = new System.Windows.Forms.TextBox();
			this.ValueTree = new System.Windows.Forms.TreeView();
			this.label4 = new System.Windows.Forms.Label();
			this.MaxText = new System.Windows.Forms.TextBox();
			this.PathLabel = new System.Windows.Forms.Label();
			this.FormatText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.HsvCheckBox = new System.Windows.Forms.CheckBox();
			this.OkButton = new System.Windows.Forms.Button();
			this._CancelButton = new System.Windows.Forms.Button();
			this.StartColorDialog = new System.Windows.Forms.ColorDialog();
			this.EndColorDialog = new System.Windows.Forms.ColorDialog();
			this.Gradient = new PCMonitor.GradientBox();
			this.IconBox = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ChangeIconButton = new System.Windows.Forms.Button();
			this.OpenIconFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.ValueLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
			this.SuspendLayout();
			// 
			// HueStartLabel
			// 
			this.HueStartLabel.BackColor = System.Drawing.Color.Green;
			this.HueStartLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HueStartLabel.Location = new System.Drawing.Point(88, 213);
			this.HueStartLabel.Name = "HueStartLabel";
			this.HueStartLabel.Size = new System.Drawing.Size(26, 23);
			this.HueStartLabel.TabIndex = 0;
			this.HueStartLabel.Click += new System.EventHandler(this.HueStartLabel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 220);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Color Start";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(120, 220);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Color End";
			// 
			// HueEndLabel
			// 
			this.HueEndLabel.BackColor = System.Drawing.Color.Red;
			this.HueEndLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HueEndLabel.Location = new System.Drawing.Point(192, 213);
			this.HueEndLabel.Name = "HueEndLabel";
			this.HueEndLabel.Size = new System.Drawing.Size(26, 23);
			this.HueEndLabel.TabIndex = 3;
			this.HueEndLabel.Click += new System.EventHandler(this.HueEndLabel_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Unit";
			// 
			// UnitText
			// 
			this.UnitText.Location = new System.Drawing.Point(49, 52);
			this.UnitText.MaxLength = 1;
			this.UnitText.Name = "UnitText";
			this.UnitText.Size = new System.Drawing.Size(22, 22);
			this.UnitText.TabIndex = 7;
			this.UnitText.Text = "%";
			this.UnitText.Enter += new System.EventHandler(this.UnitText_Enter);
			this.UnitText.Leave += new System.EventHandler(this.UnitText_Leave);
			// 
			// ValueTree
			// 
			this.ValueTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ValueTree.Location = new System.Drawing.Point(254, 29);
			this.ValueTree.Name = "ValueTree";
			this.ValueTree.Size = new System.Drawing.Size(194, 146);
			this.ValueTree.TabIndex = 8;
			this.ValueTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ValueTree_AfterSelect);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Max";
			// 
			// MaxText
			// 
			this.MaxText.Location = new System.Drawing.Point(48, 23);
			this.MaxText.MaxLength = 10;
			this.MaxText.Name = "MaxText";
			this.MaxText.Size = new System.Drawing.Size(100, 22);
			this.MaxText.TabIndex = 10;
			this.MaxText.Text = "100";
			this.MaxText.Leave += new System.EventHandler(this.MaxText_Leave);
			// 
			// PathLabel
			// 
			this.PathLabel.AutoSize = true;
			this.PathLabel.Location = new System.Drawing.Point(251, 9);
			this.PathLabel.Name = "PathLabel";
			this.PathLabel.Size = new System.Drawing.Size(11, 16);
			this.PathLabel.TabIndex = 12;
			this.PathLabel.Text = "/";
			// 
			// FormatText
			// 
			this.FormatText.Location = new System.Drawing.Point(136, 53);
			this.FormatText.Name = "FormatText";
			this.FormatText.Size = new System.Drawing.Size(100, 22);
			this.FormatText.TabIndex = 13;
			this.FormatText.Text = "% 3.0f%%";
			this.FormatText.Leave += new System.EventHandler(this.FormatText_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(81, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 16);
			this.label5.TabIndex = 14;
			this.label5.Text = "Format";
			// 
			// HsvCheckBox
			// 
			this.HsvCheckBox.AutoSize = true;
			this.HsvCheckBox.Location = new System.Drawing.Point(176, 184);
			this.HsvCheckBox.Name = "HsvCheckBox";
			this.HsvCheckBox.Size = new System.Drawing.Size(57, 20);
			this.HsvCheckBox.TabIndex = 15;
			this.HsvCheckBox.Text = "HSV";
			this.HsvCheckBox.UseVisualStyleBackColor = true;
			this.HsvCheckBox.CheckedChanged += new System.EventHandler(this.HsvCheckBox_CheckedChanged);
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(373, 229);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 17;
			this.OkButton.Text = "&Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// _CancelButton
			// 
			this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._CancelButton.Location = new System.Drawing.Point(292, 229);
			this._CancelButton.Name = "_CancelButton";
			this._CancelButton.Size = new System.Drawing.Size(75, 23);
			this._CancelButton.TabIndex = 18;
			this._CancelButton.Text = "&Cancel";
			this._CancelButton.UseVisualStyleBackColor = true;
			this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
			// 
			// StartColorDialog
			// 
			this.StartColorDialog.AnyColor = true;
			this.StartColorDialog.Color = System.Drawing.Color.Green;
			// 
			// EndColorDialog
			// 
			this.EndColorDialog.AnyColor = true;
			this.EndColorDialog.Color = System.Drawing.Color.Red;
			// 
			// Gradient
			// 
			this.Gradient.EndColor = System.Drawing.Color.Red;
			this.Gradient.Is16Bit = true;
			this.Gradient.IsHsv = true;
			this.Gradient.Location = new System.Drawing.Point(11, 167);
			this.Gradient.Name = "Gradient";
			this.Gradient.Size = new System.Drawing.Size(159, 37);
			this.Gradient.StartColor = System.Drawing.Color.Green;
			this.Gradient.TabIndex = 16;
			// 
			// IconBox
			// 
			this.IconBox.BackColor = System.Drawing.Color.Black;
			this.IconBox.Location = new System.Drawing.Point(50, 83);
			this.IconBox.Name = "IconBox";
			this.IconBox.Size = new System.Drawing.Size(20, 20);
			this.IconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.IconBox.TabIndex = 19;
			this.IconBox.TabStop = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 84);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 16);
			this.label6.TabIndex = 20;
			this.label6.Text = "Icon";
			// 
			// ChangeIconButton
			// 
			this.ChangeIconButton.Location = new System.Drawing.Point(73, 81);
			this.ChangeIconButton.Name = "ChangeIconButton";
			this.ChangeIconButton.Size = new System.Drawing.Size(75, 23);
			this.ChangeIconButton.TabIndex = 21;
			this.ChangeIconButton.Text = "C&hange...";
			this.ChangeIconButton.UseVisualStyleBackColor = true;
			this.ChangeIconButton.Click += new System.EventHandler(this.ChangeIconButton_Click);
			// 
			// OpenIconFileDialog
			// 
			this.OpenIconFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png|All Files|*.*";
			// 
			// ValueLabel
			// 
			this.ValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ValueLabel.AutoSize = true;
			this.ValueLabel.Location = new System.Drawing.Point(254, 182);
			this.ValueLabel.Name = "ValueLabel";
			this.ValueLabel.Size = new System.Drawing.Size(66, 16);
			this.ValueLabel.TabIndex = 22;
			this.ValueLabel.Text = "(no value)";
			// 
			// EditEntry
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._CancelButton;
			this.ClientSize = new System.Drawing.Size(460, 264);
			this.Controls.Add(this.ValueLabel);
			this.Controls.Add(this.ChangeIconButton);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.IconBox);
			this.Controls.Add(this._CancelButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.Gradient);
			this.Controls.Add(this.HsvCheckBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.FormatText);
			this.Controls.Add(this.PathLabel);
			this.Controls.Add(this.MaxText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ValueTree);
			this.Controls.Add(this.UnitText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.HueEndLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HueStartLabel);
			this.MinimumSize = new System.Drawing.Size(478, 311);
			this.Name = "EditEntry";
			this.Text = "Monitor Entry";
			this.Load += new System.EventHandler(this.EditLabel_Load);
			((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label HueStartLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label HueEndLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox UnitText;
		private System.Windows.Forms.TreeView ValueTree;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox MaxText;
		private System.Windows.Forms.Label PathLabel;
		private System.Windows.Forms.TextBox FormatText;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox HsvCheckBox;
		private GradientBox Gradient;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button _CancelButton;
		private System.Windows.Forms.ColorDialog StartColorDialog;
		private System.Windows.Forms.ColorDialog EndColorDialog;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button ChangeIconButton;
		private System.Windows.Forms.PictureBox IconBox;
		private System.Windows.Forms.OpenFileDialog OpenIconFileDialog;
		private System.Windows.Forms.Label ValueLabel;
	}
}