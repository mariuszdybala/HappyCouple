using System.Collections.Generic;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Custom
{
	public class ProductTypeEqualityComparer : IEqualityComparer<ProductType>
	{
		public bool Equals(ProductType x, ProductType y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(ProductType productType)
		{
			return productType.Id.GetHashCode();
		}
	}
}
