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

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vIndicacionActivity")]
    public class vIndicacionActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.vIndicacion);
            Button btIndicacion = FindViewById<Button>(Resource.Id.btIndicacion);
            btIndicacion.Click += BtIndicacion_Click;
        }

        private void BtIndicacion_Click(object sender, EventArgs e)
        {
            try
            {
                Calendar cal = Calendar.GetInstance(Java.Util.TimeZone.Default);
                Intent intent = new Intent(Intent.ActionEdit);
                intent.SetType("vnd.android.cursor.item/event");
                intent.PutExtra("beginTime", GetDateTimeMS(2016, 9, 22, 17, 0));
                intent.PutExtra("allDay", true);
                //intent.PutExtra("rrule", "FREQ=YEARLY");
                intent.PutExtra("endTime", GetDateTimeMS(2016, 9, 22, 19, 0));
                intent.PutExtra("title", "A Test Event from android app");
                StartActivity(intent);


                //int _calId = 0;

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

                throw;
            }
        }

        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, day);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, Calendar.December);
            c.Set(Calendar.Year, 2011);

            return c.TimeInMillis;
        }
    }
}