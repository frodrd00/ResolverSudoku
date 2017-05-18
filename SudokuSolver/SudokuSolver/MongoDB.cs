using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SudokuSolver
{
    class MongoDB
    {
        MongoClient mc;

        public void connection()
        {
            mc = new MongoClient("mongodb://root:root@ds143231.mlab.com:43231/sudokudb");

        }

        //nos devuelve una lista con todos los sudokus de la base de datos
        public List<BsonDocument> getCollection()
        {
            var collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("sudokus.files").AsQueryable().ToList();

            return collection;
        }

        public IMongoDatabase getDatabase() {

            var db = mc.GetDatabase("sudokudb");

            return db;

        }

        //nos devuelve la id del sudoku seleccionado
        public BsonValue getID(string file) {

            var collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("sudokus.files").AsQueryable().ToList();

            BsonValue id = "";

            foreach (var document in collection)
            {
                BsonValue bd = document.GetElement(5).Value;

                if (bd == file)
                {
                    id = document.GetElement(0).Value;
                    break;
                }
                
            }

            return id;

        }

        public void deleteSudoku(BsonValue id) {

            var collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("sudokus.files");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            collection.DeleteMany(filter);

            collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("sudokus.chunks");
            filter = Builders<BsonDocument>.Filter.Eq("files_id", id);
            collection.DeleteMany(filter);
        }


    }
}
