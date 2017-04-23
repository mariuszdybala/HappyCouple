using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.VM
{
    public class ShoppingListVm : ViewModelBase
    {
        private ObservableCollection<ProductVm> _products;

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
            get { return _name; }
            set
            {
                Set(ref _name, value);
                ShoppingList.Name = value;
            }
        }

        public DateTime AddDate
        {
            get { return _addDate; }
            set
            {
                Set(ref _addDate, value);
                ShoppingList.AddDate = value;
            }
        }

        public ShoppingListStatus Status
        {
            get { return _status; }
            set
            {
                Set(ref _status, value);
                ShoppingList.Status = value;
            }
        }

        public string BoughtProductsCount
        {
            get { return _boughtProductsCount; }
            set { Set(ref _boughtProductsCount, value); }
        }

        public string LeftProductsCount
        {
            get { return _leftProductsCount; }
            set { Set(ref _leftProductsCount, value); }
        }

        public string ProgressPercent
        {
            get { return _progressPercent; }
            set { Set(ref _progressPercent, value); }
        }

        public bool ShowPlaceholder
        {
            get { return _showPlaceholder; }
            set { Set(ref _showPlaceholder, value); }
        }

        public int ProductsCount
        {
            get { return _productsCount; }
            set { Set(ref _productsCount, value); }
        }

        public ObservableCollection<ProductVm> Products
        {
            get { return _products; }
            set
            {
                Set(ref _products, value);
                ShowPlaceholder = !Products.Any();
                ProductsCount = Products.Count;
            }
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

            Products = new ObservableCollection<ProductVm>(productVms);
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
    }
}