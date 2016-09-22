using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using VeterinariadeBolsillo.MiVetService;
using Newtonsoft.Json;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "VeterinariadeBolsillo", MainLauncher = true, Icon = "@drawable/Icono")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate 
            {
                Veterinaria vet = new Veterinaria();
                vet.Nombre = "vetadmin";
                vet.Password = "Password1";

                //Intent intent = new Intent(this, typeof(LoginUsuarioActivity));
                //intent.PutExtra("vet", JsonConvert.SerializeObject(vet));
                //StartActivity(intent);

                Intent intent = new Intent(this, typeof(vIndicacionActivity));
                intent.PutExtra("vet", JsonConvert.SerializeObject(vet));
                StartActivity(intent);

                //LinearLayout baseLinear = FindViewById<LinearLayout>(Resource.Id.baseLinear);
                //switch (count)
                //{
                //    case 1:
                //        baseLinear.SetBackgroundResource(Resource.Drawable.fondo1);
                //        break;
                //    case 2:
                //        baseLinear.SetBackgroundResource(Resource.Drawable.fondo2);
                //        break;
                //    case 3:
                //        baseLinear.SetBackgroundResource(Resource.Drawable.fondo3);
                //        break;
                //    case 4:
                //        baseLinear.SetBackgroundResource(Resource.Drawable.fondo4);
                //        break;
                //    default:
                //        count = 0;
                //        break;
                //}

                //count++;
            };
        }
    }
}

