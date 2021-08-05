using System;
using System.Collections.Generic;
using dotnet_webapi.Models;

namespace dotnet_webapi.Repositories
{
    public interface IItemsRepository
    {
        Item GetItemAsync(Guid id);
        IEnumerable<Item> GetItemsAsync();

        void CreateItemAsync(Item item);
        void UpdateItemAsync(Item item);

        void DeleteItemAsync(Guid id);
    }
}