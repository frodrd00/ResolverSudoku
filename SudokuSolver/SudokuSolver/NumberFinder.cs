using Emgu.CV;
using Emgu.CV.ML;
using System;
using System.Collections.Generic;
using Emgu.CV.Structure;
using System.Drawing;

namespace SudokuSolver
{
    //Used tutorial for this class
    // http://aishack.in/tutorials/sudoku-grabber-opencv-extracting-digits/
    // http://www.pixel-technology.com/freeware/tessnet2/


    class NumberFinder
    {
        int K = 10;
        int MAX_NUM_IMAGES = 60000;
        Image<Gray, byte>[,] listImages = new Image<Gray, byte>[9, 9];
        int[,] sudokuMatrix = new int[9, 9];

        public NumberFinder(Image<Gray, byte>[,] listImages)
        {
            this.listImages = listImages;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuMatrix[i, j] = -1;
                }
            }
        }


        public int[,] getNumbers()
        {
            getEmpyCells();
            getNumbersCell();
            return sudokuMatrix;
        }

        private void getNumbersCell()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudokuMatrix[i, j] != 0) // means is empty
                    {
                        String number = useTesseract(listImages[i, j]);

                        //float result = digitRecognizer.classify(listImages[i,j]);
                        //sudokuMatrix[i, j] = (int) result;
                        int result = 0;
                        if (int.TryParse(number, out result))
                            sudokuMatrix[i, j] = result;
                        else
                            sudokuMatrix[i, j] = -2;
                    }
                }
            }
        }

        private void getEmpyCells()
        {
            bool numberFound = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (var y = 0; y < listImages[i, j].Height; y++)
                    {
                        for (var x = 0; x < listImages[i, j].Width; x++)
                        {
                            if (listImages[i, j][y, x].Intensity == 255)
                            {
                                //If we find white means we have a number
                                numberFound = true;
                            }
                        }
                    }
                    if (numberFound != true)
                        sudokuMatrix[i, j] = 0;

                    numberFound = false; //false again, to check next cell
                }
            }
        }

        private String useTesseract(Image<Gray, byte> image)
        {
            Image<Gray, byte> imagecopy = image.Copy();

            imagecopy = imagecopy.ThresholdBinaryInv(new Gray(125), new Gray(255));
            using (Bitmap bmp = imagecopy.ToBitmap())
            {
                tessnet2.Tesseract tessocr = new tessnet2.Tesseract();
                tessocr.SetVariable("tessedit_char_whitelist", "0123456789"); // If digit only
                tessocr.Init(@"./tessdata", "eng", false);

                List<tessnet2.Word> result = tessocr.DoOCR(bmp, Rectangle.Empty);
                return result[0].Text;
            }
        }
    }
}
