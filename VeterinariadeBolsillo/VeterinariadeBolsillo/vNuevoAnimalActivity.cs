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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            vetId = Intent.GetIntExtra("vetId", 0);
            SetContentView(Resource.Layout.vNuevoAnimal);

            txNombre = FindViewById<EditText>(Resource.Id.txNombre);
            txRaza = FindViewById<AutoCompleteTextView>(Resource.Id.txRaza);
            txEspecie = FindViewById<AutoCompleteTextView>(Resource.Id.txEspecie);
            txFechaNacimiento = FindViewById<EditText>(Resource.Id.txFechaNacimiento);
            txDocDueño = FindViewById<EditText>(Resource.Id.txDocDueño);
            spnSexo = FindViewById<Spinner>(Resource.Id.spnSexo);
            List<string> sexos = new List<string>();
            sexos.Add("Hembra");
            sexos.Add("Macho");
            spnSexo.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, sexos.ToArray());

            Button btGuardar = FindViewById<Button>(Resource.Id.btGuardar);
            btGuardar.Click += BtGuardar_Click;

            FillAutoCompleteText();

            LayoutHelper.MakeTransparentToolBar(this);
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
                Toast.MakeText(this, "Carga el nombre de la mascota!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txEspecie.Text))
            {
                Toast.MakeText(this, "Carga la especie de la mascota!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txFechaNacimiento.Text))
            {
                Toast.MakeText(this, "Carga la fecha de nacimiento de la mascota!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txDocDueño.Text))
            {
                Toast.MakeText(this, "Carga el documento del dueño!", ToastLength.Short).Show();
                return;
            }

            Animal animal = new Animal();
            animal.Documento = txDocDueño.Text.Trim();
            animal.Especie = txEspecie.Text.Trim();
            animal.FechaNacimiento = Convert.ToDateTime(txFechaNacimiento.Text.Trim());
            animal.Nombre = txNombre.Text.Trim();
            animal.Sexo = spnSexo.SelectedItemId.ToString();

            MiVetService.MiVetService ws = new MiVetService.MiVetService();
            ws.UpsertAnimalCompleted += Ws_UpsertAnimalCompleted;
            ws.UpsertAnimalAsync(animal);
            

            //Toast.MakeText(this, "Guardado de animal proximamente..", ToastLength.Short).Show();
        }

        private void Ws_UpsertAnimalCompleted(object sender, UpsertAnimalCompletedEventArgs e)
        {
            Animal animalGuardado = (Animal)e.Result;
            if (animalGuardado != null)
            {
                Toast.MakeText(this, "El animal se guardo bien!", ToastLength.Short).Show();
            }
        }
    }
}