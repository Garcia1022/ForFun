using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Images;
using System.IO;
using System.Media;
using System.Drawing;

//Matthew Garcia
// This is my Implementation of the A* pathfinding algorithm
namespace MazeSolver
{
    class Astar : Algorithm
    {

        






        public Astar(Tuple<int, int> s, Tuple<int, int> fin, Bitmap i, int[,] iDat)//Constructor
        {
            start = s;
            finish = fin;

            solutionPath = new List<Tuple<int, int>>();
            img = i;
            iData = iDat;




        }
      
        private List<Node> getNeighbors(Node n)//get the unexplored nodes around the current node and return a lst of them. 
        {
            List<Node> neighbors = new List<Node>();
            int x = n.location.Item1;
            int y = n.location.Item2;

           
            if (iData[x, y + 1] == EMPTY)//down
            {
                neighbors.Add(new Node(new Tuple<int, int>(x, y + 1)));

            }
            if (iData[x, y - 1] == EMPTY)//up
            {
                neighbors.Add(new Node(new Tuple<int, int>(x, y - 1)));

            }

            if (iData[x + 1, y] == EMPTY)//right
            {
                neighbors.Add(new Node(new Tuple<int, int>(x + 1, y)));

            }
            if (iData[x - 1, y] == EMPTY)//left
            {
                neighbors.Add(new Node(new Tuple<int, int>(x - 1, y)));

            }



            return neighbors;
        }



        override public void solve()// implementing the inherited method for the abstract class Algorithm. Basically it is the A* pathfinding algorithm implemented.
        {

            Console.WriteLine("Astar is solving the maze");
            List<Node> openSet = new List<Node>();
            solutionPath = new List<Tuple<int, int>>();

            bool success = false;
            Node s = new Node(this.start);
            s.gScore = 0;
            s.hScore = distance(s.location, finish);

            s.fScore = s.gScore + s.hScore;
            openSet.Add(s);
            Node curr = s;



            while (openSet.Count > 0)
            {



                openSet.Sort();
                curr = openSet[0];


                if (curr.location.Equals(finish))
                {
                    //construct path
                     draw(curr);
                    success = true;
                    break;
                }



                //remove current explored node
                openSet.RemoveAt(0);
                //set the node location to visited in the data map
                iData[curr.location.Item1, curr.location.Item2] = VISITED;

                List<Node> neighbors = getNeighbors(curr);

                //Add unexplored nodes the the open set so that they can be explored later.
                foreach (Node i in neighbors)
                {
                    if (iData[i.location.Item1, i.location.Item2] == EMPTY)
                    {

                        double tentativeGScore = curr.gScore + distance(i.location, curr.location);
                        if (!openSet.Contains(i) || tentativeGScore < i.gScore)
                        {
                            if (!openSet.Contains(i))
                            {
                                openSet.Add(i);

                            }

                            i.prev = curr;
                            i.gScore = tentativeGScore;
                            i.hScore = distance(i.location, finish);
                            i.fScore = i.gScore + i.hScore;



                        }



                    }
                }



            }
            if (success)
            {

                Console.WriteLine("Success");
            }
            else
            {//draw the path even if you fail to find a complete one
                draw(curr);
                Console.WriteLine("Failure");
            }

        }

        //A method that draws the shortest path that was found by the algorithm
         public void draw(Node curr)
        {
           Node c=curr;
            while (true)
            if (c.prev != null)
            {
                 
              //set the pixel of the image to green so that the path shows up
                img.SetPixel(c.location.Item1, c.location.Item2, Color.Green);
                c = c.prev;
            }
            else
            {
                
                img.SetPixel(c.location.Item1, c.location.Item2, Color.Green);
                break;
            }


        }
        //A helper function use to find the distance between two points
        private static double distance(Tuple<int, int> curr, Tuple<int, int> fin)
        {
            double dx = Math.Abs(curr.Item1 - fin.Item1);
            double dy = Math.Abs(curr.Item2 - fin.Item2);
            return Math.Sqrt(dx * dx + dy * dy);



        }
    }
}