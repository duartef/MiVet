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
        EditText txDocDue�o;

        Button btGuardar;
        Button btCamera;

        Animal animal = new Animal();

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
            txDocDue�o = FindViewById<EditText>(Resource.Id.txDocDue�o);
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



            FillAutoCompleteText();

            LayoutHelper.MakeTransparentToolBar(this);
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        //private void CreateDirectoryForPictures()
        //{
        //    App._dir = new File(
        //        Environment.GetExternalStoragePublicDirectory(
        //            Environment.DirectoryPictures), "CameraAppDemo");
        //    if (!App._dir.Exists())
        //    {
        //        App._dir.Mkdirs();
        //    }
        //}

        private void BtCamera_Click(object sender, EventArgs e)
        {
            Intent chooserIntent = null;

            List<Intent> intentList = new List<Intent>();

            Intent pickIntent = new Intent(Intent.ActionPick,
                    Android.Provider.MediaStore.Images.Media.ExternalContentUri);
            Intent takePhotoIntent = new Intent(MediaStore.ActionImageCapture);
            takePhotoIntent.PutExtra("return-data", true);
            takePhotoIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(getTempFile(this)));
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
        private static File getTempFile(Context context)
        {
            File imageFile = new File(context.ExternalCacheDir, "tempImage");
            imageFile.ParentFile.Mkdirs();
            return imageFile;
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

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case 0:
                    if (resultCode == Result.Ok)
                    {
                        //Uri selectedImage = data.getData();
                        //imageview.setImageURI(selectedImage);
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
            razas.Add("Alano Espa�ol");
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
            razas.Add("Galgo Espa�ol");
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
            razas.Add("Mast�n Napolitano");
            razas.Add("Mast�n Espa�ol");
            razas.Add("Monta�a del Pirineo");
            razas.Add("Otherhound");
            razas.Add("Papillon");
            razas.Add("Pastor Aleman");
            razas.Add("Pastor Aleman Blanco");
            razas.Add("Pastor Belga");
            razas.Add("Pastor Catal�n");
            razas.Add("Pastor de Anatolia");
            razas.Add("Pastor de los Pirineos");
            razas.Add("Pastor Shetland");
            razas.Add("Pastor del C�ucaso");
            razas.Add("Pastor Griego");
            razas.Add("Pastor Mallorquin");
            razas.Add("Pastor Portugu�s");
            razas.Add("Pastor Vasco");
            razas.Add("Pekines");
            razas.Add("Perdiguero de Burgos");
            razas.Add("Perro de Agua Espa�ol");
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
            razas.Add("Sabueso Espa�ol");
            razas.Add("Samoyedo");
            razas.Add("San Bernardo");
            razas.Add("Schnauzer Gugante");
            razas.Add("Schnauzer Miniatura");
            razas.Add("Schnauzer Mediano");
            razas.Add("Scottish Terrier");
            razas.Add("Stter Gordon");
            razas.Add("Setter Ingl�s");
            razas.Add("Setter Irland�s");
            razas.Add("Shar Pei");
            razas.Add("Shiba-inu");
            razas.Add("Shikoku-inu");
            razas.Add("Shitzu");
            razas.Add("Spaniel Breton");
            razas.Add("Spitz Finlandes");
            razas.Add("Spitz Japones");
            razas.Add("Stafforshire Bul-Terrier");
            razas.Add("Teckel");
            razas.Add("Terrier Ingl�s");
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
            razas.Add("Balin�s");
            razas.Add("Bengal�");
            razas.Add("Bogtal Japon�s");
            razas.Add("Bosque de Noruega");
            razas.Add("Burmilla");
            razas.Add("Burm�s");
            razas.Add("Cornish Rex");
            razas.Add("Cymric");
            razas.Add("Chartreux");
            razas.Add("Devon Rex");
            razas.Add("Don Sphynx");
            razas.Add("Gato Bombay");
            razas.Add("Gato Brasile�o");
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
            razas.Add("Siam�s");
            razas.Add("Siberiano");
            razas.Add("Singapura");
            razas.Add("Somal�");
            razas.Add("Tonkin�s");
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
            especies.Add("Hur�n");
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
            if (!DateTime.TryParse(txFechaNacimiento.Text, out fNac))
            {
                Toast.MakeText(this, "No te olvides de la fecha de nacimiento!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txDocDue�o.Text))
            {
                Toast.MakeText(this, "No te olvides del documento del due�o!", ToastLength.Short).Show();
                return;
            }

            try
            {
                Toast.MakeText(this, "Dame unos segundos para guardarlo...", ToastLength.Short).Show();

                Animal animal = new Animal();
                animal.Documento = txDocDue�o.Text.Trim();
                animal.Especie = txEspecie.Text.Trim();
                animal.FechaNacimiento = fNac;
                animal.Nombre = txNombre.Text.Trim();
                animal.Sexo = spnSexo.SelectedItemId.ToString();
                animal.Raza = txRaza.Text;
                animal.IdVeterinaria = vetId;
                animal.Foto = new byte[1];

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

                Animal animalGuardado = (Animal)e.Result;
                if (animalGuardado != null)
                {
                    Toast.MakeText(this, "El animal se guard� bien!", ToastLength.Short).Show();
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
}