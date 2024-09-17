namespace PouleSimulatie
{
	partial class PouleForm
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
			BtnExit = new Button();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			label1 = new Label();
			SuspendLayout();
			// 
			// BtnExit
			// 
			BtnExit.Location = new Point(656, 12);
			BtnExit.Name = "BtnExit";
			BtnExit.Size = new Size(132, 23);
			BtnExit.TabIndex = 0;
			BtnExit.Text = "Sluiten";
			BtnExit.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			button1.Location = new Point(12, 12);
			button1.Name = "button1";
			button1.Size = new Size(132, 23);
			button1.TabIndex = 1;
			button1.Text = "Speel 1 wedstrijd";
			button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Location = new Point(12, 41);
			button2.Name = "button2";
			button2.Size = new Size(132, 23);
			button2.TabIndex = 2;
			button2.Text = "Simuleer alles";
			button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			button3.Location = new Point(12, 70);
			button3.Name = "button3";
			button3.Size = new Size(132, 23);
			button3.TabIndex = 3;
			button3.Text = "Vorige speelronde";
			button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			button4.Location = new Point(236, 70);
			button4.Name = "button4";
			button4.Size = new Size(132, 23);
			button4.TabIndex = 4;
			button4.Text = "Volgende speelronde";
			button4.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(170, 74);
			label1.Name = "label1";
			label1.Size = new Size(36, 15);
			label1.TabIndex = 5;
			label1.Text = "12/12";
			// 
			// PouleForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label1);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(BtnExit);
			Name = "PouleForm";
			Text = "PouleForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button BtnExit;
		private Button button1;
		private Button button2;
		private Button button3;
		private Button button4;
		private Label label1;
	}
}