using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Images;


namespace GrayConverter
{
    class Program
    {
        static void Main(string[] args)
        {
          
            
            if(args.Length<2){
                throw new System.ArgumentException("Not Enough Arguments");
                
                
             }
            else if (args[0].Equals(args[1]))
            {
                throw new System.Exception("File already exists, Please try a different file name \n\n");
            }
            else if (args[0].EndsWith(".bmp") || args[0].EndsWith(".png") || args[0].EndsWith(".jpg"))
            {
                if (args[1].EndsWith(".bmp"))
                {
                    BMP output = new BMP(args[0], args[1]);
                    output.GrayConvert();
                    output.Save();
                }
                else if (args[1].EndsWith(".png"))
                {
                    PNG output = new PNG(args[0], args[1]);
                    output.GrayConvert();
                    output.Save();
                }
                else if (args[1].EndsWith(".jpg"))
                {
                    JPG output = new JPG(args[0], args[1]);
                    output.GrayConvert();
                    output.Save();
                }
                else
                {
                    throw new System.ArgumentException("Second argument has invalid file type");
                }
            }
            else
            {
                throw new System.ArgumentException("First argument has invalid file type");
            }

        }
    }
}
