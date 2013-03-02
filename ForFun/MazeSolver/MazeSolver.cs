using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Images;

// The class that allows people to solve mazes. A command line program
namespace MazeSolver
{
    class MazeSolver
    {
        static void Main(string[] args)
        {
            int choice;
            string filename;
            string outFilename;

           
            while (true)
            {
                while (true)
                {
                    Console.Write("Enter input File Name: inputFile[.png or .bmp or .jpg]\n\n");
                    filename = Console.ReadLine();
                    Console.Write("\n");
                    if (filename.EndsWith(".bmp") || filename.EndsWith(".png") || filename.EndsWith(".jpg")) { }
                    else
                    {
                        Console.Write("Argument has invalid file type\n");
                        continue;
                    }
                    Console.Write("Enter output File Name: outputFile[.png or .bmp or .jpg]\n\n");
                    outFilename = Console.ReadLine();
                    Console.Write("\n");
                    if (filename.Equals(outFilename))
                    {
                        Console.Write("File already exists, Please try a different file names \n\n");
                        continue;
                    }
                    if (outFilename.EndsWith(".bmp") || outFilename.EndsWith(".png") || outFilename.EndsWith(".jpg")) { break;}
                    else
                    {
                        Console.Write("Argument has invalid file type\n");
                        continue;
                    }

                    
                    
                }
                while (true)
                {
                    Console.Write("Enter Algorithm choice:  [ '1' for A* and '2' for Mccurdy]\n\n");
                    choice = int.Parse(Console.ReadLine());
                    Console.Write("\n");
                    if (choice > 2 || choice < 1)
                    {
                        Console.Write("Please choose one of the valid choices \n\n");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }






                if (outFilename.EndsWith(".bmp"))
                    {
                        BMP output = new BMP(filename, outFilename);

                        Maze M = new Maze(output, choice);
                        M.solveMaze();
                        output.setOutImage(M.img);
                        output.Save();
                        
                    }
                else if (outFilename.EndsWith(".png"))
                    {
                        PNG output = new PNG(filename, outFilename);

                        Maze M = new Maze(output, choice);
                        M.solveMaze();
                        output.setOutImage(M.img);
                        output.Save();
                        
                    }
                else if (outFilename.EndsWith(".jpg"))
                    {
                        JPG output = new JPG(filename, outFilename);

                        Maze M = new Maze(output, choice);
                        M.solveMaze();
                        output.setOutImage(M.img);
                        output.Save();
                       
                    }
                while (true)
                {
                    Console.Write("Want to solve another maze? yes or no\n\n");
                    string answer = Console.ReadLine();
                    Console.Write("\n");
                    if (answer == "yes") { break; }
                    else if (answer == "no")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Write("Please type 'yes' or 'no'\n\n");
                    } 

                }
            }

        }
    }
}
