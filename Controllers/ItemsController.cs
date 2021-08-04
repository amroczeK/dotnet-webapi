using System;
using System.Collections.Generic;
using dotnet_webapi.Models;
using dotnet_webapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemoryItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemoryItemsRepository();
        }

        // GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id){
            var item = repository.GetItem(id);

            if(item is null){
                return NotFound();
            }

            return Ok(item);
        }
    }
}