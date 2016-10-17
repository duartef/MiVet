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
    [Activity(Label = "pHomeActivity")]
    public class pHomeActivity : Activity
    {
        TextView txNombre;
        ListView lstMascotas;
        Button btAgragarMascota;

        Persona persona;
        List<Animal> misAnimales;

        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pHome2);

            txNombre = FindViewById<TextView>(Resource.Id.txPersona);
            lstMascotas = FindViewById<ListView>(Resource.Id.lstMacotas);
            lstMascotas.ItemClick += LstMascotas_ItemClick;
            btAgragarMascota = FindViewById<Button>(Resource.Id.btAgregarMascota);
            btAgragarMascota.Click += BtAgragarMascota_Click;

            LayoutHelper.MakeTransparentToolBar(this);

            try
            {
                persona = JsonConvert.DeserializeObject<Persona>(Intent.GetStringExtra("persona"));
                if (persona != null)
                {
                    txNombre.SetText(txNombre.Text.Replace("@nombre", persona.Nombre), TextView.BufferType.Normal);

                    mProgress = new ProgressDialog(this);
                    mProgress.SetCancelable(false);
                    mProgress.SetTitle("Recordando cuales eran tus mascotas...");
                    mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                    mProgress.Indeterminate = true;
                    mProgress.Show();

                    MiVetService.MiVetService ws = new MiVetService.MiVetService();
                    ws.GetAnimalesPorDueñoCompleted += Ws_GetAnimalesPorDueñoCompleted;
                    ws.GetAnimalesPorDueñoAsync(persona.Documento);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void BtAgragarMascota_Click(object sender, EventArgs e)
        {
            try
            {
                Intent intent = new Intent(this, typeof(pAgregarMascotaActivity));
                intent.PutExtra("persona", JsonConvert.SerializeObject(persona));
                StartActivity(intent);
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

                misAnimales = (List<Animal>)e.Result.ToList();
                if (misAnimales != null)
                {
                    try
                    {
                        pMascotaAdapter adapter = new pMascotaAdapter(this, misAnimales);
                        lstMascotas.Adapter = adapter;
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

        private void LstMascotas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "todavia en desarrollo", ToastLength.Short).Show();
        }
    }
}