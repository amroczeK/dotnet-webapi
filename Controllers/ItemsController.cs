using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Dtos;
using dotnet_webapi.Models;
using dotnet_webapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            // First it will evaluate the asynchronous method call in the brackets, then do Select
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id){
            var item = await repository.GetItemAsync(id);

            if(item is null){
                return NotFound();
            }

            return Ok(item.AsDto());
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }
            
            // Taking existing item and creating a copy of it with modified properties/values
            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);
            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if(item is null){
                return NotFound();
            }

            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}