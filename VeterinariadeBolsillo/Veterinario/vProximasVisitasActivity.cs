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
    [Activity(Label = "vProximasVisitasActivity")]
    public class vProximasVisitasActivity : Activity
    {
        CalendarView calendar;
        ListView lstVisitas;
        DataManager dm = new DataManager();

        List<Visita> visitas = new List<Visita>();
        List<Animal> animales = new List<Animal>();

        ProgressDialog mProgress;

        int vetId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //FillDataExample();

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vProximasVisitas);

            LayoutHelper.MakeTransparentToolBar(this);

            vetId = Intent.GetIntExtra("vetId", 0);

            //FillData();

            calendar = FindViewById<CalendarView>(Resource.Id.calendar);
            calendar.DateChange += Calendar_DateChange;
            lstVisitas = FindViewById<ListView>(Resource.Id.lstVisitas);
            lstVisitas.ItemClick += lstVisitas_ItemClick;
            FiltrarListaVisita(DateTime.Now);
        }

        private void lstVisitas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                Visita visitaSeleccionada = visitas[e.Position];
                Animal animalSeleccionado = animales.Find(x => x.Id == visitaSeleccionada.IdAnimal);

                Intent intent = new Intent(this, typeof(vMascotaActivity));

                intent.PutExtra("visita", JsonConvert.SerializeObject(visitaSeleccionada));
                intent.PutExtra("animal", JsonConvert.SerializeObject(animalSeleccionado));

                //intent.PutExtra("visitaId", visitaSeleccionada.Id);
                //intent.PutExtra("animalId", animalSeleccionado.Id);
                StartActivity(intent);

                this.Finish();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Short).Show();
            }
        }

        //private void FillData()
        //{
        //    DateTime semanaPasada = DateTime.Now;
        //    semanaPasada.AddDays(-7);

        //    //visitas = dm
        //    visitas = dm.GetTable<lVisita>().Where(x => x.Fecha > semanaPasada).ToList();
        //    animales = dm.GetTable<lAnimal>().ToList();
        //    //visitas = dm.GetTable<Visita>()
        //}

        //private void FillDataExample()
        //{
        //    lVisita visita;

        //    visita = new lVisita();
        //    visita.Id = 1;
        //    visita.IdAnimal = 1;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 02);
        //    visita.Actividad = "Cortar pelo";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 2;
        //    visita.IdAnimal = 2;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 02);
        //    visita.Actividad = "Desparacitar";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 3;
        //    visita.IdAnimal = 3;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 02);
        //    visita.Actividad = "Hacerle algo que no se me ocurre";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 4;
        //    visita.IdAnimal = 4;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 03);
        //    visita.Actividad = "Circuncidarlo";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 5;
        //    visita.IdAnimal = 5;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 03);
        //    visita.Actividad = "Bañar";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 6;
        //    visita.IdAnimal = 6;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 03);
        //    visita.Actividad = "Robarle un poco";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 7;
        //    visita.IdAnimal = 7;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 03);
        //    visita.Actividad = "Ya no tengo tanta imaginación";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 8;
        //    visita.IdAnimal = 8;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 04);
        //    visita.Actividad = "Cortar pelo";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 9;
        //    visita.IdAnimal = 9;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 08, 04);
        //    visita.Actividad = "Operarlo";
        //    visitas.Add(visita);

        //    visita = new lVisita();
        //    visita.Id = 10;
        //    visita.IdAnimal = 10;
        //    visita.IdVeterinaria = 1;
        //    visita.Fecha = new DateTime(2016, 07, 02);
        //    visita.Actividad = "Cortar pelo";
        //    visitas.Add(visita);

        //    lAnimal animal;

        //    animal = new lAnimal();
        //    animal.Id = 1;
        //    animal.Nombre = "Pichicho1";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 2;
        //    animal.Nombre = "Pichicho2";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 3;
        //    animal.Nombre = "Pichicho3";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 4;
        //    animal.Nombre = "Pichicho4";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 5;
        //    animal.Nombre = "Pichicho5";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 6;
        //    animal.Nombre = "Pichicho6";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 7;
        //    animal.Nombre = "Pichicho7";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 8;
        //    animal.Nombre = "Pichicho8";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 9;
        //    animal.Nombre = "Pichicho9";
        //    animales.Add(animal);

        //    animal = new lAnimal();
        //    animal.Id = 10;
        //    animal.Nombre = "Pichicho10";
        //    animales.Add(animal);
        //    //animal.Sexo = "Macho";
        //    //animal.Documento = "33998935";
        //    //animal.Especie = "Perro";
        //    //animal.FechaNacimiento = DateTime.Now;

        //}

        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            DateTime dt = new DateTime(e.Year, e.Month + 1, e.DayOfMonth);

            FiltrarListaVisita(dt);
        }

        private void FiltrarListaVisita(DateTime dt)
        {
            visitas.Clear();
            animales.Clear();
            lstVisitas.Adapter = null;

            //List<lVisita> visitasDelDia = visitas.Where(x => x.Fecha.Date == dt.Date).ToList();
            //lstVisitas.Adapter = new vVisitaAdapter(this, visitasDelDia, animales);

            MiVetService.MiVetService ws = new MiVetService.MiVetService();
            ws.GetVisitasDelDiaPorVeterinariaCompleted += Ws_GetVisitasDelDiaPorVeterinariaCompleted;
            ws.GetVisitasDelDiaPorVeterinariaAsync(vetId, dt);

            mProgress = new ProgressDialog(this);
            mProgress.SetCancelable(false);
            mProgress.SetTitle("Obteniendo Visitas");
            mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
            mProgress.Indeterminate = true;
            mProgress.Show();

            RunOnUiThread(() => mProgress.SetMessage("Aguarde un instante, buscando las visitas del día."));
        }

        private void Ws_GetVisitasDelDiaPorVeterinariaCompleted(object sender, GetVisitasDelDiaPorVeterinariaCompletedEventArgs e)
        {
            mProgress.Dismiss();

            try
            {
                List<VisitAnimal> visitAnimals = (List<VisitAnimal>)e.Result.ToList();
                if (visitAnimals != null)
                {
                    foreach (VisitAnimal visitAnimal in visitAnimals)
                    {
                        animales.Add(visitAnimal.Animal);
                        visitas.Add(visitAnimal.Visita);
                    }

                    lstVisitas.Adapter = new vVisitaAdapter(this, visitas, animales);

                    //try
                    //{
                    //    DataManager dm = new DataManager();
                    //    dm.InsertlAnimal(animalGuardado);
                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}

                    //Toast.MakeText(this, "El animal se guardó bien!", ToastLength.Short).Show();
                    //this.Finish();
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
    }
}