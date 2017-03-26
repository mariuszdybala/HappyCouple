using System;
using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductViewControl : StackLayout
    {
        public event Action<Product> Checked;
        public event Action<Product> Delete;
        public event Action<Product> Edit;
        public event Action<Product> Add;
        public event Action<Product> ControlPanelInvoked;

        public static BindableProperty ProductProperty = BindableProperty
        .Create(nameof(Product), typeof(Product), typeof(ProductViewControl), null);

        public Product Product
        {
            get { return (Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        public Command ProductCheckedCommand { get; set; }
        public Command AddProductCommand { get; set; }
        public Command EditProductCheckedCommand { get; set; }
        public Command DeleteProductCheckedCommand { get; set; }
        public Command ProductItemTappedCommand { get; set; }

        public ProductViewControl()
        {
            InitializeComponent();

            ProductCheckedCommand = new Command(OnProductChecked);
            AddProductCommand = new Command(OnAddProduct);
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
            Delete?.Invoke(Product);
        }

        private void OnEditProduct()
        {
            Edit?.Invoke(Product);
        }

        private void OnAddProduct()
        {
            Add?.Invoke(Product);
        }

        private void OnProductChecked()
        {
            Checked?.Invoke(Product);
        }

        public void HideAddControlItem()
        {
            AddStack.IsVisible = false;
        }

        public void HideControlPanel()
        {
            ControlPanel.IsVisible = false;
        }
    }
}
