using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
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
        Image<Gray, Byte> myImageBlackWhite;

        public Form1()
        {
            InitializeComponent();
            creaCeldas(null);
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
            Console.WriteLine(openFileDialog1.FileName);
            myImageGray = new Image<Gray, Byte>(bm);
            imageBox.Image = myImageGray;
        }
        private void creaCeldas(Image<Gray,byte> image)
        {
            int widthCell = 36;
            int heightCell = 36;
            Size sizeCell = new Size(widthCell, heightCell);
            Point start = new Point(460, 48);

           
            int margin = 8;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ImageBox im = new ImageBox();
                    ((System.ComponentModel.ISupportInitialize)(im)).BeginInit();
                    SuspendLayout();
                    im.Location = new Point(start.X + j * (widthCell + margin), start.Y + i * (heightCell + margin));
                    im.Name = "imageBox" + i + j;
                    im.Size = sizeCell;
                    im.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    im.TabIndex = 2;
                    im.TabStop = false;
                    im.Enabled = false;
                    im.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    Controls.Add(im);
                    ((System.ComponentModel.ISupportInitialize)(im)).EndInit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            SudokuGrabber sg = new SudokuGrabber();
            myImageGray = sg.applyFilters(myImageGray);
            myImageBlackWhite = myImageGray.Copy();
            myImageGray = sg.findLargestObject(myImageGray, 0);
            PointF[] arrayCorner = new PointF[4];
     
            //tenemos que hacer una funcion que encuentre las esquinas
            arrayCorner = sg.findCorners(myImageGray);

            myImageGray = sg.stretchImage(myImageGray,myImageBlackWhite,arrayCorner);

            imageBox.Image = myImageGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}