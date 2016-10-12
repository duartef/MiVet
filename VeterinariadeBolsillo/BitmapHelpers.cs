using Android.Graphics;
using System.IO;

namespace VeterinariadeBolsillo
{
    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            try
            {
                BitmapFactory.Options options;

                options = new BitmapFactory.Options();
                options.InSampleSize = 2;
                Bitmap bitmap = BitmapFactory.DecodeFile(fileName, options);
                return bitmap;
            }
            catch (System.Exception)
            {
                throw;
            }
            


            //// First we get the the dimensions of the file on disk
            //BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true, InSampleSize = 3 };
            //Bitmap image = BitmapFactory.DecodeFile(fileName, options);

            //// Next we calculate the ratio that we need to resize the image by
            //// in order to fit the requested dimensions.
            //int outHeight = options.OutHeight;
            //int outWidth = options.OutWidth;

            //return image;

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            //int outHeight = options.OutHeight;
            //int outWidth = options.OutWidth;
            //int inSampleSize = 1;

            //while (outWidth > 1000)
            //{
            //    outHeight /= 2;
            //    outWidth /= 2;
            //}

            //options.OutHeight = outWidth;
            //options.OutWidth = outWidth;

            //if (outHeight > height || outWidth > width)
            //{
            //    inSampleSize = outWidth > outHeight
            //                       ? outHeight / height
            //                       : outWidth / width;
            //}

            //// Now we will load the image and have BitmapFactory resize it for us.
            //options.InSampleSize = inSampleSize;
            //options.InJustDecodeBounds = false;
            ////Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            //Bitmap resizedBitmap = Bitmap.CreateScaledBitmap(image, outWidth, outHeight, false);

            //return resizedBitmap;
        }
    }
}