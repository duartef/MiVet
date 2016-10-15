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
    [Activity(Label = "vProximasVisitasAnimal")]
    public class vProximasVisitasAnimal : Activity
    {
        TextView txNombre;
        ListView lstVisitas;

        int vetId;
        Animal animal = null;

        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vProximasVisitasAnimal);

            txNombre = FindViewById<TextView>(Resource.Id.txNombre);
            lstVisitas = FindViewById<ListView>(Resource.Id.lstVisitas);

            vetId = Intent.GetIntExtra("vetId", 0);
            animal = JsonConvert.DeserializeObject<Animal>(Intent.GetStringExtra("animal"));

            if (vetId != 0 && animal != null)
            {
                txNombre.SetText(txNombre.Text.Replace("@nombre", animal.Nombre), TextView.BufferType.Normal);

                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Cargando Visitas!");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();

                RunOnUiThread(() => mProgress.SetMessage("Obteniendo próximas visitas, por favor esperá unos segundos.."));

                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.GetProximasVisitasDelAnimalCompleted += Ws_GetProximasVisitasDelAnimalCompleted;
                ws.GetProximasVisitasDelAnimalAsync(vetId, animal.Id);
            }
        }

        private void Ws_GetProximasVisitasDelAnimalCompleted(object sender, MiVetService.GetProximasVisitasDelAnimalCompletedEventArgs e)
        {
            try
            {
                mProgress.Dismiss();

                List<Visita> visitas = (List<Visita>)e.Result.ToList();
                if (visitas != null)
                {
                    try
                    {
                        List<Animal> animales = new List<Animal>();
                        animales.Add(animal);
                        vVisitaAdapter adapter = new vVisitaAdapter(this, visitas, animales);
                        lstVisitas.Adapter = adapter;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Toast.MakeText(this, "Hubo un error :( ", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}