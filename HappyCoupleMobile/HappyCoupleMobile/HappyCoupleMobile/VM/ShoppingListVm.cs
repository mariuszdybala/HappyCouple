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
        private ObservableCollection<GroupedProductList> _groupedProducts;
        private ObservableCollection<ProductType> _productTypes;

        private string _name;
        private DateTime _addDate;
        private ShoppingListStatus _status;

        private string _boughtProductsCount;
        private string _leftProductsCount;
        private string _progressPercent;
        private bool _showPlaceholder;
        private int _productsCount;

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

        public ObservableCollection<GroupedProductList> GroupedProducts
        {
            get => _groupedProducts;
	        set
            {
                Set(ref _groupedProducts, value);
                UpdateAdditionalData();
            }
        }

	    public IList<ProductVm> Products => GroupedProducts.SelectMany(x=>x).ToList();

        public ObservableCollection<ProductType> ProductTypes
        {
            get => _productTypes;
	        set => Set(ref _productTypes, value);
        }

        public ShoppingListVm(ShoppingList shoppingList)
        {
            ShoppingList = shoppingList;
            Name = ShoppingList.Name;
            AddDate = ShoppingList.AddDate;
            Status = ShoppingList.Status;

            InitializeProducts();
            CalculateCurrentShoppingProgress();
        }

        private void InitializeProducts()
        {
            var productVms = ShoppingList.Products.Select(x => new ProductVm(x));

	        var groupedData = productVms.OrderBy(x => x.ProductType.IconName)
		        .GroupBy(x => x.ProductType)
		        .Select(x => new GroupedProductList(x))
		        .ToList();

	        GroupedProducts = new ObservableCollection<GroupedProductList>(groupedData);
        }

	    public void CalculateCurrentShoppingProgress()
        {
            var totalProductsCountValue = Products.Count;
            var boughtProductsCountValue = Products.Count(x => x.ProductModel.IsBought);
            var leftProductsCountValue = Products.Count(x => !x.ProductModel.IsBought);

            BoughtProductsCount = boughtProductsCountValue.ToString();
            LeftProductsCount = leftProductsCountValue.ToString();

            var progressPercent = leftProductsCountValue == 0 ? 100 : boughtProductsCountValue * 100 / totalProductsCountValue;

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
            ProductTypes = new ObservableCollection<ProductType>(GroupedProducts.Select(x=>x.ProductType));
            RaisePropertyChanged(() => ProductTypes);
        }

        public void AddProduct(ProductVm product)
        {
	        if (GroupedProducts.All(x => x.ProductType.Id != product.ProductType.Id))
	        {
		        GroupedProducts.Add(new GroupedProductList(product.ProductType));
	        }

	        GroupedProducts.FirstOrDefault(x=>x.ProductType.Id == product.ProductType.Id).Add(product);

            UpdateAdditionalData();
        }

        public void DeleteProduct(ProductVm product)
        {
	        var productGroup = GroupedProducts.FirstOrDefault(x => x.ProductType == product.ProductType);

	        if (productGroup == null)
	        {
		        return;
	        }

	        productGroup.Remove(product);

	        if (!productGroup.Any())
	        {
		        GroupedProducts.Remove(productGroup);
	        }

            UpdateAdditionalData();
        }
    }
}
