using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Drawing;
using System.Drawing.Imaging;

//class used by the A8 algorithm. Holds the heuristic data and location data.
namespace MazeSolver
{
    class Node : IEquatable<Node> , IComparable
    {



        public double gScore;
        public double hScore;
        public double fScore;
        public Tuple<int, int> location;


        public Node prev;






        public Node(Tuple<int, int> l)//constructor
        {


            location = l;




        }
        
        public  bool Equals(Node other)//define equals method to compare the location of a node in order to determin equality. 
        {
            
            if (location.Equals(other.location))
            {
                return true;
            }
            else
                return false;


        }
        public int CompareTo(object obj)// define compare function so that nodes ar compared by fscore. Used for sorting a list of nodes.
        {
            // If other is not a valid object reference, this instance is greater. 
            if (obj == null) return 1;

            // The temperature comparison depends on the comparison of  
            // the underlying Double values.  
            Node other = obj as Node;
            return fScore.CompareTo(other.fScore);

        }
        

    }
}
