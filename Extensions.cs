// Extends definition of one type by adding a method that can be executed on the type
using dotnet_webapi.Dtos;
using dotnet_webapi.Models;

namespace dotnet_webapi
{
    public static class Extensions {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}