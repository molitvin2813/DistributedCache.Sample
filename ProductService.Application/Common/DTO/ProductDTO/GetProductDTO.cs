namespace ProductService.Application.Common.DTO.ProductDTO
{
    public sealed class GetProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
