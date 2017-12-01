using Xamarin.Forms;

namespace Neco.Client.Control
{
    class ColoredButton : Button
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(Button), (object)Color.Default, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
    }
}
