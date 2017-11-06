using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using HappyCoupleMobile.Custom;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.VM
{
    public class ShoppingListVm : ViewModelBase
    {
        private ObservableCollection<ProductType> _productTypes;
        private string _name;
        private DateTime _addDate;
        private ShoppingListStatus _status;

        private string _boughtProductsCount;
        private string _leftProductsCount;
        private string _progressPercent;
        private bool _showPlaceholder;
        private int _productsCount;
	    private bool _isListCompleted;
	    private DateTime? _closeDate;
	    private DateTime? _editDate;

	    public event Action<OperationMode> ProductChanged;

        public ShoppingList ShoppingList { get; set; }

        public int Id => ShoppingList.Id;

        public string Name
        {
            get => _name;
	        set
            {
                Set(ref _name, value);
                ShoppingList.Name = value;
            }
        }

        public DateTime AddDate
        {
            get => _addDate;
	        set
            {
                Set(ref _addDate, value);
                ShoppingList.AddDate = value;
            }
        }
	    
	    public DateTime? CloseDate
	    {
		    get => _closeDate;
		    set
		    {
			    Set(ref _closeDate, value);
			    ShoppingList.CloseDate = value;
		    }
	    }
	    
	    public DateTime? EditDate
	    {
		    get => _editDate;
		    set
		    {
			    Set(ref _editDate, value);
			    ShoppingList.EditDate = value;
		    }
	    }

        public ShoppingListStatus Status
        {
            get => _status;
	        set
            {
                Set(ref _status, value);
                ShoppingList.Status = value;
            }
        }

        public string BoughtProductsCount
        {
            get => _boughtProductsCount;
	        set => Set(ref _boughtProductsCount, value);
        }

        public string LeftProductsCount
        {
            get => _leftProductsCount;
	        set => Set(ref _leftProductsCount, value);
        }

        public string ProgressPercent
        {
            get => _progressPercent;
	        set => Set(ref _progressPercent, value);
        }

	    public bool IsListCompleted
	    {
		    get => _isListCompleted;
		    set
		    {
			    Set(ref _isListCompleted, value);
		    }
	    }

        public bool ShowPlaceholder
        {
            get => _showPlaceholder;
	        set => Set(ref _showPlaceholder, value);
        }

        public int ProductsCount
        {
            get => _productsCount;
	        set => Set(ref _productsCount, value);
        }

	    public List<ProductVm> Products { get; set; }

        public ObservableCollection<ProductType> ProductTypes
        {
            get => _productTypes;
	        set => Set(ref _productTypes, value);
        }

        public ShoppingListVm(ShoppingList shoppingList)
        {
            ShoppingList = shoppingList;
	        Products = ShoppingList.Products.Select(x=>new ProductVm(x)).ToList();
            Name = ShoppingList.Name;
            AddDate = ShoppingList.AddDate;
	        CloseDate = ShoppingList.CloseDate;
            Status = ShoppingList.Status;
	        EditDate = shoppingList.EditDate;

	        UpdateAdditionalData();
        }

	    public void CalculateCurrentShoppingProgress()
        {
            var totalProductsCountValue = Products.Count;
            var boughtProductsCountValue = Products.Count(x => x.ProductModel.IsBought);
            var leftProductsCountValue = Products.Count(x => !x.ProductModel.IsBought);

            BoughtProductsCount = boughtProductsCountValue.ToString();
            LeftProductsCount = leftProductsCountValue.ToString();

            var progressPercent = leftProductsCountValue == 0 ? 100 : boughtProductsCountValue * 100 / totalProductsCountValue;
	        IsListCompleted = leftProductsCountValue == 0 && totalProductsCountValue > 0;

            ProgressPercent = progressPercent.ToString();
        }

        public void UpdateAdditionalData()
        {
            CalculateCurrentShoppingProgress();
            ShowPlaceholder = !Products.Any();
            ProductsCount = Products.Count;
            UpdateProductTypeList();
        }

        private void UpdateProductTypeList()
        {
            ProductTypes = new ObservableCollection<ProductType>(Products.Select(x=>x.ProductType).Distinct(new ProductTypeEqualityComparer()));
            //RaisePropertyChanged(() => ProductTypes);
        }

	    public void UpdateProducts(IList<ProductVm> products, bool withNotification = true)
	    {
		    if (!products.Any())
		    {
			    return;
		    }
		   		    
		    foreach (var editedProduct in products)
		    {
			    foreach (var product in Products)
			    {
				    if (product.FavouriteProductId != editedProduct.Id)
				    {
					    continue;
				    }

				    product.Comment = editedProduct.Comment;
				    product.Name = editedProduct.Name;
			    }
		    }

		    if (withNotification)
		    {
			    InvokeProductChanged(OperationMode.Update);
		    }
	    }

	    public void InvokeProductChanged(OperationMode operationMode)
	    {
		    ProductChanged?.Invoke(operationMode);
	    }

	    public void AddProducts(IList<ProductVm> products, bool withNotification = true)
	    {
		    Products.AddRange(products);
		    UpdateAdditionalData();

		    if (withNotification)
		    {
			    InvokeProductChanged(OperationMode.InsertNew);
		    }
	    }

        public void DeleteProduct(ProductVm product)
        {
	        Products.Remove(product);
            UpdateAdditionalData();
        }
	    
	    public static ShoppingListVm CreateNewShoppingList(string newShoppingListName, int userId)
	    {
		    var shoppingList = new ShoppingList
		    {
			    AddDate = DateTime.UtcNow,
			    AddedById = userId,
			    Name = newShoppingListName,
			    Status = ShoppingListStatus.Active
		    };
		    
		    return new ShoppingListVm(shoppingList);
	    }
    }
}
