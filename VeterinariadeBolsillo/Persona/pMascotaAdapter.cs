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
using Android.Graphics;

namespace VeterinariadeBolsillo
{
    class pMascotaAdapter : BaseAdapter
    {
        Activity context;
        List<Animal> animales = new List<Animal>();

        public pMascotaAdapter(Activity context, List<Animal> animales)
        {
            this.context = context;
            this.animales = animales;
        }

        public override int Count
        {
            get
            {
                return animales.Count;
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
            var animal = animales[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // This gives us some performance gains by not always inflating a new view
            // This will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
            var view = (convertView ??
                       context.LayoutInflater.Inflate(
                           Resource.Layout.pRowMascota,
                           parent,
                           false)) as LinearLayout;

            //Find references to each subview in the list item's view
            var txNombre = view.FindViewById<TextView>(Resource.Id.txNombre);
            var txRaza = view.FindViewById<TextView>(Resource.Id.txRaza);
            var img = view.FindViewById<ImageView>(Resource.Id.img);

            txNombre.SetText(animal.Nombre, TextView.BufferType.Normal);
            txRaza.SetText(animal.Raza, TextView.BufferType.Normal);
            if (animal.Foto != null)
            {
                img.SetImageBitmap(BitmapFactory.DecodeByteArray(animal.Foto, 0, animal.Foto.Length));
            }
            //Finally return the view
            return view;
        }

        public Animal GetItemAtPosition(int position)
        {
            return animales[position];
        }
    }
}