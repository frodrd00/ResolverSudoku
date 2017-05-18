namespace SudokuSolver
{
    partial class SudokusDB
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
            this.listSudokus = new System.Windows.Forms.ListBox();
            this.buttonBorrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAbrir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listSudokus
            // 
            this.listSudokus.BackColor = System.Drawing.SystemColors.Window;
            this.listSudokus.FormattingEnabled = true;
            this.listSudokus.Location = new System.Drawing.Point(24, 54);
            this.listSudokus.Name = "listSudokus";
            this.listSudokus.Size = new System.Drawing.Size(200, 199);
            this.listSudokus.TabIndex = 0;
            this.listSudokus.SelectedIndexChanged += new System.EventHandler(this.listSudokus_SelectedIndexChanged);
            // 
            // buttonBorrar
            // 
            this.buttonBorrar.Enabled = false;
            this.buttonBorrar.Location = new System.Drawing.Point(122, 273);
            this.buttonBorrar.Name = "buttonBorrar";
            this.buttonBorrar.Size = new System.Drawing.Size(102, 23);
            this.buttonBorrar.TabIndex = 2;
            this.buttonBorrar.Text = "Borrar";
            this.buttonBorrar.UseVisualStyleBackColor = true;
            this.buttonBorrar.Click += new System.EventHandler(this.buttonBorrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista de Sudokus";
            // 
            // buttonAbrir
            // 
            this.buttonAbrir.Enabled = false;
            this.buttonAbrir.Location = new System.Drawing.Point(24, 273);
            this.buttonAbrir.Name = "buttonAbrir";
            this.buttonAbrir.Size = new System.Drawing.Size(92, 23);
            this.buttonAbrir.TabIndex = 4;
            this.buttonAbrir.Text = "Abrir";
            this.buttonAbrir.UseVisualStyleBackColor = true;
            this.buttonAbrir.Click += new System.EventHandler(this.buttonAbrir_Click);
            // 
            // SudokusDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 316);
            this.Controls.Add(this.buttonAbrir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBorrar);
            this.Controls.Add(this.listSudokus);
            this.Name = "SudokusDB";
            this.Text = "SudokusDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listSudokus;
        private System.Windows.Forms.Button buttonBorrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAbrir;
    }
}