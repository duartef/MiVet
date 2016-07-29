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
    [Activity(Label = "HomeVetActivity")]
    public class HomeVetActivity : Activity
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
            throw new NotImplementedException();
        }

        private void BtAlta_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}