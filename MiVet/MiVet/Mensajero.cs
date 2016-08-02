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

namespace MiVet
{
    public static class Mensajero
    {
        public static void MostrarMensaje(Context context, string title, string message)
        {
            Activity activity = (Activity)context;
            activity.RunOnUiThread(() =>
            {
                AlertDialog alert;
                AlertDialog.Builder dialog = new AlertDialog.Builder(context)
                .SetTitle(title)
                .SetMessage(message)
                .SetPositiveButton("Aceptar", delegate
                {
                });

                alert = dialog.Create();
                alert.Show();
            });
        }
    }
}