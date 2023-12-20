namespace Interface_gcam
{
    partial class Form4
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
            button2 = new Button();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            textBox1 = new TextBox();
            button4 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(12, 24);
            button2.Name = "button2";
            button2.Size = new Size(115, 38);
            button2.TabIndex = 5;
            button2.Text = "Page principale";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 68);
            button1.Name = "button1";
            button1.Size = new Size(115, 38);
            button1.TabIndex = 6;
            button1.Text = "Supprimer une vidéo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(168, 30);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(211, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(168, 59);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(399, 23);
            textBox1.TabIndex = 11;
            // 
            // button4
            // 
            button4.Location = new Point(658, 30);
            button4.Name = "button4";
            button4.Size = new Size(89, 23);
            button4.TabIndex = 13;
            button4.Text = "Filtrer";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(658, 59);
            button3.Name = "button3";
            button3.Size = new Size(89, 23);
            button3.TabIndex = 12;
            button3.Text = "Rechercher";
            button3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Location = new Point(168, 97);
            panel1.Name = "panel1";
            panel1.Size = new Size(579, 330);
            panel1.TabIndex = 14;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(button1);
            Controls.Add(button2);
            Name = "Form4";
            Text = "G-CAM/Vidéos";
            Load += Form4_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox1;
        private Button button4;
        private Button button3;
        private Panel panel1;
    }
}