using System;
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
		    get => _favouriteProductId;
		    set { Set(ref _favouriteProductId, value);
			    ProductModel.FavouriteProductId = value;
		    }
	    }
	    
        private string _name;
        public string Name
        {
            get => _name;
	        set { Set(ref _name, value);
                ProductModel.Name = value;
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
	        set
            {
                Set(ref _comment, value);
                ProductModel.Comment = value;
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
	        set
            {
                Set(ref _quantity, value);
                ProductModel.Quantity = value;
            }
        }

        private bool _isBought;
        public bool IsBought
        {
            get => _isBought;
	        set
            {
                Set(ref _isBought, value);
                ProductModel.IsBought = value;
            }
        }
	    
	    private int? _shoppingListId;
	    public int? ShoppingListId
	    {
		    get => _shoppingListId;
		    set
		    {
			    Set(ref _shoppingListId, value);
			    ProductModel.ShoppingListId = value;
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
	        ShoppingListId = ProductModel.ShoppingListId;
        }
	    
	    public static ProductVm CreateProductVm(string name, string comment, int quantity, ProductType productType, User user, int? favouriteProductId = null, bool isFavourite = true, int? shoppingListId =null)
	    {
		    var product = new Product();

		    product.Name = name;
		    product.Comment = comment;
		    product.Quantity = quantity;
		    product.AddDate = DateTime.UtcNow;
		    product.AddedById = user.Id;
		    product.ProductType = productType;
		    product.ProductTypeId = productType.Id;
		    product.IsFavourite = isFavourite;
		    product.FavouriteProductId = favouriteProductId;
		    product.ShoppingListId = shoppingListId;

		    return new ProductVm(product);
	    }
	    
	    public static ProductVm CreateProductVm(string name, string comment)
	    {
		    var product = new Product();
		    product.Name = name;
		    product.Comment = comment;
		    return new ProductVm(product);
	    }

	    public static ProductVm CreateProductVmFromFavouriteProduct(ProductVm favouriteProduct, ProductType productType, int quantity, User user, int? shoppingListId)
	    {
		    return CreateProductVm(favouriteProduct.Name, favouriteProduct.Comment, quantity, productType, user,favouriteProduct.Id, false, shoppingListId);
	    }
    }
}