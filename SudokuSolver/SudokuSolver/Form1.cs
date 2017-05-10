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
        Image<Gray, Byte> original;
        ImageBox ibNumber;
        int widthCell = 36;
        int heightCell = 36;

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
            original = myImageGray.Copy();
        }
        private void creaCeldas(Image<Gray,byte> image)
        {
            Size sizeCell = new Size(widthCell, heightCell);
            Point start = new Point(460, 48);

           
            int margin = 8;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ibNumber = new ImageBox();
                    ((System.ComponentModel.ISupportInitialize)(ibNumber)).BeginInit();
                    SuspendLayout();
                    ibNumber.Location = new Point(start.X + j * (widthCell + margin), start.Y + i * (heightCell + margin));
                    ibNumber.Name = "imageBox" + i + j;
                    ibNumber.Size = sizeCell;
                    ibNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    ibNumber.TabIndex = 2;
                    ibNumber.TabStop = false;
                    ibNumber.Enabled = false;
                    ibNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    Controls.Add(ibNumber);
                    ((System.ComponentModel.ISupportInitialize)(ibNumber)).EndInit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Gray, byte>[,] listImages = new Image<Gray, byte>[9, 9];
            Size sizeCell = new Size(widthCell, heightCell);
            //

            lbCargando.Visible = true;
            progressBar1.Visible = true;

            SudokuGrabber sg = new SudokuGrabber();
            myImageGray = sg.applyFilters(myImageGray);
            myImageBlackWhite = myImageGray.Copy();
            myImageGray = sg.findLargestObject(myImageGray, 0);
            PointF[] arrayCorner = new PointF[4];
     
            //tenemos que hacer una funcion que encuentre las esquinas
            arrayCorner = sg.findCorners(myImageGray);

            myImageGray = sg.stretchImage(myImageGray,myImageBlackWhite,arrayCorner);

          //  imageBox.Image = original;

            int widthImageNumber = myImageGray.Width / 9;
            int heightImageNumber = myImageGray.Height / 9;
            Size sizeCellNumber = new Size(widthImageNumber, heightImageNumber);

            
            //dividir la imagen
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    progressBar1.Increment(1);
                    myImageGray.ROI = new Rectangle(new Point((j * widthImageNumber), i * heightImageNumber), sizeCellNumber);
                    Image<Gray, byte> imagetest = new Image<Gray, byte>(widthImageNumber, heightImageNumber);
                    imagetest = myImageGray.Copy();
                    imagetest = sg.findLargestObject(imagetest, 1);
                    imagetest = sg.center(imagetest, new Point(sg.NumberBox.Left + sg.NumberBox.Width / 2, sg.NumberBox.Top + sg.NumberBox.Height / 2));
                    listImages[i, j] = imagetest;
                    ImageBox ib = this.Controls.Find("imageBox" + i.ToString() + j.ToString(), true).FirstOrDefault() as ImageBox;
                    ib.Image = imagetest;
                }
            }
            
            progressBar1.Visible = false;
            lbCargando.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}