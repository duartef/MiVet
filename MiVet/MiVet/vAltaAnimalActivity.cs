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
using MiVet.MiVetService;

namespace MiVet
{
    [Activity(Label = "vAltaAnimalActivity")]
    public class vAltaAnimalActivity : Activity
    {
        int vetId;
        EditText txNombre;
        EditText txRaza;
        EditText txEspecie;
        EditText txFechaNacimiento;
        Spinner spnSexo;
        EditText txDocDueño;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            vetId = Intent.GetIntExtra("vetId", 0);
            SetContentView(Resource.Layout.vAltaAnimal);

            txNombre = FindViewById<EditText>(Resource.Id.txNombre);
            txRaza = FindViewById<EditText>(Resource.Id.txRaza);
            txEspecie = FindViewById<EditText>(Resource.Id.txEspecie);
            txFechaNacimiento = FindViewById<EditText>(Resource.Id.txFechaNacimiento);
            txDocDueño = FindViewById<EditText>(Resource.Id.txDocDueño);
            spnSexo = FindViewById<Spinner>(Resource.Id.spnSexo);
            List<string> sexos = new List<string>();
            sexos.Add("Hembra");
            sexos.Add("Macho");
            spnSexo.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, sexos.ToArray());

            Button btGuardar = FindViewById<Button>(Resource.Id.btGuardar);
            btGuardar.Click += BtGuardar_Click;
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

            Toast.MakeText(this, "Guardado de animal proximamente..", ToastLength.Short).Show();
        }
    }
}