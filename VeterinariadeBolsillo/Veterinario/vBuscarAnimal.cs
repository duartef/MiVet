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
using Newtonsoft.Json;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vBuscarAnimal")]
    public class vBuscarAnimal : Activity
    {
        EditText txDni;
        ListView lstAnimales;
        List<Animal> animales = new List<Animal>();
        int vetId;
        ProgressDialog mProgress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vBuscarAnimal);

            vetId = Intent.GetIntExtra("vetId", 0);

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
                Animal animalSeleccionado = animales[e.Position];

                Intent intent = new Intent(this, typeof(vMascotaActivity));
                //intent.PutExtra("animalId", animalSeleccionado.Id);
                intent.PutExtra("animal", JsonConvert.SerializeObject(animalSeleccionado));
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

                //MiVetService.MiVetService ws = new MiVetService.MiVetService();
                //ws.GetAnimalesDeLaVeterinariaCompleted += Ws_GetAnimalesDeLaVeterinariaCompleted;
                //ws.GetAnimalesDeLaVeterinariaAsync(vetId);

                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Buscando entre las mascotas..");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();


                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.GetAnimalesPorDueñoCompleted += Ws_GetAnimalesPorDueñoCompleted;
                ws.GetAnimalesPorDueñoAsync(vetId, dni);
                //DataManager dm = new DataManager();
                //animales = dm.GetTable<lAnimal>().Where(x => x.Documento == dni).ToList();

                //Toast.MakeText(this, "Llenar el listView", ToastLength.Short).Show();
                //lstAnimales.Adapter = null;
                //lstAnimales.Adapter = new vAnimalAdapter(this, animales);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Ws_GetAnimalesPorDueñoCompleted(object sender, GetAnimalesPorDueñoCompletedEventArgs e)
        {
            try
            {
                mProgress.Dismiss();
                animales = (List<Animal>)e.Result.ToList();
                lstAnimales.Adapter = null;
                if (animales != null && animales.Count > 0)
                {
                    lstAnimales.Adapter = new vAnimalAdapter(this, animales);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}