using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Commands.ProductCommands.CreateProduct
{
    public sealed class CreateProductCommand
        : IRequest<IServiceResponse>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
