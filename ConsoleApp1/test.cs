// See https://aka.ms/new-console-template for more information
using System.Drawing.Imaging;
using System.Drawing;
using Encoder = System.Drawing.Imaging.Encoder;

static class test
{
    public static void aaaa()
    {
      
        Bitmap myBitmap;
        ImageCodecInfo myImageCodecInfo;
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;

        byte[] myBytes;

        // Create a Bitmap object based on a BMP file.
        myBitmap = new Bitmap("");

        // Get an ImageCodecInfo object that represents the JPEG codec.
        myImageCodecInfo = GetEncoderInfo("image/jpeg");

        // Create an Encoder object based on the GUID

        // for the Quality parameter category.
        myEncoder = Encoder.Quality;

       
        myEncoderParameters = new EncoderParameters(1);

        // Save the bitmap as a JPEG file with quality level 25.
        myEncoderParameter = new EncoderParameter(myEncoder, 25L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        myBitmap.Save("Shapes025.jpg", myImageCodecInfo, myEncoderParameters);

        // Save the bitmap as a JPEG file with quality level 50.
        myEncoderParameter = new EncoderParameter(myEncoder, 70L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        myBitmap.Save("Shapes050.jpg", myImageCodecInfo, myEncoderParameters);

        using (MemoryStream ms = new MemoryStream())
        {
            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save(ms, myImageCodecInfo, myEncoderParameters);
            myBytes= ms.ToArray();
            Console.WriteLine(myBytes);  
            

        }
    }
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
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
 

