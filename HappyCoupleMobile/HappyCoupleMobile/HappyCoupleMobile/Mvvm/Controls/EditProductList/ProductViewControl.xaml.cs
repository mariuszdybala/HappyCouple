using System;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductViewControl : StackLayout
    {
        public event Action<ProductVm> Checked;
        public event Action<ProductVm> DeleteButtonClick;
        public event Action<ProductVm> EditButtonClick;
        public event Action<ProductVm> SelectProductButtonClick;
        public event Action<ProductVm> ControlPanelInvoked;

        public static BindableProperty ProductProperty = BindableProperty
        .Create(nameof(Product), typeof(ProductVm), typeof(ProductViewControl), null);

        public ProductVm Product
        {
            get { return (ProductVm)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        public Command ProductCheckedCommand { get; set; }
        public Command SelectProductCommand { get; set; }
        public Command EditProductCheckedCommand { get; set; }
        public Command DeleteProductCheckedCommand { get; set; }
        public Command ProductItemTappedCommand { get; set; }

        public ProductViewControl()
        {
            InitializeComponent();

            ProductCheckedCommand = new Command(OnProductChecked);
            SelectProductCommand = new Command(OnSelectProduct);
            EditProductCheckedCommand = new Command(OnEditProduct);
            DeleteProductCheckedCommand = new Command(OnDeleteProduct);
            ProductItemTappedCommand = new Command(OnProductItemTapped);
        }

        private void OnProductItemTapped()
        {
            ControlPanel.IsVisible = !ControlPanel.IsVisible;

            if (ControlPanel.IsVisible)
            {
                ControlPanelInvoked?.Invoke(Product);
            }
        }

        private void OnDeleteProduct()
        {
            DeleteButtonClick?.Invoke(Product);
        }

        private void OnEditProduct()
        {
            EditButtonClick?.Invoke(Product);
        }

        private void OnSelectProduct()
        {
            SelectProductButtonClick?.Invoke(Product);
        }

        private void OnProductChecked()
        {
            Checked?.Invoke(Product);
        }

        public void HideSelectControlItem()
        {
            AddStack.IsVisible = false;
        }

        public void HideEditControlItem()
        {
            EditStack.IsVisible = false;
        }

        public void HideCheckbox()
        {
            IsBoughtCheckbox.IsVisible = false;
        }

        public void HideControlPanel()
        {
            ControlPanel.IsVisible = false;
        }
    }
}
