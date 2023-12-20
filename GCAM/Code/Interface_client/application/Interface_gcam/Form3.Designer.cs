namespace Interface_gcam
{
    partial class Form3
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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button1 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(115, 38);
            button2.TabIndex = 4;
            button2.Text = "Page principale";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 56);
            button1.Name = "button1";
            button1.Size = new Size(115, 38);
            button1.TabIndex = 5;
            button1.Text = "Ajouter une caméra";
            button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(12, 100);
            button3.Name = "button3";
            button3.Size = new Size(115, 38);
            button3.TabIndex = 6;
            button3.Text = "Supprimer une caméra";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 144);
            button4.Name = "button4";
            button4.Size = new Size(115, 38);
            button4.TabIndex = 7;
            button4.Text = "Changer de mot de passe";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(button2);
            Name = "Form3";
            Text = "G-CAM/Paramètres";
            Load += Form3_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button1;
        private Button button3;
        private Button button4;
    }
}