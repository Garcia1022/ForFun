using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Drawing;
using System.Drawing.Imaging;

// This class abstract different types of images and provides functionality to convert to grey scale, draw the path 
namespace Images
{
   public class ImageHolder
    {

        protected String inputImageName;
        protected String outputImageName;
        
        protected Bitmap inputImage;
        protected Bitmap outputImage;

        public Bitmap getImage() { return inputImage; }

        public String getImageName(){
            return inputImageName;
        }
       //converts an image to grey scale
        public void GrayConvert()
        {
            outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            Graphics g = Graphics.FromImage(outputImage);
            float[][] grayArray =
            {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            };

            ColorMatrix mat = new ColorMatrix(grayArray);
            ImageAttributes att = new ImageAttributes();

            att.SetColorMatrix(mat);
            g.DrawImage(inputImage, new Rectangle(0, 0, inputImage.Width, inputImage.Height), 0, 0, inputImage.Width, inputImage.Height, GraphicsUnit.Pixel, att);
            g.Dispose();

        
        }
       //save the image to the disk
        public virtual void Save(){}
       //sets the output image, the image that will be saved to disk.
        public void setOutImage(Bitmap input)
        {
            outputImage = input;
           
           
        }
        
        public virtual System.Drawing.Color getPixel(Bitmap image, int x, int y) { return Color.Lime; }
        
       
       public ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        
               
    }
   public class JPG : ImageHolder
    {
        public JPG(String imageName, String outName) { 
            inputImageName=imageName;
            outputImageName=outName;
            try
            {
                inputImage = new Bitmap(inputImageName);
            }
            catch(FileNotFoundException e){
                throw e;
            }
            outputImage = null;
        
        }


        public override void Save()
        {
           
            try
            {
                
            ImageCodecInfo codecInfo = GetEncoderInfo("image/jpeg");
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 100L);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = encoderParameter;
            this.outputImage.Save(outputImageName, codecInfo, encoderParameters);

            }
            catch (Exception)
           {
                throw new System.Exception("File already exists, Please try a different file name \n\n");
            }
        }
        
        public override System.Drawing.Color getPixel(Bitmap image, int x, int y)
        {
            return image.GetPixel(x,y);
        }


    }


    public class PNG : ImageHolder
    {
        public PNG(String imageName, String outName)
        {
            inputImageName = imageName;
            outputImageName = outName;
            try
            {
                inputImage = new Bitmap(inputImageName);
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            outputImage = null;

        }


        public override  void Save()
        {
            try
            {
                ImageCodecInfo codecInfo = GetEncoderInfo("image/png");
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameter encoderParameter = new EncoderParameter(encoder, 50L);
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = encoderParameter;
                this.outputImage.Save(outputImageName, codecInfo, encoderParameters);

            }
            catch (Exception)
            {
                throw new System.Exception("File already exists, Please try a different file name \n\n");
            }

        }
       
        public override System.Drawing.Color getPixel(Bitmap image, int x, int y)
        {
            return image.GetPixel(x, y);
        }
    }

   public class BMP : ImageHolder
    {
        public BMP(String imageName, String outName)
        {
            inputImageName = imageName;
            outputImageName = outName;
            try
            {
                inputImage = new Bitmap(inputImageName);
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
            outputImage = null;

        }


        public override void Save()
        {
            try{
            ImageCodecInfo codecInfo = GetEncoderInfo("image/bmp");
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 50L);
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = encoderParameter;



            this.outputImage.Save(outputImageName, codecInfo, encoderParameters);
             }
            catch (Exception)
            {
                throw new System.Exception("File already exists, Please try a different file name \n\n");
            }

        }
       
        public override System.Drawing.Color getPixel(Bitmap image, int x, int y)
        {
            return image.GetPixel(x, y);
        }











    }

}
