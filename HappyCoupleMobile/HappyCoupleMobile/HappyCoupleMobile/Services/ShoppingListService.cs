using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyCoupleMobile.Enums;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;
using HappyCoupleMobile.Services.Interfaces;

namespace HappyCoupleMobile.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ISimpleAuthService _simpleAuthService;
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListService(ISimpleAuthService simpleAuthService, IShoppingListRepository shoppingListRepository)
        {
            _simpleAuthService = simpleAuthService;
            _shoppingListRepository = shoppingListRepository;
        }

        private ShoppingList CreateNewShoppingList(string newShoppingListName)
        {
            return new ShoppingList
            {
                AddDate = DateTime.UtcNow,
                AddedById = _simpleAuthService.Admin.Id,
                Name = newShoppingListName,
                Status = ShoppingListStatus.Active
            };
        }

        public async Task<ShoppingList> AddShoppingList(IList<ShoppingList> shoppingLists, string newShoppingListName)
        {
            var newShoppingList = CreateNewShoppingList(newShoppingListName);

            shoppingLists.Add(newShoppingList);

            await _shoppingListRepository.InsertShoppingListAsync(newShoppingList);

            return newShoppingList;
        }


    }
}