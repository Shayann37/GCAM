namespace Interface_gcam
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
            textBox1 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(184, 136);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(439, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(276, 212);
            button1.Name = "button1";
            button1.Size = new Size(237, 45);
            button1.TabIndex = 1;
            button1.Text = "Se connecter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "G-CAM/Connexion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
    }
}