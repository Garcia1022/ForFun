using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Drawing;
using Images;


// This class analyzes and solves an image of a maze
namespace MazeSolver
{
    class Maze
    {

        private bool foundStart = false;
        private bool foundFinish = false;
        private int[,] iData;
        public Bitmap img;

        private static int EMPTY = 0;
        private static int WALL = -100000;
        private static int BLACK = Color.Black.ToArgb();
        private static int WHITE = Color.White.ToArgb();
        private Tuple<int, int> start;
        private Tuple<int, int> finish;
        private int startColor;
        private int finishColor;
        private Tuple<int, int> s1;
        private Tuple<int, int> s2;
        private Tuple<int, int> s3;
        private Tuple<int, int> s4;
        private Tuple<int, int> f1;
        private Tuple<int, int> f2;
        private Tuple<int, int> f3;
        private Tuple<int, int> f4;
        private int selection = 0;
        //Algorithms
        private Astar algorithm1;
        private Mccurdy algorithm2;


        public Maze(ImageHolder i, int s)//constructor
        {

            selection = s;
            img = i.getImage();
            iData = new int[img.Width, img.Height];
            imageAnalyzer(img);
            
        }


      

        private void imageAnalyzer(Bitmap i)// analyze image and fins starting and finishing points
        {
            Console.WriteLine("Analyzing");
            for (int x = 0; x < i.Width; x++)
            {
                for (int y = 0; y < i.Height; y++)
                {

                    if (i.GetPixel(x, y).ToArgb() == BLACK || x == 0 || y == 0 || x == i.Width - 1 || y == i.Height - 1)
                    {
                        this.iData[x, y] = WALL;


                    }
                    else if (i.GetPixel(x, y).ToArgb() == WHITE)
                    {

                        this.iData[x, y] = EMPTY;
                    }
                    else
                    {
                        if (!foundStart)
                        {

                            startColor = i.GetPixel(x, y).ToArgb();


                            foundStart = true;
                            start = new Tuple<int, int>(x, y);
                            s1 = start;
                            s2 = s1;
                            s3 = s1;
                            s4 = s1;

                        }
                        else
                        {
                            if (i.GetPixel(x, y).ToArgb() != startColor && !foundFinish)
                            {
                                finishColor = i.GetPixel(x, y).ToArgb();
                                finish = new Tuple<int, int>(x, y);
                                f1 = finish;
                                f2 = f1;
                                f3 = f1;
                                f4 = f1;
                                foundFinish = true;
                            }

                            if (foundStart)//start in the center of the start pixels
                            {
                                if (i.GetPixel(x, y).ToArgb() == startColor)
                                {
                                    if (x < s1.Item1)
                                    {
                                        s1 = new Tuple<int, int>(x, y);

                                    }
                                    else if (x > s2.Item1)
                                    {
                                        s2 = new Tuple<int, int>(x, y);
                                    }
                                    else if (y < s3.Item2)
                                    {
                                        s3 = new Tuple<int, int>(x, y);

                                    }
                                    else if (y > s4.Item2)
                                    {
                                        s4 = new Tuple<int, int>(x, y);
                                    }
                                }
                            }
                            if (foundFinish)//end in the center of finish pixels
                            {
                                if (i.GetPixel(x, y).ToArgb() == finishColor)
                                {
                                    if (x < f1.Item1)
                                    {
                                        f1 = new Tuple<int, int>(x, y);

                                    }
                                    else if (x > f2.Item1)
                                    {
                                        f2 = new Tuple<int, int>(x, y);
                                    }
                                    else if (y < f3.Item2)
                                    {
                                        f3 = new Tuple<int, int>(x, y);

                                    }
                                    else if (y > f4.Item2)
                                    {
                                        f4 = new Tuple<int, int>(x, y);
                                    }
                                }
                            }
                        }

                    }


                }

            }
            start = new Tuple<int, int>((Math.Abs((s1.Item1 + s2.Item1) / 2)), (Math.Abs((s3.Item2 + s4.Item2) / 2)));
            finish = new Tuple<int, int>((Math.Abs((f1.Item1 + f2.Item1) / 2)), (Math.Abs((f3.Item2 + f4.Item2) / 2)));
           
            algorithm1 = new Astar(start, finish, img, iData);
            algorithm2 = new Mccurdy(start, finish, img, iData);
        }
       
        public void solveMaze()
        {
            switch(selection){

                case 1: algorithm1.solve();
                        break;

                case 2: algorithm2.solve();
                        break;

                default:algorithm2.solve();
                        break;
            }
            
            img = algorithm1.getImage();
        }


       
       




        




















    }
    
}
