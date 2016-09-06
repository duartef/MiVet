//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Provider;
//using Android.Content.PM;
//using Android.Graphics;
//using Java.IO;
//using Android.Net;

//namespace VeterinariadeBolsillo
//{
//    public class ImagePicker
//    {

//        private static int DEFAULT_MIN_WIDTH_QUALITY = 400;        // min pixels
//        private static string TAG = "ImagePicker";
//        private static string TEMP_IMAGE_NAME = "tempImage";

//        public static int minWidthQuality = DEFAULT_MIN_WIDTH_QUALITY;


//        public static Intent getPickImageIntent(Context context)
//        {
//            Intent chooserIntent = null;

//            List<Intent> intentList = new List<Intent>();

//            Intent pickIntent = new Intent(Intent.ActionPick,
//                    Android.Provider.MediaStore.Images.Media.ExternalContentUri);
//            Intent takePhotoIntent = new Intent(MediaStore.ActionImageCapture);
//            takePhotoIntent.PutExtra("return-data", true);
//            takePhotoIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(getTempFile(context)));
//            intentList = addIntentsToList(context, intentList, pickIntent);
//            intentList = addIntentsToList(context, intentList, takePhotoIntent);

//            if (intentList.Count > 0)
//            {
//                chooserIntent = Intent.CreateChooser(intentList[0], "Tomar una imagen desde..");
//                chooserIntent.PutExtra(Intent.ExtraInitialIntents, intentList.ToArray());
//            }

//            return chooserIntent;
//        }

//        private static List<Intent> addIntentsToList(Context context, List<Intent> list, Intent intent)
//        {
//            IList<ResolveInfo> resInfo = context.PackageManager.QueryIntentActivities(intent, 0);
//            foreach (ResolveInfo resolveInfo in resInfo)
//            {
//                string packageName = resolveInfo.ActivityInfo.PackageName;
//                Intent targetedIntent = new Intent(intent);
//                targetedIntent.SetPackage(packageName);
//                list.Add(targetedIntent);
//                //Log.d(TAG, "Intent: " + intent.getAction() + " package: " + packageName);
//            }
//            return list;
//        }


//        public static Bitmap getImageFromResult(Context context, Result resultCode,
//                                                Intent imageReturnedIntent)
//        {
//            //Log.d(TAG, "getImageFromResult, resultCode: " + resultCode);
//            Bitmap bm = null;
//            File imageFile = getTempFile(context);
//            if (resultCode == Result.Ok)
//            {
//                Android.Net.Uri selectedImage;
//                bool isCamera = (imageReturnedIntent == null ||
//                        imageReturnedIntent.Data == null ||
//                        imageReturnedIntent.Data.ToString().Contains(imageFile.ToString()));
//                if (isCamera)
//                {     /** CAMERA **/
//                    selectedImage = Uri.FromFile(imageFile);
//                }
//                else
//                {            /** ALBUM **/
//                    selectedImage = imageReturnedIntent.Data;
//                }
//                //Log.d(TAG, "selectedImage: " + selectedImage);

//                bm = getImageResized(context, selectedImage);
//                int rotation = getRotation(context, selectedImage, isCamera);
//                bm = rotate(bm, rotation);
//            }
//            return bm;
//        }


//        private static File getTempFile(Context context)
//        {
//            File imageFile = new File(context.getExternalCacheDir(), TEMP_IMAGE_NAME);
//            imageFile.getParentFile().mkdirs();
//            return imageFile;
//        }

//        private static Bitmap decodeBitmap(Context context, Uri theUri, int sampleSize)
//        {
//            BitmapFactory.Options options = new BitmapFactory.Options();
//            options.inSampleSize = sampleSize;

//            AssetFileDescriptor fileDescriptor = null;
//            try
//            {
//                fileDescriptor = context.getContentResolver().openAssetFileDescriptor(theUri, "r");
//            }
//            catch (FileNotFoundException e)
//            {
//                e.printStackTrace();
//            }

//            Bitmap actuallyUsableBitmap = BitmapFactory.decodeFileDescriptor(
//                    fileDescriptor.getFileDescriptor(), null, options);

//            Log.d(TAG, options.inSampleSize + " sample method bitmap ... " +
//                    actuallyUsableBitmap.getWidth() + " " + actuallyUsableBitmap.getHeight());

//            return actuallyUsableBitmap;
//        }

//        /**
//         * Resize to avoid using too much memory loading big images (e.g.: 2560*1920)
//         **/
//        private static Bitmap getImageResized(Context context, Uri selectedImage)
//        {
//            Bitmap bm = null;
//            int[] sampleSizes = new int[] { 5, 3, 2, 1 };
//            int i = 0;
//            do
//            {
//                bm = decodeBitmap(context, selectedImage, sampleSizes[i]);
//                Log.d(TAG, "resizer: new bitmap width = " + bm.getWidth());
//                i++;
//            } while (bm.getWidth() < minWidthQuality && i < sampleSizes.length);
//            return bm;
//        }


//        private static int getRotation(Context context, Uri imageUri, bool isCamera)
//        {
//            int rotation;
//            if (isCamera)
//            {
//                rotation = getRotationFromCamera(context, imageUri);
//            }
//            else
//            {
//                rotation = getRotationFromGallery(context, imageUri);
//            }
//            Log.d(TAG, "Image rotation: " + rotation);
//            return rotation;
//        }

//        private static int getRotationFromCamera(Context context, Uri imageFile)
//        {
//            int rotate = 0;
//            try
//            {

//                context.getContentResolver().notifyChange(imageFile, null);
//                ExifInterface exif = new ExifInterface(imageFile.getPath());
//                int orientation = exif.getAttributeInt(
//                        ExifInterface.TAG_ORIENTATION,
//                        ExifInterface.ORIENTATION_NORMAL);

//                switch (orientation)
//                {
//                    case ExifInterface.ORIENTATION_ROTATE_270:
//                        rotate = 270;
//                        break;
//                    case ExifInterface.ORIENTATION_ROTATE_180:
//                        rotate = 180;
//                        break;
//                    case ExifInterface.ORIENTATION_ROTATE_90:
//                        rotate = 90;
//                        break;
//                }
//            }
//            catch (Exception e)
//            {
//                e.printStackTrace();
//            }
//            return rotate;
//        }

//        public static int getRotationFromGallery(Context context, Uri imageUri)
//        {
//            int result = 0;
//            String[] columns = { MediaStore.Images.Media.ORIENTATION };
//            Cursor cursor = null;
//            try
//            {
//                cursor = context.getContentResolver().query(imageUri, columns, null, null, null);
//                if (cursor != null && cursor.moveToFirst())
//                {
//                    int orientationColumnIndex = cursor.getColumnIndex(columns[0]);
//                    result = cursor.getInt(orientationColumnIndex);
//                }
//            }
//            catch (Exception e)
//            {
//                //Do nothing
//            }
//            finally
//            {
//                if (cursor != null)
//                {
//                    cursor.close();
//                }
//            }//End of try-catch block
//            return result;
//        }


//        private static Bitmap rotate(Bitmap bm, int rotation)
//        {
//            if (rotation != 0)
//            {
//                Matrix matrix = new Matrix();
//                matrix.postRotate(rotation);
//                Bitmap bmOut = Bitmap.createBitmap(bm, 0, 0, bm.getWidth(), bm.getHeight(), matrix, true);
//                return bmOut;
//            }
//            return bm;
//        }
//    }