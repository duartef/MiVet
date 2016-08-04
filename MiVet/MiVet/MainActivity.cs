using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MiVet
{
    [Activity(Label = "Prueba")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            string usuario = "San Jorge";
            Intent intent = new Intent(this, typeof(HomeActivity));
            intent.PutExtra("vetId", 1);
            intent.PutExtra("vetNombre", usuario);
            StartActivity(intent);
        }
    }
}

