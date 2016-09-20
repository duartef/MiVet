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
using System.Globalization;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "Programar Visita")]
    public class vProgramarVisitaActivity : Activity
    {
        ProgressDialog mProgress;

        Animal animal;

        EditText txFechaVisita;
        EditText txVisita;
        EditText txComentariosInternos;
        Button btGuardar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vProgramarVisita);

            animal = JsonConvert.DeserializeObject<Animal>(Intent.GetStringExtra("animal"));

            txVisita = FindViewById<EditText>(Resource.Id.txVisita);
            txComentariosInternos = FindViewById<EditText>(Resource.Id.txComentariosInternos);

            txFechaVisita = FindViewById<EditText>(Resource.Id.txFechaVisita);
            txFechaVisita.FocusChange += datePickerHandler;

            btGuardar = FindViewById<Button>(Resource.Id.btGuardar);
            btGuardar.Click += BtGuardar_Click;
            Button btCancelar = FindViewById<Button>(Resource.Id.btCancelar);
            btCancelar.Click += BtCancelar_Click;

            LayoutHelper.MakeTransparentToolBar(this);
        }

        private void BtCancelar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void BtGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txVisita.Text))
            {
                Toast.MakeText(this, "No te olvides de completar el campo Visita!", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrEmpty(txFechaVisita.Text))
            {
                Toast.MakeText(this, "No te olvides de la fecha de visita!", ToastLength.Short).Show();
                return;
            }
            DateTime fVis;
            if (!DateTime.TryParseExact(txFechaVisita.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fVis))
            {
                Toast.MakeText(this, "No te olvides de la fecha de visita!", ToastLength.Short).Show();
                return;
            }

            try
            {
                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Guardando Visita!");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();

                RunOnUiThread(() => mProgress.SetMessage("Se está guardando la próxima visita, por favor esperá unos segundos.."));

                //Toast.MakeText(this, "Dame unos segundos para guardarlo...", ToastLength.Short).Show();

                Visita visita = new Visita();
                visita.Actividad = txVisita.Text.Trim();
                visita.ComentariosInternos = txComentariosInternos.Text.Trim();
                visita.Fecha = fVis;
                visita.IdAnimal = animal.Id;
                visita.IdVeterinaria = animal.IdVeterinaria;

                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.UpsertVisitaCompleted += Ws_UpsertVisitaCompleted;
                ws.UpsertVisitaAsync(visita);

                btGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                btGuardar.Enabled = true;
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Ws_UpsertVisitaCompleted(object sender, UpsertVisitaCompletedEventArgs e)
        {
            try
            {
                btGuardar.Enabled = true;
                mProgress.Dismiss();

                Visita visitaGuardada = (Visita)e.Result;
                if (visitaGuardada != null)
                {
                    try
                    {
                        //DataManager dm = new DataManager();
                        //dm.InsertlAnimal(animalGuardado);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Toast.MakeText(this, "La visita se guardó bien!", ToastLength.Short).Show();
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(this, "Hubo un error :(", ToastLength.Short).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void datePickerHandler(object sender, View.FocusChangeEventArgs e)
        {
            try
            {
                EditText dSender = ((EditText)sender);
                if (dSender.IsFocused)
                {
                    DatePickerFragment frag = DatePickerFragment.NewInstance(time => dSender.Text = time.ToString("dd/MM/yyyy"));
                    frag.Show(FragmentManager, "datePicker");
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                //Helper.LogDebug(ex);
                throw ex;
            }
        }
    }
}