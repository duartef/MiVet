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
using Android.Provider;
using Java.Util;
using VeterinariadeBolsillo.MiVetService;
using Newtonsoft.Json;
using System.Globalization;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vIndicacionActivity")]
    public class vIndicacionActivity : Activity
    {
        EditText txFechaInicio;
        EditText txHoraInicio;
        EditText txFrecuencia;
        EditText txDuracion;
        EditText txDescripcion;
        Button btCancelar;
        Button btGuardar;

        int animalId;
        ProgressDialog mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vIndicacion);

            try
            {
                animalId = Intent.GetIntExtra("animalId", 0);
                if (animalId == 0)
                {
                    Toast.MakeText(this, "Error: No hay animal asociado", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }

            txFechaInicio = FindViewById<EditText>(Resource.Id.txFechaInicio);
            txFechaInicio.FocusChange += datePickerHandler;
            txHoraInicio = FindViewById<EditText>(Resource.Id.txHoraInicio);
            txFrecuencia = FindViewById<EditText>(Resource.Id.txFrecuencia);
            txDuracion = FindViewById<EditText>(Resource.Id.txDuracion);
            btGuardar = FindViewById<Button>(Resource.Id.btGuardar);
            btGuardar.Click += btGuardar_Click;
            btCancelar = FindViewById<Button>(Resource.Id.btCancelar);
            btCancelar.Click += BtCancelar_Click;
        }

        private void BtCancelar_Click(object sender, EventArgs e)
        {
            this.Finish();
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

        private void btGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txDescripcion.Text))
            {
                Toast.MakeText(this, "No te olvides de la Descripción!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txDuracion.Text))
            {
                Toast.MakeText(this, "No te olvides de la Duración!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txFechaInicio.Text))
            {
                Toast.MakeText(this, "No te olvides de la fecha de nacimiento!", ToastLength.Short).Show();
                return;
            }
            DateTime fNac;
            if (!DateTime.TryParseExact(txFechaInicio.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fNac))
            {
                Toast.MakeText(this, "No te olvides de la fecha de inicio!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txFrecuencia.Text))
            {
                Toast.MakeText(this, "No te olvides de la frecuencia!", ToastLength.Short).Show();
                return;
            }
            if (string.IsNullOrEmpty(txHoraInicio.Text))
            {
                Toast.MakeText(this, "No te olvides de la hora de inicio!", ToastLength.Short).Show();
                return;
            }


            try
            {
                mProgress = new ProgressDialog(this);
                mProgress.SetCancelable(false);
                mProgress.SetTitle("Guardando Indicaciones!");
                mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                mProgress.Indeterminate = true;
                mProgress.Show();

                RunOnUiThread(() => mProgress.SetMessage("Se está guardando la indicación, por favor esperá unos segundos.."));

                Indicacion indicacion = new Indicacion();
                indicacion.AnimalId = animalId;
                indicacion.Descripcion = txDescripcion.Text.Trim();
                indicacion.Duracion = Convert.ToInt32(txDuracion.Text.Trim());
                indicacion.FechaInicio = fNac;
                indicacion.Frecuencia = Convert.ToInt32(txFrecuencia.Text.Trim());
                indicacion.HoraInicio = Convert.ToInt32(txHoraInicio.Text.Trim());

                MiVetService.MiVetService ws = new MiVetService.MiVetService();
                ws.UpsertIndicacionCompleted += Ws_UpsertIndicacionCompleted;
                ws.UpsertIndicacionAsync(indicacion);

                btGuardar.Enabled = false;

                //Calendar cal = Calendar.GetInstance(Java.Util.TimeZone.Default);
                //Intent intent = new Intent(Intent.ActionEdit);
                //intent.SetType("vnd.android.cursor.item/event");
                //intent.PutExtra("beginTime", GetDateTimeMS(2016, 9, 22, 17, 0));
                //intent.PutExtra("allDay", true);
                ////intent.PutExtra("rrule", "FREQ=YEARLY");
                //intent.PutExtra("endTime", GetDateTimeMS(2016, 9, 22, 19, 0));
                //intent.PutExtra("title", "A Test Event from android app");
                //StartActivity(intent);


                //int calId = 0;

                //ContentValues eventValues = new ContentValues();

                //eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId,
                //    calId);
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.Title,
                //    "Recordatorio de MiVet!");
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.Description,
                //    "Lleva a bañar al perro!");
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart,
                //    GetDateTimeMS(2016, 9, 22, 17, 0));
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,
                //    GetDateTimeMS(2016, 9, 22, 19, 0));
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,
                //    "UTC");
                //eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,
                //    "UTC");

                //var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri,
                //    eventValues);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void Ws_UpsertIndicacionCompleted(object sender, UpsertIndicacionCompletedEventArgs e)
        {
            try
            {
                btGuardar.Enabled = true;
                mProgress.Dismiss();

                Indicacion indicacionGuardada = (Indicacion)e.Result;
                if (indicacionGuardada != null)
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

                    Toast.MakeText(this, "La indicación se guardó bien!", ToastLength.Short).Show();
                    this.Finish();
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

        //long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        //{
        //    Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);
        //    month--;
        //    c.Set(Calendar.DayOfMonth, day);
        //    c.Set(Calendar.HourOfDay, hr);
        //    c.Set(Calendar.Minute, min);
        //    c.Set(Calendar.Month, month);
        //    c.Set(Calendar.Year, yr);

        //    return c.TimeInMillis;
        //}
    }
}