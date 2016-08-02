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
    [Activity(Label = "vBuscarAnimalActivity")]
    public class vBuscarAnimalActivity : Activity
    {
        EditText txDni;
        List<Mascota> mascotas;
        List<Animal> animales;
        ListView lstMascotas;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.vBuscarAnimal);
            txDni = FindViewById<EditText>(Resource.Id.txDni);
            Button btBuscar = FindViewById<Button>(Resource.Id.btBuscar);
            btBuscar.Click += BtBuscar_Click;
            lstMascotas = FindViewById<ListView>(Resource.Id.lstMascotas);
            lstMascotas.ItemClick += LstMascotas_ItemClick;
        }

        private void LstMascotas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txDni.Text))
            {
                Toast.MakeText(this, "No te olvides de poner un DNI!", ToastLength.Short).Show();
            }

            //Buscar dentro de las mascotas
            mascotas = new List<Mascota>();
            animales = new List<Animal>();

            lstMascotas.Adapter = null;
            var sAdapter = new vAnimalAdapter(this, mascotas, animales);
            lstMascotas.Adapter = sAdapter;
        }
    }
}