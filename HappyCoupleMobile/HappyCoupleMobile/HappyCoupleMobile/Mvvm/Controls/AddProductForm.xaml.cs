using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HappyCoupleMobile.Helpers;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls
{
    public partial class AddProductForm : StackLayout
    {
        public static BindableProperty ProductNameProperty = BindableProperty.Create
        (nameof(ProductName), typeof(string), typeof(AddProductForm), defaultBindingMode:BindingMode.TwoWay);

        public static BindableProperty ProductCommentProperty = BindableProperty.Create
        (nameof(ProductComment).GetBindableName(), typeof(string), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty ProductQuantityProperty = BindableProperty.Create
        (nameof(ProductQuantity), typeof(string), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty AddToFavoriteCommandProperty = BindableProperty.Create
            (nameof(AddToFavoriteCommandProperty).GetBindableName(), typeof(ICommand), typeof(AddProductForm));

        public static BindableProperty SelectedProductTypeProperty = BindableProperty.Create
            (nameof(SelectedProductType), typeof(ProductType), typeof(AddProductForm), defaultBindingMode: BindingMode.TwoWay);

        public ProductType SelectedProductType
        {
            get { return (ProductType)GetValue(SelectedProductTypeProperty); }
            set { SetValue(SelectedProductTypeProperty, value); }
        }

        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        public string ProductQuantity
        {
            get { return (string)GetValue(ProductQuantityProperty); }
            set { SetValue(ProductQuantityProperty, value); }
        }

        public string ProductComment
        {
            get { return (string)GetValue(ProductCommentProperty); }
            set { SetValue(ProductCommentProperty, value); }
        }

        public ICommand AddToFavoriteCommand
        {
            get { return (ICommand)GetValue(AddToFavoriteCommandProperty); }
            set { SetValue(AddToFavoriteCommandProperty, value); }
        }

        public ICommand EraseEntryCommand => new Command<Entry>(OnEraseEntry);


        public AddProductForm()
        {
            InitializeComponent();
        }

        private void OnEraseEntry(Entry entry)
        {
            entry.Text = string.Empty;
        }

        private void OnProductAddedToFavourite(bool isToggled)
        {
            AddToFavoriteCommand?.Execute(isToggled);
        }

        private void OnNewNameEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewNameEraseImage == null)
            {
                return;
            }

            NewNameEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private void OnQuantityEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuantitytEraseImage == null)
            {
                return;
            }

            QuantitytEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private void OnDescriptionEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (DescriptionEraseImage == null)
            {
                return;
            }

            DescriptionEraseImage.IsVisible = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }
    }
}
