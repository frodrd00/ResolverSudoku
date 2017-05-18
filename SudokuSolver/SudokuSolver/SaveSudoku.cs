using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.IO;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SaveSudoku : Form
    {
        String path;

        public SaveSudoku(string path)
        {
            InitializeComponent();
            nombreSudoku.Select();
            this.path = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String filename = nombreSudoku.Text;
            this.saveFile(filename);

        }

        public void saveFile(String filename) {

            //nos conectamos a la base de datos
            MongoDB mongo = new MongoDB();
            mongo.connection();

            //comprobar que no se haya nombres repetidos de sudokus
            var checkName = mongo.getID(filename);

            if (checkName == "") {

                gridfs(mongo, filename);
            }
            else
            {
                DialogResult result = MessageBox.Show("Ya existe un sudoku con ese nombre," +
                 "¿quieres remplazarlo?",
                 "Aviso",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    mongo.deleteSudoku(mongo.getID(filename));
                    gridfs(mongo, filename);
                }
            }
        }

        private void gridfs(MongoDB mongo, String filename)
        {
            //seleccionamos la base de datos
            IMongoDatabase database = mongo.getDatabase();

            //nombre de la collecion grid
            var bucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = "sudokus",
            });

            //cogemos los bytes de la imagen
            byte[] source = File.ReadAllBytes(path);
            //guardamos los bytes en la base de datos
            var id = bucket.UploadFromBytes(filename, source);

            MessageBox.Show("El sudoku ha sido guardado", "Aviso", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void key_enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String filename = nombreSudoku.Text;
                this.saveFile(filename);
            }
        }
    }
}
