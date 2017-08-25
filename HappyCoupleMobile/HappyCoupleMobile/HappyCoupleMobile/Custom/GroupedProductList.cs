using System.Collections.ObjectModel;
using System.Linq;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.VM;

namespace HappyCoupleMobile.Custom
{
	public class GroupedProductList : ObservableCollection<ProductVm>
	{
		public ProductType ProductType { get; }

		public GroupedProductList(ProductType productType)
		{
			ProductType = productType;
		}

		public GroupedProductList(IGrouping<ProductType, ProductVm> group) : base(group)
		{
			ProductType = group.Key;
		}
	}
}
