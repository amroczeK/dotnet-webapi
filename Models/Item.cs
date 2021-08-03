using System;

namespace dotnet_webapi.Models
{
    public record Item
    {
        // Init is a property initializer, can't modify property after initialization
        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}