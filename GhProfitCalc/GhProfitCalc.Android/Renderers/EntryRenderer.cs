using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using GhProfitCalc.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace GhProfitCalc.Droid.Renderers
{
    /// <summary>
    /// For Android we use a custom renderer to enable people to use the period/comma
    /// symbol for Numeric keyboards. It's only a problem on Samsung's custom keyboards.
    /// </summary>
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {
        }

        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            this.Control.KeyListener = DigitsKeyListener.GetInstance("1234567890,.");
        }
    }
}