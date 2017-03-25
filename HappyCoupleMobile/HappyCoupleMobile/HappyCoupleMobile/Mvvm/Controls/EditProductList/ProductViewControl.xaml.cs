using HappyCoupleMobile.Model;
using Xamarin.Forms;

namespace HappyCoupleMobile.Mvvm.Controls.EditProductList
{
    public partial class ProductViewControl : StackLayout
    {
        private Product _product;

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                SetData();
            }
        }

        public ProductViewControl()
        {
            InitializeComponent();
        }

        private void SetData()
        {
            CommentLabel.Text = Product.Comment;
            NameLabel.Text = Product.Name;
            QuantityLabel.Text = $"Ilosc: {Product.Quantity}";
        }
    }
}
