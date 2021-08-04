using System;
using System.Collections.Generic;
using dotnet_webapi.Models;

namespace dotnet_webapi.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();

        void CreateItem(Item item);
    }
}