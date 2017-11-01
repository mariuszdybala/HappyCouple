using GalaSoft.MvvmLight;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.VM
{
    public class ProductVm : ViewModelBase
    {
        public Product ProductModel { get; }

        public int Id => ProductModel.Id;
        public ProductType ProductType => ProductModel.ProductType;

	    private int? _favouriteProductId;
	    public int? FavouriteProductId
	    {
		    get { return _favouriteProductId; }
		    set { Set(ref _favouriteProductId, value);
			    ProductModel.FavouriteProductId = value;
		    }
	    }
	    
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value);
                ProductModel.Name = value;
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                Set(ref _comment, value);
                ProductModel.Comment = value;
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                Set(ref _quantity, value);
                ProductModel.Quantity = value;
            }
        }

        private bool _isBought;
        public bool IsBought
        {
            get { return _isBought; }
            set
            {
                Set(ref _isBought, value);
                ProductModel.IsBought = value;
            }
        }


        public ProductVm(Product product)
        {
            ProductModel = product;

            Name = ProductModel.Name;
            Comment = ProductModel.Comment;
            Quantity = ProductModel.Quantity;
            IsBought = ProductModel.IsBought;
	        FavouriteProductId = ProductModel.FavouriteProductId;
        }
    }
}