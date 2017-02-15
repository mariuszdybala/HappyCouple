using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.ToggleImages
{
    public abstract class ToggleImage : TappableImage
    {
        public abstract string PrimaryImageKeyInResources { get; }
        public abstract string SecondaryImageKeyInResources { get; }

        public event Action<bool> Toggled;

        public static BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(ToggleImage), false, BindingMode.TwoWay, propertyChanged: OnIsToggledChanded);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        protected ToggleImage()
        {
            SetCheckboxImageSource(false);
        }

        private void SetCheckboxImageSource(bool isToogled)
        {
            string imageKeyInResources = isToogled ? PrimaryImageKeyInResources : SecondaryImageKeyInResources;

            var imageSource = Application.Current.Resources[imageKeyInResources] as FileImageSource;

            if (imageSource == null)
            {
                throw new ArgumentNullException(nameof(imageSource), $"Key for resource {imageKeyInResources} does't exist in resources directory.");
            }

            Source = imageSource;
        }

        private static void OnIsToggledChanded(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null || newValue == oldValue)
            {
                return;
            }

            var isToggled = (bool)newValue;
            var checkbox = (ToggleImage)bindable;

            checkbox.SetCheckboxImageSource(isToggled);
            checkbox.Toggled?.Invoke(isToggled);
        }

        public override Task OnImageTapped()
        {
            IsToggled = !IsToggled;

            return base.OnImageTapped();
        }
    }
}