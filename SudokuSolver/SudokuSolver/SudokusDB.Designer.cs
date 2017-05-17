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
            this.SuspendLayout();
            // 
            // listSudokus
            // 
            this.listSudokus.FormattingEnabled = true;
            this.listSudokus.Location = new System.Drawing.Point(24, 27);
            this.listSudokus.Name = "listSudokus";
            this.listSudokus.Size = new System.Drawing.Size(200, 199);
            this.listSudokus.TabIndex = 0;
            this.listSudokus.SelectedIndexChanged += new System.EventHandler(this.listSudokus_SelectedIndexChanged);
            // 
            // SudokusDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 262);
            this.Controls.Add(this.listSudokus);
            this.Name = "SudokusDB";
            this.Text = "SudokusDB";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listSudokus;

    }
}