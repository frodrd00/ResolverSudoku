using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        Image<Gray, Byte> myImageGray;

        public Form1()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void quienesSomosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AboutBox1 box = new AboutBox1();
            //box.ShowDialog();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap bm = new Bitmap(openFileDialog1.FileName);
            myImageGray = new Image<Gray, Byte>(bm);
            imageBox.Image = myImageGray;
        }
    }
}
