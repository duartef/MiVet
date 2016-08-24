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

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vHomeActivity")]
    public class vHomeActivity : Activity
    {
        int vetId;
        string vetNombre;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vHome);

            vetId = Intent.GetIntExtra("vetId", 0);
            vetNombre = Intent.GetStringExtra("vetNombre");

            LayoutHelper.MakeTransparentToolBar(this);

            ImageView btNuevoAnimal = FindViewById<ImageView>(Resource.Id.btAlta);
            btNuevoAnimal.Click += BtNuevoAnimal_Click;
            ImageView btBuscarAnimal = FindViewById<ImageView>(Resource.Id.btBuscar);
            btBuscarAnimal.Click += BtBuscarAnimal_Click;
            ImageView btVerVisitas = FindViewById<ImageView>(Resource.Id.btVisitas);
            btVerVisitas.Click += BtVerVisitas_Click;
            ImageView btAjustes = FindViewById<ImageView>(Resource.Id.btAjustes);
            btAjustes.Click += BtAjustes_Click;
        }

        private void BtAjustes_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtVerVisitas_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtBuscarAnimal_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtNuevoAnimal_Click(object sender, EventArgs e)
        {
            if (vetId != 0)
            {
                Intent intent = new Intent(this, typeof(vNuevoAnimalActivity));
                intent.PutExtra("vetId", vetId);
                StartActivity(intent);
            }
        }
    }
}