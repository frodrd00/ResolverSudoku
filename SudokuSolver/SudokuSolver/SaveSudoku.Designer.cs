namespace SudokuSolver
{
    partial class SaveSudoku
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
            this.button1 = new System.Windows.Forms.Button();
            this.nombre = new System.Windows.Forms.Label();
            this.nombreSudoku = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nombre
            // 
            this.nombre.AutoSize = true;
            this.nombre.Location = new System.Drawing.Point(110, 20);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(44, 13);
            this.nombre.TabIndex = 1;
            this.nombre.Text = "Nombre";
            // 
            // nombreSudoku
            // 
            this.nombreSudoku.Location = new System.Drawing.Point(61, 50);
            this.nombreSudoku.Name = "nombreSudoku";
            this.nombreSudoku.Size = new System.Drawing.Size(133, 20);
            this.nombreSudoku.TabIndex = 2;
            this.nombreSudoku.KeyDown += new System.Windows.Forms.KeyEventHandler(this.key_enter);
            // 
            // SaveSudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 129);
            this.Controls.Add(this.nombreSudoku);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveSudoku";
            this.Text = "SaveSudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label nombre;
        private System.Windows.Forms.TextBox nombreSudoku;
    }
}