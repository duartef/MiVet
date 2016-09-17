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
    public static class LayoutHelper
    {
        public static void MakeTransparentToolBar(Activity context)
        {
            context.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
        }
    }
}