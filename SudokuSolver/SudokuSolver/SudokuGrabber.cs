using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace SudokuSolver
{
    class SudokuGrabber
    {
        Rectangle numberBox = new Rectangle();

        public Image<Gray, byte> applyFilters(Image<Gray, byte> image)
        {
            image = image.SmoothGaussian(5, 5, 1, 1);
            image = image.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, 5, new Gray(2));
            return image;
        }

        public Image<Gray, byte> findLargestObject(Image<Gray, byte> image, int value)
        {
            //if value == 0, means we want to find the frame
            //if value == 1, means we are trying to find the numbers
            int largestArea = 0;
            int area = 0;
            Point location = new Point(0, 0);
            Rectangle boundingbox = new Rectangle();

            // Floodfill every white pixel with new Gray(64), and while doing that, keep track of the largest area that was filled.
            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[y, x].Intensity == 255)//we are looking for black pixel because we are looking for the frame
                    {
                        //perform the floodfill on the pixel
                        area = CvInvoke.FloodFill(image, null, new Point(x, y), new MCvScalar(64),
                            out boundingbox,
                            new MCvScalar(0),
                            new MCvScalar(0));
                        if (area > largestArea)
                        {
                            if (value == 1 && boundingbox.Width < 35 && boundingbox.Height < 35 && area > 150)
                            {
                                //only enters here when we are looking for numbers
                                CvInvoke.FloodFill(image, null, location, new MCvScalar(0),
                                out boundingbox,
                                new MCvScalar(0),
                                new MCvScalar(0));
                                largestArea = area;

                                location = new Point(x, y);
                            }
                            else if (value == 1 && boundingbox.Width > 35 || value == 1 && boundingbox.Height > 35 || value == 1 && area < 150) // to print in black
                            {
                                CvInvoke.FloodFill(image, null, new Point(x, y), new MCvScalar(0), out boundingbox, new MCvScalar(0), new MCvScalar(0));
                            }

                            if (value == 0)
                            {
                                CvInvoke.FloodFill(image, null, location, new MCvScalar(0),
                                out boundingbox,
                                new MCvScalar(0),
                                new MCvScalar(0));
                                largestArea = area;
                                location = new Point(x, y);
                            }
                        }
                        else
                        {
                            CvInvoke.FloodFill(image, null, new Point(x, y), new MCvScalar(0),
                                    out boundingbox,
                                    new MCvScalar(0),
                                    new MCvScalar(0));
                        }
                    }
                }
            }
            if (location != new Point(0, 0)) //we only want the numbers in white, rest in black //for the second call
            {
                CvInvoke.FloodFill(image, null, location, new MCvScalar(255),
                            out boundingbox,
                            new MCvScalar(0),
                            new MCvScalar(0));
                numberBox = boundingbox;
            }
            return image;
        }

        public PointF[] findCorners(Image<Gray, byte> image)
        {
            double smallestDistance00 = 0;
            double smallestDistance10 = 0;
            double smallestDistance01 = 0;
            double smallestDistance11 = 0;
            PointF[] arrayCorner = new PointF[4];

            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    if (image[y, x].Intensity == 255)
                    {
                        double distance00 = Math.Sqrt(Math.Pow(y, 2) + Math.Pow(x, 2));
                        double distance10 = Math.Sqrt(Math.Pow(y, 2) + Math.Pow(image.Width - x, 2));
                        double distance01 = Math.Sqrt(Math.Pow(image.Height - y, 2) + Math.Pow(x, 2));
                        double distance11 = Math.Sqrt(Math.Pow(image.Height - y, 2) + Math.Pow(image.Width - x, 2));

                        if (smallestDistance00 == 0)
                        {
                            smallestDistance00 = distance00;
                        }
                        else if (smallestDistance00 > distance00)
                        {
                            smallestDistance00 = distance00;
                            arrayCorner[0] = new Point(x, y);
                        }

                        if (smallestDistance10 == 0)
                        {
                            smallestDistance10 = distance10;
                        }
                        else if (smallestDistance10 > distance10)
                        {
                            smallestDistance10 = distance10;
                            arrayCorner[1] = new Point(x, y);
                        }

                        if (smallestDistance01 == 0)
                        {
                            smallestDistance01 = distance01;
                        }
                        else if (smallestDistance01 > distance01)
                        {
                            smallestDistance01 = distance01;
                            arrayCorner[2] = new Point(x, y);
                        }

                        if (smallestDistance11 == 0)
                        {
                            smallestDistance11 = distance11;
                        }
                        else if (smallestDistance11 > distance11)
                        {
                            smallestDistance11 = distance11;
                            arrayCorner[3] = new Point(x, y);
                        }

                    }
                }
            }
            return arrayCorner;
        }
    }
}
