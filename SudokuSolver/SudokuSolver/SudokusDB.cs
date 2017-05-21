using Emgu.CV;
using Emgu.CV.Structure;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokusDB : Form
    {
        private Form1 form1;

        public SudokusDB(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
            getsudokus();
        }

        private void listSudokus_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAbrir.Enabled = true;
            buttonBorrar.Enabled = true;
        }

        public void getsudokus()
        {

            MongoDB mongo = new MongoDB();
            mongo.connection();
            var collection = mongo.getCollection();

            foreach (var document in collection)
            {
                listSudokus.Items.Add(document.GetElement(5).Value);
            }

        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Estas seguro de borrarlo?",
                 "Aviso",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string file = listSudokus.GetItemText(listSudokus.SelectedItem);

                MongoDB mongo = new MongoDB();
                mongo.connection();
                BsonValue id = mongo.getID(file);

                mongo.deleteSudoku(id);

                listSudokus.Items.Remove(listSudokus.SelectedItem);
                listSudokus.Refresh();

                MessageBox.Show("El sudoku ha sido borrado", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            buttonAbrir.Enabled = false;
            buttonBorrar.Enabled = false;

        }

        private void buttonAbrir_Click(object sender, EventArgs e)
        {
            //seleccionamos el sudoku de la lista
            string file = listSudokus.GetItemText(listSudokus.SelectedItem);

            //cogemos el sudoku de la base de datos
            MongoDB mongo = new MongoDB();
            mongo.connection();
            BsonValue id = mongo.getID(file);
            IMongoDatabase database = mongo.getDatabase();

            var bucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = "sudokus",
            });

            //bytes del sodoku 
            var bytes = bucket.DownloadAsBytes(id);

            //convertir los bytes a la imagen
            MemoryStream ms = new MemoryStream(bytes);

            //cargamos la imagen el la imagebox
            Bitmap bm = new Bitmap(ms);
            form1.myImageGray = new Image<Gray, Byte>(bm);
            form1.imageBox.Image = form1.myImageGray;
            form1.original = form1.myImageGray.Copy();

            //cerramos ventana lista imagenes
            Close();
        }

        private void listSudokus_DoubleClick(object sender, EventArgs e)
        {
            if (listSudokus != null)
            {
                buttonAbrir_Click(sender,e);
            }
        }
    }
}
