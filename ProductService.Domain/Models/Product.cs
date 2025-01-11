using ProductService.Domain.Interfaces;

namespace ProductService.Domain.Models
{
    public class Product
        : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Count { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
