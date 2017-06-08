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
        public Image<Gray, Byte> myImageBlackWhite; //imagen original en blanco y negro
        public Image<Gray, Byte> original; //imagen origninal
        private Image<Gray, byte>[,] listImages = new Image<Gray, byte>[9, 9]; //lista de 81 imagenes (celdas)
        private int[,] sudokuMatrix = new int[9,9]; //matriz del sudoku sin resolver
        private ImageBox ibNumber; 
        private int widthCell = 36;
        private int heightCell = 36;
        private String puzzle = ""; //string donde se almacena el sudoku sin resover seguido
        private bool salir = false;
        public Form1()
        {
            InitializeComponent();
            creaCeldas(null);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog1.FileName = "";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    Bitmap bm = new Bitmap(openFileDialog1.FileName);
                    System.Diagnostics.Debug.WriteLine(openFileDialog1.FileName);
                    myImageGray = new Image<Gray, Byte>(bm);
                    imageBox.Image = myImageGray;
                    original = myImageGray.Copy(); //guardamos la imagen original

                    guardarToolStripMenuItem.Enabled = true;
                    buttonAnalizar.Enabled = true;
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
            DialogResult result = MessageBox.Show("¿Estas seguro de salir?",
                  "Aviso",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                salir = true;
                Close();
            }
               
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String msg = string.Format("Esta aplicación permite analizar una imagen que contenga un sudoku para extraer los números y posteriormente resolver el sudoku.{0} " +
                "{0}Para comenzar puede subir una imagen directamente desde su ordenador mediante el botón nuevo, en la barra superior, o abrir un sudoku guardado en la base de datos mediante el botón abrir{0} " +
                "{0}Mediante el botón analizar, la aplicación buscará en la imagen los números del sudoku.{0} " +
                "{0}Mediante el boton resolver cada casilla vacia se rellenará del número correspondiente, es decir, se resolverá el sudoku.{0} " +
                "{0} {0}" +
                "{0}Nota: La aplicación solo acepta imagenes los siguientes formatos: .png  .jpg  .jpeg  .gif .bmp {0}", Environment.NewLine);
            MessageBox.Show(this, msg, "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void quienesSomosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String msg = string.Format("Aplicación realizada por:{0} " +
                "{0}Flavio Rodrigues Dias {0}" +
                "{0}Mario Llamas Lanza{0}", Environment.NewLine);
            MessageBox.Show(this, msg, "Sobre nosotros", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void creaCeldas(Image<Gray,byte> image)
        {
            //creamos las 81 celdas vacias
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

        private void buttonAnalizar_Click(object sender, EventArgs e)
        {
            buttonAnalizar.Enabled = false;
            guardarToolStripMenuItem.Enabled = false;
            Size sizeCell = new Size(widthCell, heightCell);
           
            progressBar1.Visible = true;

            SudokuGrabber sg = new SudokuGrabber(); //clase con los algoritmos
            myImageGray = sg.applyFilters(myImageGray); //aplicamos threshold y gausian blur, para cambiar a blanco y negro y reducir ruido respectivamente
            myImageBlackWhite = myImageGray.Copy();
            myImageGray = sg.findLargestObject(myImageGray, 0); //encuentra el elemento mas grande negro
            PointF[] arrayCorner = new PointF[4]; //array con las 4 esquinas
     
            
            arrayCorner = sg.findCorners(myImageGray); //encuentra las equinas

            myImageGray = sg.stretchImage(myImageGray,myImageBlackWhite,arrayCorner); //ajusta las imagenes al image box

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
            buttonResolver.Enabled = true;
            guardarToolStripMenuItem.Enabled = true;
        }

        private void buttonResolver_Click(object sender, EventArgs e)
        {
            puzzle = "";
            buttonResolver.Enabled = false;
            buttonAnalizar.Enabled = false;
            guardarToolStripMenuItem.Enabled = false;

            NumberFinder nf = new NumberFinder(listImages);
            sudokuMatrix = nf.getNumbers(); //aplica OCR (Optical Character Recognition ) en cada celda

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    System.Diagnostics.Debug.Write(sudokuMatrix[i,j]+" ");
                    puzzle = puzzle + sudokuMatrix[i, j];
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            csSudokuBruteForce sudokusolver = new csSudokuBruteForce(); //aplica fuerza bruta al sudoku
            int[] solution = sudokusolver.BruteForce(puzzle);
            int[,] solutionMatrix = new int[9, 9];

            // BlockCopy uses byte lengths: a double is 8 bytes
            Buffer.BlockCopy(solution, 0, solutionMatrix, 0, solution.Length * sizeof(int)); //cambiamos el vector a matriz
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            changeImages( solutionMatrix);

            progressBar1.Visible = false;
            guardarToolStripMenuItem.Enabled = true;

        }

        private void changeImages(int[,] solutionMatrix)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    progressBar1.Increment(1);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!salir)
            {
                DialogResult result = MessageBox.Show("¿Estas seguro de salir?",
                                       "Aviso",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }

            }
            
        }
    }
}