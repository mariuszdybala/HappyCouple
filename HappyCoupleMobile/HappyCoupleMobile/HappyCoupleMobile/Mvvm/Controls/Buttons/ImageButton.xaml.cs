using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyCoupleMobile.Helpers;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.Buttons
{
    public partial class ImageButton : Frame
    {
        public static BindableProperty ImageButtonClickedCommandProperty = BindableProperty.Create
            (nameof(ImageButtonClickedCommand), typeof(ICommand), typeof(ImageButton), null);

        public static BindableProperty ImageButtonCommandParameterProperty = BindableProperty.Create
            (nameof(ImageButtonCommandParameter), typeof(object), typeof(ImageButton));

        public static BindableProperty ButtonImageProperty = BindableProperty.Create
            (nameof(ButtonImage), typeof(FileImageSource), typeof(ImageButton));

        public static BindableProperty ButtonTextProperty = BindableProperty.Create
            (nameof(ButtonText), typeof(string), typeof(ImageButton));

        public ICommand ImageButtonClickedIntenalCommand { get; set; }

        public ICommand ImageButtonClickedCommand
        {
            get { return (ICommand)GetValue(ImageButtonClickedCommandProperty); }
            set { SetValue(ImageButtonClickedCommandProperty, value); }
        }

        public object ImageButtonCommandParameter
        {
            get { return (object)GetValue(ImageButtonCommandParameterProperty); }
            set { SetValue(ImageButtonCommandParameterProperty, value); }
        }

        public FileImageSource ButtonImage
        {
            get { return (FileImageSource)GetValue(ButtonImageProperty); }
            set { SetValue(ButtonImageProperty, value); }
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public ImageButton()
        {
            InitializeComponent();

            ImageButtonClickedIntenalCommand = new Command(async () => await OnImageButtonClickedIntenal());
        }

        private async Task OnImageButtonClickedIntenal()
        {
            await this.SetAnimation(0.95, 120);

            ImageButtonClickedCommand?.Execute(ImageButtonCommandParameter);
        }
    }
}
