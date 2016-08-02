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

namespace MiVet
{
    [Activity(Label = "vHomeActivity")]
    public class vHomeActivity : Activity
    {
        int vetId;
        string vetNombre;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            vetId = Intent.GetIntExtra("vetId", 0);
            vetNombre = Intent.GetStringExtra("vetNombre");

            TextView txVeterinaria = FindViewById<TextView>(Resource.Id.txVeterinaria);
            Button btBuscar = FindViewById<Button>(Resource.Id.btBuscar);
            btBuscar.Click += BtBuscar_Click;
            Button btAlta = FindViewById<Button>(Resource.Id.btAlta);
            btAlta.Click += BtAlta_Click;
            Button btVisitas = FindViewById<Button>(Resource.Id.btVisitas);
            btVisitas.Click += BtVisitas_Click;
        }

        private void BtVisitas_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(vVisitasActivity));
            intent.PutExtra("vetId", vetId);
            intent.PutExtra("vetNombre", vetNombre);
            StartActivity(intent);
        }

        private void BtAlta_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(vAltaAnimalActivity));
            intent.PutExtra("vetId", vetId);
            intent.PutExtra("vetNombre", vetNombre);
            StartActivity(intent);
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(vBuscarAnimalActivity));
            intent.PutExtra("vetId", vetId);
            intent.PutExtra("vetNombre", vetNombre);
            StartActivity(intent);
        }
    }
}