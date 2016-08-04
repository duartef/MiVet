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
    [Activity(Label = "vVisitasActivity", MainLauncher = true, Icon = "@drawable/Logo")]
    public class vVisitasActivity : Activity
    {
        CalendarView calendar;
        ListView lstVisitas;

        List<Visita> visitas = new List<Visita>();
        List<Animal> animales = new List<Animal>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            FillDataExample();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vVisitas);

            calendar = FindViewById<CalendarView>(Resource.Id.calendar);
            calendar.DateChange += Calendar_DateChange;
            lstVisitas = FindViewById<ListView>(Resource.Id.lstVisitas);
            //lstVisitas.ItemClick += LstVisitas_Click;

            calendar.SetDate(DateTime.Now.Date.Ticks, false, false);

        }

        private void FillDataExample()
        {
            Visita visita;
            
            visita = new Visita();
            visita.Id = 1;
            visita.IdAnimal = 1;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 02);
            visita.Actividad = "Cortar pelo";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 2;
            visita.IdAnimal = 2;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 02);
            visita.Actividad = "Desparacitar";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 3;
            visita.IdAnimal = 3;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 02);
            visita.Actividad = "Hacerle algo que no se me ocurre";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 4;
            visita.IdAnimal = 4;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 03);
            visita.Actividad = "Circuncidarlo";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 5;
            visita.IdAnimal = 5;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 03);
            visita.Actividad = "Bañar";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 6;
            visita.IdAnimal = 6;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 03);
            visita.Actividad = "Robarle un poco";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 7;
            visita.IdAnimal = 7;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 03);
            visita.Actividad = "Ya no tengo tanta imaginación";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 8;
            visita.IdAnimal = 8;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 04);
            visita.Actividad = "Cortar pelo";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 9;
            visita.IdAnimal = 9;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 08, 04);
            visita.Actividad = "Operarlo";
            visitas.Add(visita);

            visita = new Visita();
            visita.Id = 10;
            visita.IdAnimal = 10;
            visita.IdVeterinaria = 1;
            visita.Fecha = new DateTime(2016, 07, 02);
            visita.Actividad = "Cortar pelo";
            visitas.Add(visita);

            Animal animal;

            animal = new Animal();
            animal.Id = 1;
            animal.Nombre = "Pichicho1";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 2;
            animal.Nombre = "Pichicho2";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 3;
            animal.Nombre = "Pichicho3";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 4;
            animal.Nombre = "Pichicho4";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 5;
            animal.Nombre = "Pichicho5";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 6;
            animal.Nombre = "Pichicho6";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 7;
            animal.Nombre = "Pichicho7";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 8;
            animal.Nombre = "Pichicho8";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 9;
            animal.Nombre = "Pichicho9";
            animales.Add(animal);

            animal = new Animal();
            animal.Id = 10;
            animal.Nombre = "Pichicho10";
            animales.Add(animal);
            //animal.Sexo = "Macho";
            //animal.Documento = "33998935";
            //animal.Especie = "Perro";
            //animal.FechaNacimiento = DateTime.Now;

        }

        private void LstVisitas_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Enviar al perfíl de la mascota", ToastLength.Short).Show();
        }

        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            //e.DayOfMonth;
            //e.Month;
            //e.Year;
            FiltrarListaVisita();
        }

        private void FiltrarListaVisita()
        {
            lstVisitas.Adapter = null;

            List<Visita> visitasDelDia = visitas.Where(x => x.Fecha.Date == new DateTime(calendar.Date).Date).ToList();
            lstVisitas.Adapter = new vVisitaAdapter(this, visitasDelDia, animales);
        }
    }
}