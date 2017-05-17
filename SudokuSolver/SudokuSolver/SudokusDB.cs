using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokusDB : Form
    {
        public SudokusDB()
        {
            InitializeComponent();
            getsudokus();
        }

        private void listSudokus_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        public void getsudokus()
        {

            MongoDB mongo = new MongoDB();
            mongo.connection();
            var collection = mongo.getCollection();

            foreach (var document in collection)
            {
                listSudokus.Items.Add(document.GetElement(1).Value);
            }

        }
    }
}
