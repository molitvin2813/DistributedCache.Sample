using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Commands.ProductCommands.UpdateProduct
{
    public sealed class UpdateProductCommand
          : IRequest<IServiceResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? Count { get; set; }
    }
}
