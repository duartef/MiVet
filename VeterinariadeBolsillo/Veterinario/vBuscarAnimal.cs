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
    [Activity(Label = "vBuscarAnimal")]
    public class vBuscarAnimal : Activity
    {
        EditText txDni;
        ListView lstAnimales;
        List<lAnimal> animales = new List<lAnimal>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vBuscarAnimal);

            txDni = FindViewById<EditText>(Resource.Id.txDni);
            Button btBuscar = FindViewById<Button>(Resource.Id.btBuscar);
            btBuscar.Click += BtBuscar_Click;
            lstAnimales = FindViewById<ListView>(Resource.Id.lstAnimales);
            lstAnimales.ItemClick += LstAnimales_ItemClick;
        }

        private void LstAnimales_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                lAnimal animalSeleccionado = animales[e.Position];

                Intent intent = new Intent(this, typeof(vMascotaActivity));
                intent.PutExtra("animalId", animalSeleccionado.Id);
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                animales = null;

                string dni = txDni.Text.Trim();

                DataManager dm = new DataManager();
                animales = dm.GetTable<lAnimal>().Where(x => x.Documento == dni).ToList();

                //Toast.MakeText(this, "Llenar el listView", ToastLength.Short).Show();
                lstAnimales.Adapter = null;
                lstAnimales.Adapter = new vAnimalAdapter(this, animales);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}