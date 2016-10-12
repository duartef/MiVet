using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VeterinariadeBolsillo.MiVetService;
using Android.Provider;
using Android.Content.PM;
using Java.IO;
using Android.Graphics;
using Android.Database;
using System.Globalization;
//using System.IO;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vNuevoAnimalActivity")]
    public class vNuevoAnimalActivity : Activity
    {
        int vetId;
        EditText txNombre;
        AutoCompleteTextView txRaza;
        AutoCompleteTextView txEspecie;
        EditText txFechaNacimiento;
        Spinner spnSexo;
        EditText txDocDueño;

        Button btGuardar;
        Button btCamera;

        Animal animal = new Animal();

        static string path;
        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            vetId = Intent.GetIntExtra("vetId", 0);
            SetContentView(Resource.Layout.vNuevoAnimal);

            txNombre = FindViewById<EditText>(Resource.Id.txNombre);
            txRaza = FindViewById<AutoCompleteTextView>(Resource.Id.txRaza);
            txEspecie = FindViewById<AutoCompleteTextView>(Resource.Id.txEspecie);
            txFechaNacimiento = FindViewById<EditText>(Resource.Id.txFechaNacimiento);
            txFechaNacimiento.FocusChange += datePickerHandler;
            txDocDueño = FindViewById<EditText>(Resource.Id.txDocDueño);
            spnSexo = FindViewById<Spinner>(Resource.Id.spnSexo);
            List<string> sexos = new List<string>();
            sexos.Add("Hembra");
            sexos.Add("Macho");
            spnSexo.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, sexos.ToArray());

            btGuardar = FindViewById<Button>(Resource.Id.btGuardar);
            btGuardar.Click += BtGuardar_Click;

            Button btCancelar = FindViewById<Button>(Resource.Id.btCancelar);
            btCancelar.Click += BtCancelar_Click;

            if (IsThereAnAppToTakePictures())
            {
                //CreateDirectoryForPictures();

                btCamera = FindViewById<Button>(Resource.Id.btCamera);
                btCamera.Click += BtCamera_Click;
            }
            else
            {
                btCamera.Enabled = false;
            }

            FillAutoCompleteText();

            LayoutHelper.MakeTransparentToolBar(this);

            //if (vetId != 0)
            //{
            //    FillAnimalInfo();
            //}
        }

        private void FillAnimalInfo()
        {
            Toast.MakeText(this, "COMPLETAR CON EL ANIMAL QUE VINO", ToastLength.Short).Show();
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void BtCamera_Click(object sender, EventArgs e)
        {

            CreateDirectoryForPictures();

            Mierda._file = new File(Mierda._dir, String.Format("mascota_{0}.jpg", Guid.NewGuid()));
            //intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            //StartActivityForResult(intent, 0);

            Intent chooserIntent = null;

            List<Intent> intentList = new List<Intent>();

            Intent pickIntent = new Intent(Intent.ActionPick,
                    Android.Provider.MediaStore.Images.Media.ExternalContentUri);
            Intent takePhotoIntent = new Intent(MediaStore.ActionImageCapture);
            takePhotoIntent.PutExtra("return-data", true);
            takePhotoIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(Mierda._file));
            intentList = addIntentsToList(this, intentList, pickIntent);
            intentList = addIntentsToList(this, intentList, takePhotoIntent);

            if (intentList.Count > 0)
            {
                //intentList.Remove(intentList.Last());
                chooserIntent = Intent.CreateChooser(intentList[0], "Tomar una imagen desde..");
                chooserIntent.PutExtra(Intent.ExtraInitialIntents, intentList.ToArray());
            }

            StartActivityForResult(chooserIntent, 0);
        }

        private static List<Intent> addIntentsToList(Context context, List<Intent> list, Intent intent)
        {
            IList<ResolveInfo> resInfo = context.PackageManager.QueryIntentActivities(intent, 0);
            foreach (ResolveInfo resolveInfo in resInfo)
            {
                string packageName = resolveInfo.ActivityInfo.PackageName;
                Intent targetedIntent = new Intent(intent);
                targetedIntent.SetPackage(packageName);
                list.Add(targetedIntent);
                //Log.d(TAG, "Intent: " + intent.getAction() + " package: " + packageName);
            }
            return list;
        }

        //private void BtCamera_Click1(object sender, EventArgs e)
        //{
        //    Intent pickIntent = new Intent();
        //    pickIntent.SetType("image/*");
        //    pickIntent.SetAction(Intent.ActionGetContent);

        //    Intent takePhotoIntent = new Intent(MediaStore.ActionImageCapture);

        //    String pickTitle = "Tomar una imagen desde.."; // Or get from strings.xml
        //    Intent chooserIntent = Intent.CreateChooser(pickIntent, pickTitle);
        //    chooserIntent.PutExtra
        //    (
        //      Intent.ExtraInitialIntents,
        //      new Intent[] { takePhotoIntent }
        //    );

        //    StartActivityForResult(chooserIntent, 0);
        //}

        private void CreateDirectoryForPictures()
        {
            Mierda._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "MiVet");
            if (!Mierda._dir.Exists())
            {
                Mierda._dir.Mkdirs();
            }
        }

        private string GetImagePath(Android.Net.Uri uri)
        {
            string path = null;
            // The projection contains the columns we want to return in our query.
            string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
                cursor.Close();
                cursor.Dispose();
            }
            return path;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case 0:
                    if (resultCode == Result.Ok)
                    {
                        try
                        {
                            Android.Net.Uri contentUri;

                            // Make it available in the gallery
                            if (data != null && data.Data != null)
                            {
                                contentUri = data.Data;
                                Mierda._file = new File(GetImagePath(contentUri));
                            }
                            else
                            {
                                contentUri = Android.Net.Uri.FromFile(Mierda._file);
                            }
                            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                            //Android.Net.Uri contentUri = data.Data;//Android.Net.Uri.FromFile(data.Data);
                            mediaScanIntent.SetData(contentUri);
                            SendBroadcast(mediaScanIntent);

                            // Display in ImageView. We will resize the bitmap to fit the display.
                            // Loading the full sized image will consume to much memory
                            // and cause the application to crash.

                            int height = Resources.DisplayMetrics.HeightPixels;
                            int width = Resources.DisplayMetrics.WidthPixels;

                            //using (Mi)
                            //{

                            //}





                            //Mierda.bitmap = Mierda._file.Path.LoadAndResizeBitmap(width, height);
                            //BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
                            //using (var image = BitmapFactory.DecodeFile(Mierda._file.Path, options))
                            //{
                            //    //if (image != null)
                            //    {
                            //        int outHeight = options.OutHeight;
                            //        int outWidth = options.OutWidth;

                            //        while (outWidth > 1000)
                            //        {
                            //            outHeight /= 2;
                            //            outWidth /= 2;
                            //        }

                            //        using (var bitmapScaled = Bitmap.CreateScaledBitmap(image, outHeight, outWidth, true))
                            //        {
                            //            using (System.IO.MemoryStream outStream = new System.IO.MemoryStream())
                            //            {
                            //                bitmapScaled.Compress(Bitmap.CompressFormat.Jpeg, 50, outStream);
                            //                animal.Foto = outStream.ToArray();
                            //                outStream.Close();
                            //                outStream.Dispose();
                            //            }
                            //            bitmapScaled.Recycle();
                            //        }
                            //    }

                            //}

                            Mierda.bitmap = Mierda._file.Path.LoadAndResizeBitmap(width, height);
                            if (Mierda.bitmap != null)
                            {
                                //_imageView.SetImageBitmap(App.bitmap);
                                //App.bitmap = null;
                                byte[] byteArray = new byte[0];
                                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                                {
                                    Mierda.bitmap.Compress(Bitmap.CompressFormat.Jpeg, 25, stream);
                                    animal.Foto = stream.ToArray();
                                    stream.Close();
                                    stream.Dispose();
                                }
                            }

                            // Dispose of the Java side bitmap.
                            Mierda.bitmap.Dispose();
                            Mierda._dir.Dispose();
                            Mierda._file.Dispose();

                            GC.Collect();

                            Toast.MakeText(this, "Se adjunto bien la imagen!", ToastLength.Short).Show();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    break;
                case 1:
                    if (resultCode == Result.Ok)
                    {
                        //    Uri selectedImage = imageReturnedIntent.getData();
                        //    imageview.setImageURI(selectedImage);
                    }
                    break;
                default:
                    break;
            }
        }

        private void datePickerHandler(object sender, View.FocusChangeEventArgs e)
        {
            try
            {
                EditText dSender = ((EditText)sender);
                if (dSender.IsFocused)
                {
                    DatePickerFragment frag = DatePickerFragment.NewInstance(time => dSender.Text = time.ToString("dd/MM/yyyy"));
                    frag.Show(FragmentManager, "datePicker");
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                //Helper.LogDebug(ex);
                throw ex;
            }
        }

        private void BtCancelar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void FillAutoCompleteText()
        {
            List<string> razas = new List<string>();
            razas.Add("Airedale Terrier");
            razas.Add("Akita-inu");
            razas.Add("Alano Español");
            razas.Add("Alaskan Malamute");
            razas.Add("American-Staffordshire-Terrier");
            razas.Add("Azawakh");
            razas.Add("Basset-hound");
            razas.Add("Beagle");
            razas.Add("Beauceron");
            razas.Add("Bichon");
            razas.Add("Bobtail-dog");
            razas.Add("Boerboer");
            razas.Add("Border Collie");
            razas.Add("Boston Terrier");
            razas.Add("Boxer");
            razas.Add("Boyero de Verna");
            razas.Add("Braco de Weimar");
            razas.Add("Bulldog Frances");
            razas.Add("Bulldog Ingles");
            razas.Add("Bullmastiff");
            razas.Add("Cairn Terrier");
            razas.Add("Caniche");
            razas.Add("Carolina Dog");
            razas.Add("Catalburun");
            razas.Add("Cavalier King");
            razas.Add("Chino Crestado");
            razas.Add("Chiuhuahua");
            razas.Add("Chow Chow");
            razas.Add("Cocker Spaniel Ingles");
            razas.Add("Cocker Americano");
            razas.Add("Collie");
            razas.Add("Dalmata");
            razas.Add("Daschund");
            razas.Add("Doberman");
            razas.Add("Dogo Mallorquin");
            razas.Add("Dogo Argentino");
            razas.Add("Dogo Canario");
            razas.Add("Dogo del Tibet");
            razas.Add("Drahthaar");
            razas.Add("Fila Braseilero");
            razas.Add("Fox Terrier de Pelo Duro");
            razas.Add("French Poodle");
            razas.Add("Galgo Ruso (Borzoi)");
            razas.Add("Galgo Español");
            razas.Add("Golden Retriever");
            razas.Add("Gran Danes");
            razas.Add("Greyhound");
            razas.Add("Hachi");
            razas.Add("Hokaido.inu");
            razas.Add("Husky Siberiano");
            razas.Add("Jack Russell Terrier");
            razas.Add("Komondor");
            razas.Add("Labrador Retreiver");
            razas.Add("Labrel Persa");
            razas.Add("Leonberger");
            razas.Add("Lhasa Apso");
            razas.Add("Lundehund Noruego");
            razas.Add("Majorero");
            razas.Add("Maltes");
            razas.Add("Mastín Napolitano");
            razas.Add("Mastín Español");
            razas.Add("Montaña del Pirineo");
            razas.Add("Otherhound");
            razas.Add("Papillon");
            razas.Add("Pastor Aleman");
            razas.Add("Pastor Aleman Blanco");
            razas.Add("Pastor Belga");
            razas.Add("Pastor Catalán");
            razas.Add("Pastor de Anatolia");
            razas.Add("Pastor de los Pirineos");
            razas.Add("Pastor Shetland");
            razas.Add("Pastor del Cáucaso");
            razas.Add("Pastor Griego");
            razas.Add("Pastor Mallorquin");
            razas.Add("Pastor Portugués");
            razas.Add("Pastor Vasco");
            razas.Add("Pekines");
            razas.Add("Perdiguero de Burgos");
            razas.Add("Perro de Agua Español");
            razas.Add("Perro Cantor de Nueva Guinea");
            razas.Add("Perro Peruano");
            razas.Add("Pinscher Miniatura");
            razas.Add("Pit-bull American");
            razas.Add("Podenco Andaluz");
            razas.Add("Podenco Canario");
            razas.Add("Podenco Ibicenco");
            razas.Add("Podenco Portugues");
            razas.Add("Pointer");
            razas.Add("Pomeriana");
            razas.Add("Puddle");
            razas.Add("Pug");
            razas.Add("Ratonero Bodeguero Andaluz");
            razas.Add("Ridgeback Tailandes");
            razas.Add("Rottweiler");
            razas.Add("Sabueso Español");
            razas.Add("Samoyedo");
            razas.Add("San Bernardo");
            razas.Add("Schnauzer Gugante");
            razas.Add("Schnauzer Miniatura");
            razas.Add("Schnauzer Mediano");
            razas.Add("Scottish Terrier");
            razas.Add("Stter Gordon");
            razas.Add("Setter Inglés");
            razas.Add("Setter Irlandés");
            razas.Add("Shar Pei");
            razas.Add("Shiba-inu");
            razas.Add("Shikoku-inu");
            razas.Add("Shitzu");
            razas.Add("Spaniel Breton");
            razas.Add("Spitz Finlandes");
            razas.Add("Spitz Japones");
            razas.Add("Stafforshire Bul-Terrier");
            razas.Add("Teckel");
            razas.Add("Terrier Inglés");
            razas.Add("Terranova");
            razas.Add("Tosa-inu");
            razas.Add("Visla");
            razas.Add("Weimaraner");
            razas.Add("West Highland White Terrier");
            razas.Add("Whippet");
            razas.Add("Wolfhoun");
            razas.Add("Xoloescuincle");
            razas.Add("Yorkshire Terreir");
            razas.Add("Abisinio");
            razas.Add("Aleman de Pelo Largo");
            razas.Add("Angora Turco");
            razas.Add("American Curl");
            razas.Add("American Shorthair");
            razas.Add("American Wirehair");
            razas.Add("Australain Mist");
            razas.Add("Azul Ruso");
            razas.Add("Balinés");
            razas.Add("Bengalí");
            razas.Add("Bogtal Japonés");
            razas.Add("Bosque de Noruega");
            razas.Add("Burmilla");
            razas.Add("Burmés");
            razas.Add("Cornish Rex");
            razas.Add("Cymric");
            razas.Add("Chartreux");
            razas.Add("Devon Rex");
            razas.Add("Don Sphynx");
            razas.Add("Gato Bombay");
            razas.Add("Gato Brasileño");
            razas.Add("Ceylon");
            razas.Add("Europeo");
            razas.Add("Exotico");
            razas.Add("Gato Habana");
            razas.Add("Korat");
            razas.Add("Manx");
            razas.Add("Munchkin");
            razas.Add("Ocicat");
            razas.Add("Ojos Azules");
            razas.Add("Oriental");
            razas.Add("Oriental de Pelo Largo");
            razas.Add("Persa");
            razas.Add("Siamés");
            razas.Add("Siberiano");
            razas.Add("Singapura");
            razas.Add("Somalí");
            razas.Add("Tonkinés");
            razas.Add("LaPerm");
            razas.Add("Mau Egipcio");
            razas.Add("Peterbald");
            razas.Add("Pixiebob");
            razas.Add("Sagrado de Birmania");
            razas.Add("Scottish Fold");
            razas.Add("Selkirk Rex");
            razas.Add("Sphynx");
            razas.Add("Van Turco");
            razas.Add("Otra");
            txRaza.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, razas.ToArray());

            List<string> especies = new List<string>();
            especies.Add("Canino");
            especies.Add("Felino");
            especies.Add("Conejo");
            especies.Add("Ave");
            especies.Add("Iguana");
            especies.Add("Hurón");
            especies.Add("Cobayo");
            especies.Add("Erizo");
            especies.Add("Otros");
            txEspecie.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, especies.ToArray());

        }

        private void BtGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txNombre.Text))
            {
                Toast.MakeText(this, "No te olvides del nombre!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txEspecie.Text))
            {
                Toast.MakeText(this, "No te olvides de la especie!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txFechaNacimiento.Text))
            {
                Toast.MakeText(this, "No te olvides de la fecha de nacimiento!", ToastLength.Short).Show();
                return;
            }
            DateTime fNac;
            if (!DateTime.TryParseExact(txFechaNacimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fNac))
            {
                Toast.MakeText(this, "No te olvides de la fecha de nacimiento!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txDocDueño.Text))
            {
                Toast.MakeText(this, "No te olvides del documento del dueño!", ToastLength.Short).Show();
                return;
            }

            try
            {
                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Guardando Mascota!");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();

                RunOnUiThread(() => mProgress.SetMessage("Se está guardando la mascota, por favor esperá unos segundos.."));

                //Toast.MakeText(this, "Dame unos segundos para guardarlo...", ToastLength.Short).Show();

                animal.Documento = txDocDueño.Text.Trim();
                animal.Especie = txEspecie.Text.Trim();
                animal.FechaNacimiento = fNac;
                animal.Nombre = txNombre.Text.Trim();
                animal.Sexo = spnSexo.SelectedItemId.ToString();
                animal.Raza = txRaza.Text;
                animal.IdVeterinaria = vetId;
                animal.FechaDeCreacion = DateTime.Now;

                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.UpsertAnimalCompleted += Ws_UpsertAnimalCompleted;
                ws.UpsertAnimalAsync(animal);

                btGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                btGuardar.Enabled = true;
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Ws_UpsertAnimalCompleted(object sender, UpsertAnimalCompletedEventArgs e)
        {
            try
            {
                btGuardar.Enabled = true;
                mProgress.Dismiss();

                Animal animalGuardado = (Animal)e.Result;
                if (animalGuardado != null)
                {
                    try
                    {
                        //DataManager dm = new DataManager();
                        //dm.InsertlAnimal(animalGuardado);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Toast.MakeText(this, "El animal se guardó bien!", ToastLength.Short).Show();
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(this, "Hubo un error :(", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }

    public static class Mierda
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }
}