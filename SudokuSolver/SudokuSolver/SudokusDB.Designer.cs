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
            this.buttonAbrir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listSudokus
            // 
            this.listSudokus.BackColor = System.Drawing.SystemColors.Window;
            this.listSudokus.FormattingEnabled = true;
            this.listSudokus.Location = new System.Drawing.Point(12, 12);
            this.listSudokus.Name = "listSudokus";
            this.listSudokus.Size = new System.Drawing.Size(200, 199);
            this.listSudokus.TabIndex = 0;
            this.listSudokus.SelectedIndexChanged += new System.EventHandler(this.listSudokus_SelectedIndexChanged);
            this.listSudokus.DoubleClick += new System.EventHandler(this.listSudokus_DoubleClick);
            // 
            // buttonBorrar
            // 
            this.buttonBorrar.Enabled = false;
            this.buttonBorrar.Location = new System.Drawing.Point(110, 226);
            this.buttonBorrar.Name = "buttonBorrar";
            this.buttonBorrar.Size = new System.Drawing.Size(102, 23);
            this.buttonBorrar.TabIndex = 2;
            this.buttonBorrar.Text = "Borrar";
            this.buttonBorrar.UseVisualStyleBackColor = true;
            this.buttonBorrar.Click += new System.EventHandler(this.buttonBorrar_Click);
            // 
            // buttonAbrir
            // 
            this.buttonAbrir.Enabled = false;
            this.buttonAbrir.Location = new System.Drawing.Point(12, 226);
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
            this.ClientSize = new System.Drawing.Size(227, 265);
            this.Controls.Add(this.buttonAbrir);
            this.Controls.Add(this.buttonBorrar);
            this.Controls.Add(this.listSudokus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SudokusDB";
            this.ShowIcon = false;
            this.Text = "Lista Sudokus";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listSudokus;
        private System.Windows.Forms.Button buttonBorrar;
        private System.Windows.Forms.Button buttonAbrir;
    }
}