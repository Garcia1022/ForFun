using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Drawing;

//Abstract class that have common things needed by various path finding / maze solving algorithms
namespace MazeSolver
{
     abstract class Algorithm
    {
         protected Bitmap img;
         protected static int VISITED=1;
         protected static int EMPTY = 0;
         protected static int WALL = -100000;
         protected Tuple<int, int> start;
         protected Tuple<int, int> finish;
         protected  List<Tuple<int, int>> solutionPath;
         protected int[,] iData;


         public Bitmap getImage() { return img; }
        
         public virtual void solve() { }
         
      



    }
}
