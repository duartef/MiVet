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
    [Activity(Label = "LoginUsuarioActivity")]
    public class LoginUsuarioActivity : Activity
    {
        EditText txUsuario;
        EditText txPassword;
        Button btLogin;

        protected void setStatusBarTranslucent(bool makeTranslucent)
        {
            if (makeTranslucent)
            {
                this.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
                //getWindow().addFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS);
            }
            else
            {
                this.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

                // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
                this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

                // finally change the color
                this.Window.SetStatusBarColor(Resources.GetColor(Resource.Color.my_purple));


                //this.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                //getWindow().clearFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginUsuario);

            txUsuario = FindViewById<EditText>(Resource.Id.txUsuario);
            txPassword = FindViewById<EditText>(Resource.Id.txPassword);
            btLogin = FindViewById<Button>(Resource.Id.btLogin);
            btLogin.Click += BtLogin_Click;
            setStatusBarTranslucent(true);
        }

        private void BtLogin_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txUsuario.Text) || string.IsNullOrEmpty(txPassword.Text))
            //{
            //    Toast.MakeText(this, "No te olvides de poner el Usuario y la Contraseña!", ToastLength.Short);
            //    return;
            //}
            try
            {
                btLogin.Enabled = false;
                Toast.MakeText(this, "Iniciando sesión...", ToastLength.Short).Show();

                MiVetService.MiVetService ws = new MiVetService.MiVetService();

                ws.LogInCompleted += Ws_LogInCompleted;
                ws.LogInAsync(txUsuario.Text, txPassword.Text);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                btLogin.Enabled = true;
            }

        }

        private void Ws_LogInCompleted(object sender, MiVetService.LogInCompletedEventArgs e)
        {
            //Intent intent = new Intent(this, typeof(vHomeActivity));
            //intent.PutExtra("vetId", 1);
            //intent.PutExtra("vetNombre", "Prueba");
            //StartActivity(intent);
            try
            {
                btLogin.Enabled = true;

                object resultado = e.Result;
                if (resultado.GetType() == typeof(Veterinaria))
                {
                    Intent intent = new Intent(this, typeof(vHomeActivity));
                    intent.PutExtra("vetId", ((Veterinaria)resultado).Id);
                    intent.PutExtra("vetNombre", ((Veterinaria)resultado).Nombre);
                    StartActivity(intent);
                }
                else if (resultado.GetType() == typeof(Persona))
                {
                    Intent intent = new Intent(this, typeof(pHomeActivity));
                    intent.PutExtra("personaId", ((Persona)resultado).Id);
                    intent.PutExtra("personaNombre", ((Persona)resultado).Nombre);
                    StartActivity(intent);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                btLogin.Enabled = true;
            }
            

        }
    }
}