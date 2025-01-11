namespace ProductService.Application.Common.DTO.ProductDTO
{
    public sealed class AddProductDTO
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
