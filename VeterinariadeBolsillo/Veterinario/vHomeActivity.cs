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
using System.Threading;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vHomeActivity")]
    public class vHomeActivity : Activity
    {
        int vetId;
        string vetNombre;

        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vHome);

            vetId = Intent.GetIntExtra("vetId", 0);
            vetNombre = Intent.GetStringExtra("vetNombre");

            TextView txVeterinaria = FindViewById<TextView>(Resource.Id.txVeterinaria);
            txVeterinaria.SetText(txVeterinaria.Text.Replace("@nombre", vetNombre), TextView.BufferType.Normal);

            LayoutHelper.MakeTransparentToolBar(this);

            ImageView btNuevoAnimal = FindViewById<ImageView>(Resource.Id.btAlta);
            btNuevoAnimal.Click += BtNuevoAnimal_Click;
            ImageView btBuscarAnimal = FindViewById<ImageView>(Resource.Id.btBuscar);
            btBuscarAnimal.Click += BtBuscarAnimal_Click;
            ImageView btVerVisitas = FindViewById<ImageView>(Resource.Id.btVisitas);
            btVerVisitas.Click += BtVerVisitas_Click;
            ImageView btAjustes = FindViewById<ImageView>(Resource.Id.btAjustes);
            btAjustes.Click += BtAjustes_Click;

            //MiVetService.MiVetService ws = new MiVetService.MiVetService();
            //ws.GetAnimalesDeLaVeterinariaCompleted += Ws_GetAnimalesDeLaVeterinariaCompleted;
            //ws.GetAnimalesDeLaVeterinariaAsync(vetId);

            //mProgress = new ProgressDialog(this);
            //mProgress.SetCancelable(false);
            //mProgress.SetTitle("Sincronizando Datos");
            //mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
            //mProgress.Indeterminate = true;
            //mProgress.Show();

            //RunOnUiThread(() => mProgress.SetMessage("Probando el Web Service"));

            //new Thread(new ThreadStart(delegate
            //{
            //    COMM.Dispose_Resources();

            //    RunOnUiThread(() => mProgress.SetMessage("Probando el Web Service"));
            //    if (!COMM.Test_WEB_Service(this))
            //    {
            //        comunicando = false;
            //        RunOnUiThread(mProgress.Dismiss);
            //        return;
            //    }

            //})).Start();

            //Toast.MakeText(this, "Esperaaaaa", ToastLength.Long).Show();
        }

        private void Ws_GetAnimalesDeLaVeterinariaCompleted(object sender, MiVetService.GetAnimalesDeLaVeterinariaCompletedEventArgs e)
        {
            try
            {
                List<Animal> animales = (List<Animal>)e.Result.ToList();

                DataManager dm = new DataManager();
                foreach (Animal animal in animales)
                {
                    try
                    {
                        dm.InsertlAnimal(animal);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }

            RunOnUiThread(mProgress.Dismiss);

            //Toast.MakeText(this, "Listo!", ToastLength.Short).Show();
        }

        private void BtAjustes_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtVerVisitas_Click(object sender, EventArgs e)
        {
            if (vetId != 0)
            {
                Intent intent = new Intent(this, typeof(vProximasVisitasActivity));
                intent.PutExtra("vetId", vetId);
                StartActivity(intent);
            }
        }

        private void BtBuscarAnimal_Click(object sender, EventArgs e)
        {
            if (vetId != 0)
            {
                Intent intent = new Intent(this, typeof(vBuscarAnimal));
                intent.PutExtra("vetId", vetId);
                StartActivity(intent);
            }
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