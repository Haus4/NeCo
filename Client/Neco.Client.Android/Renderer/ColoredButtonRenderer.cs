using Xamarin.Forms.Platform.Android;
using Neco.Client.Droid;
using Android.Graphics;
using Xamarin.Forms;
using Neco.Client.Control;

[assembly: ExportRenderer(typeof(ColoredButton), typeof(ColoredButtonRenderer))]
namespace Neco.Client.Droid
{
    public class ColoredButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ColoredButton control)
            {
                if (control.Color != Xamarin.Forms.Color.Default)
                {
                    var androidColor = control.Color.ToAndroid();
                    Control.Background.SetColorFilter(androidColor, PorterDuff.Mode.Src);
                }
            }
        }
    }
}
