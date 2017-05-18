using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using MongoDB.Bson;
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
        public Image<Gray, Byte> myImageGray;
        public Image<Gray, Byte> myImageBlackWhite;
        public Image<Gray, Byte> original;
        private Image<Gray, byte>[,] listImages = new Image<Gray, byte>[9, 9];
        private int[,] sudokuMatrix = new int[9,9];
        private ImageBox ibNumber;
        private int widthCell = 36;
        private int heightCell = 36;
        private String puzzle = "";

        public Form1()
        {
            InitializeComponent();
            creaCeldas(null);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog1.FileName = "";
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
            SudokusDB sdb = new SudokusDB(this);
            sdb.ShowDialog();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSudoku ss = new SaveSudoku(openFileDialog1.FileName);
            ss.ShowDialog();
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
            System.Diagnostics.Debug.WriteLine(openFileDialog1.FileName);
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
            
            Size sizeCell = new Size(widthCell, heightCell);
           
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumberFinder nf = new NumberFinder(listImages);
            sudokuMatrix = nf.getNumbers();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    puzzle = puzzle + sudokuMatrix[i, j];
                }
            }

            csSudokuBruteForce sudokusolver = new csSudokuBruteForce();
            int[] solution = sudokusolver.BruteForce(puzzle);
            int[,] solutionMatrix = new int[9, 9];

            // BlockCopy uses byte lengths: a double is 8 bytes
            Buffer.BlockCopy(solution, 0, solutionMatrix, 0, solution.Length * sizeof(int)); //cambiamos el vector a matriz

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuMatrix[i, j] == 0) //si es 0 tenemos que cambiar la imagen
                    {
                        int numero = solutionMatrix[i, j]; //tenemos que poner en ese cuadrado la imagen de este numero
                        int auxi = 0, auxj = 0;
                        for (int h = 0; h < 9; h++)
                        {
                            for (int k = 0; k < 9; k++)
                            {
                                if (sudokuMatrix[h, k] == numero)
                                {
                                    auxi = h;
                                    auxj = k;
                                }
                            }
                        }
                        ImageBox ib = this.Controls.Find("imageBox" + i.ToString() + j.ToString(), true).FirstOrDefault() as ImageBox;
                        ib.Image = listImages[auxi, auxj];
                    }
                }
            }
        }
    }
}