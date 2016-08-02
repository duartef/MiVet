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
using MiVet.MiVetService;

namespace MiVet
{
    [Activity(Label = "vVisitasActivity")]
    public class vVisitasActivity : Activity
    {
        CalendarView calendar;
        ListView lstVisitas;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vVisitas);

            calendar = FindViewById<CalendarView>(Resource.Id.calendar);
            calendar.DateChange += Calendar_DateChange;
            lstVisitas = FindViewById<ListView>(Resource.Id.lstVisitas);
            lstVisitas.Click += LstVisitas_Click;
        }

        private void LstVisitas_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Enviar al perfíl de la mascota", ToastLength.Short).Show();
        }

        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            
        }
    }
}