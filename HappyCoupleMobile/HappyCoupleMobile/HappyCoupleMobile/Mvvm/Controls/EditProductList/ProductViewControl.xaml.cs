using System;
using HappyCoupleMobile.Mvvm.Controls.ContextMenu;
using HappyCoupleMobile.VM;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductViewControl : ContextMenuLayout
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
        public Command DeleteProductCheckedCommand => new Command(OnDeleteProduct);

		public override ContextMenuView ContextMenu => ProductContextMenu;
        public override Xamarin.Forms.View DataContent => ProductData;

        public ProductViewControl()
        {
            InitializeComponent();

            ProductCheckedCommand = new Command(OnProductChecked);
            SelectProductCommand = new Command(OnSelectProduct);
            EditProductCheckedCommand = new Command(OnEditProduct);
        }

		public override void OnTapInternal()
		{
			ControlPanelInvoked?.Invoke(Product);
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
            SelectMenuItem.IsVisible = false;
        }

        public void HideEditControlItem()
        {
            EditMenuItem.IsVisible = false;
        }

        public void HideCheckbox()
        {
            IsBoughtCheckbox.IsVisible = false;
        }

        public void HideControlPanel()
        {
            CloseMenu();
        }
    }
}
