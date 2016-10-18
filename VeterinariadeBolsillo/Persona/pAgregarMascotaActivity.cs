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
    [Activity(Label = "pAgregarMascotaActivity")]
    public class pAgregarMascotaActivity : Activity
    {
        EditText txDni;
        ListView lstAnimales;
        List<Animal> animales = new List<Animal>();

        Persona persona;

        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vAgregarMascota);

            txDni = FindViewById<EditText>(Resource.Id.txDni);
            Button btBuscar = FindViewById<Button>(Resource.Id.btBuscar);
            btBuscar.Click += BtBuscar_Click;
            lstAnimales = FindViewById<ListView>(Resource.Id.lstAnimales);
            lstAnimales.ItemClick += LstAnimales_ItemClick;

            try
            {
                persona = JsonConvert.DeserializeObject<Persona>(Intent.GetStringExtra("persona"));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        public override void OnBackPressed()
        {
            this.Finish();
            Intent intent = new Intent(this, typeof(pHomeActivity));
            intent.PutExtra("persona", JsonConvert.SerializeObject(persona));
            StartActivity(intent);
        }

        private void LstAnimales_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                Animal animalSeleccionado = animales[e.Position];

                Mascota mascota = new Mascota();
                mascota.IdAnimal = animalSeleccionado.Id;
                mascota.IdPersona = persona.Id;

                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Agregando a mis mascotas..");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();

                

                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.UpsertMascotaCompleted += Ws_UpsertMascotaCompleted;
                ws.UpsertMascotaAsync(mascota);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Ws_UpsertMascotaCompleted(object sender, UpsertMascotaCompletedEventArgs e)
        {
            try
            {
                mProgress.Dismiss();
                Mascota m = (Mascota)e.Result;

                if (m != null)
                {
                    Toast.MakeText(this, "Su mascota ya fue agregada..", ToastLength.Short).Show();
                    Intent intent = new Intent(this, typeof(pHomeActivity));
                    intent.PutExtra("persona", JsonConvert.SerializeObject(persona));
                    StartActivity(intent);
                    this.Finish();
                }
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

                //var a = ws.GetAnimalesPorDueño(dni);

                //animales = a.ToList();
                ws.GetAnimalesPorDueñoCompleted += Ws_GetAnimalesPorDueñoCompleted;
                ws.GetAnimalesPorDueñoAsync(dni);
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
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}