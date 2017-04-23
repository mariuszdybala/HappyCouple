using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public static class MockedData
    {
        public static ShoppingList GetShoppingList(int id, string name,int addedBy, ShoppingListStatus status = ShoppingListStatus.Active)
        {
            return new ShoppingList
            {
                Id = id,
                AddDate = DateTime.Now,
                AddedById = addedBy,
                Status = status,
                Name = name
            };
        }

        public static Product GetProduct(string name, int addedBy, string comment, int shoppingListId, int productTypeID, int quantity = 1)
        {
            return new Product
            {
                Name = name,
                AddDate = DateTime.Now,
                AddedById = addedBy,
                Comment = comment,
                ProductTypeId = productTypeID,
                Quantity = quantity,
                ShoppingListId = shoppingListId
            };
        }

        public static ProductType GetProductType(string name, string iconResourceName)
        {
            return new ProductType
            {
                Type = name,
                IconName = iconResourceName
            };
        }
    }
}
