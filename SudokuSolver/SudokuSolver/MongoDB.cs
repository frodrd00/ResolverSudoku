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

        public List<BsonDocument> getCollection()
        {
            var collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("usuarios").AsQueryable().ToList();

            return collection;
        }

        public void saveSudoku(BsonDocument document)
        {
            var collection = mc.GetDatabase("sudokudb").GetCollection<BsonDocument>("usuarios");

            collection.InsertOne(document);
        }

    }
}
