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

namespace VeterinariadeBolsillo
{
    [Activity(Label = "vMascotaActivity")]
    public class vMascotaActivity : Activity
    {
        lMascota lMascota;
        lAnimal lAnimal;
        lVisita lVisita;
        DataManager dm = new DataManager();

        ImageView imgMascota;
        TextView txMascota;
        TextView txNombre;
        TextView txEspecie;
        TextView txRaza;
        TextView txFechaNacimiento;
        TextView txVisita;
        TextView txComentariosInternos;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vProximasVisitas);

            LayoutHelper.MakeTransparentToolBar(this);

            int lMascotaId = Intent.GetIntExtra("lMascotaId", 0);
            int lVisitaId = Intent.GetIntExtra("lVisitaId", 0);

            if (lMascotaId != 0 && lVisitaId != 0)
            {
                lMascota = dm.Get<lMascota>(x => x.Id == lMascotaId);
                lAnimal = dm.Get<lAnimal>(x => x.Id == lMascota.IdAnimal);
                lVisita = dm.Get<lVisita>(x => x.Id == lVisitaId);
            }
            else
            {
                Toast.MakeText(this, "No se encontró la mascota dentro de la base", ToastLength.Long);
            }

            imgMascota = FindViewById<ImageView>(Resource.Id.imgMascota);
            txMascota = FindViewById<TextView>(Resource.Id.txMascota);
            txNombre = FindViewById<TextView>(Resource.Id.txNombre);
            txEspecie = FindViewById<TextView>(Resource.Id.txEspecie);
            txRaza = FindViewById<TextView>(Resource.Id.txRaza);
            txFechaNacimiento = FindViewById<TextView>(Resource.Id.txFechaNacimiento);
            txVisita = FindViewById<TextView>(Resource.Id.txVisita);
            txComentariosInternos = FindViewById<TextView>(Resource.Id.txComentariosInternos);

            ImageView btProgramarVisita = FindViewById<ImageView>(Resource.Id.btProgramarVisita);
            btProgramarVisita.Click += BtProgramarVisita_Click;
            ImageView btIndicaciones = FindViewById<ImageView>(Resource.Id.btIndicaciones);
            btIndicaciones.Click += BtIndicaciones_Click;
            ImageView btProximasVisitas = FindViewById<ImageView>(Resource.Id.btProximasVisitas);
            btProximasVisitas.Click += BtProximasVisitas_Click;
            ImageView btEditarAnimal = FindViewById<ImageView>(Resource.Id.btEditarAnimal);
            btEditarAnimal.Click += BtEditarAnimal_Click;

            try
            {
                //ToDo: Setear la imagen que existe del animal, agregar una Raza en animal, agregar ComentariosInternos en Visita

                //set Image
                txMascota.SetText(txMascota.Text.Replace("@mascota", lAnimal.Nombre), TextView.BufferType.Normal);
                txNombre.SetText(lAnimal.Nombre, TextView.BufferType.Normal);
                txEspecie.SetText(lAnimal.Especie, TextView.BufferType.Normal);
                txRaza.SetText(lAnimal.Raza, TextView.BufferType.Normal);
                txFechaNacimiento.SetText(lAnimal.FechaNacimiento.ToString("dd/MM/yyyy"), TextView.BufferType.Normal);
                txVisita.SetText(lVisita.Actividad, TextView.BufferType.Normal);
                //txComentariosInternos.SetText(lAnimal.Nombre, TextView.BufferType.Normal);

            }
            catch (Exception ex)
            {
                //ToDo: enviar por email el error, crear pantalla graciosa diciendo que hubo un error
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private void BtEditarAnimal_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(vNuevoAnimalActivity));
            intent.PutExtra("lAnimalId", lMascota.IdAnimal);
            StartActivity(intent);
            this.Finish();
        }

        private void BtProximasVisitas_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtIndicaciones_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtProgramarVisita_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}