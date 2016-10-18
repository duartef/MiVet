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
using Newtonsoft.Json;
using VeterinariadeBolsillo.MiVetService;

namespace VeterinariadeBolsillo
{
    [Activity(Label = "pHome")]
    public class pHome : Activity
    {
        Persona persona;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pHome);
            LayoutHelper.MakeTransparentToolBar(this);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            //Toolbar will now take on default Action Bar characteristics
            //toolbar.OverflowIcon = GetDrawable(Resource.Drawable.LogoChico);

            SetActionBar(toolbar);
            //You can now use and reference the ActionBar
            ActionBar.Title = "Mi Veterinaria!";
            toolbar.SetNavigationIcon(Resource.Drawable.LogoChico);

            try
            {
                persona = JsonConvert.DeserializeObject<Persona>(Intent.GetStringExtra("persona"));
                if (persona != null)
                {
                    //txNombre.SetText(txNombre.Text.Replace("@nombre", persona.Nombre), TextView.BufferType.Normal);

                    //mProgress = new ProgressDialog(this);
                    //mProgress.SetCancelable(false);
                    //mProgress.SetTitle("Recordando cuales eran tus mascotas...");
                    //mProgress.SetProgressStyle(ProgressDialogStyle.Spinner);
                    //mProgress.Indeterminate = true;
                    //mProgress.Show();

                    //MiVetService.MiVetService ws = new MiVetService.MiVetService();
                    //ws.GetMascotasCompleted += Ws_GetMascotasCompleted;
                    //ws.GetMascotasAsync(persona.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            MenuInflater inflater = this.MenuInflater;

            inflater.Inflate(Resource.Menu.menu, menu);

            return true;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);

            switch (item.ItemId)
            {
                case Resource.Id.agregarMascota:
                    Intent intent = new Intent(this, typeof(pAgregarMascotaActivity));
                    intent.PutExtra("persona", JsonConvert.SerializeObject(persona));
                    StartActivity(intent);
                    break;

                case Resource.Id.misIndicaciones:
                    Toast.MakeText(this, "En desarrollo", ToastLength.Short).Show();
                    break;

                case Resource.Id.misTurnos:
                    Toast.MakeText(this, "En desarrollo", ToastLength.Short).Show();
                    break;

                default:
                    break;
            }

            return true;
        }
    }
}