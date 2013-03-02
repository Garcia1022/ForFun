using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Images;
using System.IO;
using System.Media;
using System.Drawing;

namespace MazeSolver
{
    //Alogorithm created by Ben McCurdy
    //Implemented by Matthew Garcia
    class Mccurdy : Algorithm
    {
        public Mccurdy(Tuple<int, int> s, Tuple<int, int> fin, Bitmap i, int[,] iDat)//constructor
        {
            start = s;
            finish = fin;

            solutionPath = new List<Tuple<int, int>>();
            img = i;
            iData = iDat;




        }
        public override void solve()
        {
            depthscan();
        }

        //Finds the coordinate with the next lowest depth value
      
         private Tuple<int, int> findnextcoordinate(Tuple<int, int> c)
         {
            
             int depth = iData[c.Item1,c.Item2];
             if (depth < 0)
             {
                 return null;
             }

             Tuple<int, int> t = new Tuple<int, int>(c.Item1, c.Item2);
             int x = t.Item1;
             while (true)
             {
                 x++;
                 if (iData[x, t.Item2] == depth - 1 || (iData[x, t.Item2] == 0 && depth == 2))
                 {
                     return new Tuple<int,int>(x,t.Item2);
                 }
                 else if (iData[x,t.Item2] != depth)
                 {
                     break;
                 }
             }
             t =  new Tuple<int, int>(c.Item1, c.Item2);
            x = t.Item1;
             while (true)
             {
                 x--;
                 
                 if (iData[x, t.Item2] == depth - 1 || (iData[x, t.Item2] == 0 && depth == 2))
                 {
                 
                     return new Tuple<int, int>(x, t.Item2);
                 }
                 else if (iData[x,t.Item2] != depth)
                 {
                     break;
                 }
             }
             t = new Tuple<int, int>(c.Item1, c.Item2);
             int y = t.Item2;
             while (true)
             {
                 y++;
                 if (iData[t.Item1, y] == depth - 1 || (iData[t.Item1, y] == 0 && depth == 2))
                 {
                    
                     return  new Tuple<int, int>(t.Item1,y);
                 }
                 else if (iData[t.Item1, y] != depth)
                 {
                     break;
                 }
             }
             t = new Tuple<int, int>(c.Item1, c.Item2);
             y = t.Item2;
             while (true)
             {
                 y--;

                 if (iData[t.Item1, y] == depth - 1 || (iData[t.Item1, y] == 0 && depth == 2))
                 {
                    
                     return new Tuple<int, int>(t.Item1, y);
                 }
                 else if (iData[t.Item1,y] != depth)
                 {
                     break;
                 }
             }
             return null;
         }
        // loop through pixel array and assigns depth values to each pixel
         public void depthscan()
         {
             Console.WriteLine("McCurdy is solving the maze");
             Tuple<int, int> s = start;
             Tuple<int, int> f = finish ;
             bool go = true;
             String expand = "x";
             List<Tuple<int, int>> nextscan = new List<Tuple<int, int>>();
             List<Tuple<int, int>> scan = new List<Tuple<int, int>>();
             scan.Add(f);
             nextscan.Add(f);
             int depth = 1;
             //expand out in the Y direction first, then expand all those out in the x direction, incrementing the depth by one for each expansion
             /*         __
              *       | 1|
              *       | 1|__________________
              *       | 12222222222222222222|
              *       | s2222222222222222222|
              *       | 12222222222222222222|________________
              *       | 122222222222222222222222222222222222|                      
              *       | 122222222222222222222222222222222222|
              *         _______________________|333333333333|
              *                                |333333333333|
              *                                |33333f333333|
              *                                |____________|
              * 
              */
             while (go)
             {
                 if (expand.Equals("x"))
                 {
                     expand = "y";
                 }
                 else
                 {
                     expand = "x";
                 }
                 if (expand.Equals("x"))
                 {
                     foreach (Tuple<int, int> c in scan)
                     {
                         int range = 1;
                         while (iData[c.Item1 + range, c.Item2] == EMPTY)
                         {
                             iData[c.Item1 + range, c.Item2] = depth;
                             nextscan.Add(new Tuple<int, int>(c.Item1 + range, c.Item2));
                             if (c.Item1 + range == s.Item1 && c.Item2 == s.Item2)
                             {
                                 go = false;
                             }
                             range++;
                         }
                         range = 1;
                         while (iData[c.Item1 - range, c.Item2] == EMPTY)
                         {
                             iData[c.Item1 - range, c.Item2] = depth;
                             nextscan.Add(new Tuple<int, int>(c.Item1 - range, c.Item2));
                             if (c.Item1 - range == s.Item1 && c.Item2 == s.Item2)
                             {
                                 go = false;
                             }
                             range++;
                         }
                     }

                 }
                 else
                 {                    
                     foreach (Tuple<int, int> c in scan)
                     {
                         int range = 1;
                         while (iData[c.Item1, c.Item2 + range] == EMPTY)
                         {
                             iData[c.Item1, c.Item2 + range] = depth;
                             nextscan.Add(new Tuple<int, int>(c.Item1, c.Item2 + range));
                             if (c.Item1 == s.Item1 && c.Item2 + range == s.Item2)
                             {
                                 go = false;
                             }
                             range++;
                         }
                         range = 1;
                         while (iData[c.Item1, c.Item2 - range] == EMPTY)
                         {
                             iData[c.Item1, c.Item2 - range] = depth;
                             nextscan.Add(new Tuple<int, int>(c.Item1, c.Item2 - range));
                             if (c.Item1 == s.Item1 && c.Item2 - range == s.Item2)
                             {
                                 go = false;
                             }
                             range++;
                         }
                     }
                 }
                 scan.Clear();
                 scan.AddRange(nextscan);
                 nextscan.Clear();
                 depth++;
             }
             Console.WriteLine("Finished!");
             draw();
         }


