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

namespace VeterinariadeBolsillo
{
    class vVisitaAdapter : BaseAdapter
    {
        Activity context;
        List<Visita> visitas = new List<Visita>();
        List<Animal> animales = new List<Animal>();

        public vVisitaAdapter(Activity context, List<Visita> visitas, List<Animal> animales)
        {
            this.context = context;
            this.visitas = visitas;
            this.animales = animales;
        }

        public override int Count
        {
            get
            {
                return visitas.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Get our object for this position
            var visita = visitas[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // This gives us some performance gains by not always inflating a new view
            // This will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
            var view = (convertView ??
                       context.LayoutInflater.Inflate(
                           Resource.Layout.vVisitaView,
                           parent,
                           false)) as LinearLayout;

            //Find references to each subview in the list item's view
            var txNombre = view.FindViewById<TextView>(Resource.Id.txNombre);
            var txActividad = view.FindViewById<TextView>(Resource.Id.txActividad);
            Animal animal = animales.Find(x => x.Id == visita.IdAnimal);
            txNombre.SetText(animal.Nombre, TextView.BufferType.Normal);
            txActividad.SetText(visita.Actividad, TextView.BufferType.Normal);

            //Finally return the view
            return view;
        }

        public Visita GetItemAtPosition(int position)
        {
            return visitas[position];
        }
    }
}