        //begins at the finishline and retraces the path by sliding down the depth values until the startingline is reached.
         /*         __
             *       | 1|
             *       | 1|__________________
             *       | 12222222222222222222|
             *       | s2222222222222222222|
             *       | 12222222222222222222|________________
             *       | 122222222222222222222222222222222222|                      
             *       | 122222222222222222222222222222222222|
             *         _______________________|333333333333|
             *                                |333333333333|
             *                                |33333f333333|
             *                                |____________|
             * Start at f and go in the Y direction until you hit the next lowest depth and the in the X direction, repeat until you have reached the s
             */
         public void draw()
         {
             Tuple<int, int> c = new Tuple<int, int>(start.Item1, start.Item2);
             Tuple<int, int> temp = new Tuple<int, int>(start.Item1, start.Item2);

             while (c != null)
             {

                 temp = new Tuple<int, int>(c.Item1, c.Item2);//prev                
                 c = findnextcoordinate(c);//next

                 if (c != null)
                 {
                     if (temp.Item1 < c.Item1)
                     {
                         for (int i = temp.Item1; i <= c.Item1; i++)
                         {
                             img.SetPixel(i, c.Item2, Color.Green);

                         }
                     }
                     if (temp.Item1 > c.Item1)
                     {
                         for (int i = c.Item1; i < temp.Item1; i++)
                         {
                             img.SetPixel(i, c.Item2, Color.Green);
                         }
                     }
                     if (temp.Item2 < c.Item2)
                     {
                         for (int i = temp.Item2; i <= c.Item2; i++)
                         {
                             img.SetPixel(c.Item1, i, Color.Green);

                         }
                     }
                     if (temp.Item2 > c.Item2)
                     {
                         for (int i = c.Item2; i < temp.Item2; i++)
                         {
                             img.SetPixel(c.Item1, i, Color.Green);

                         }

                     }

                 }

             }
         }






    }
}
