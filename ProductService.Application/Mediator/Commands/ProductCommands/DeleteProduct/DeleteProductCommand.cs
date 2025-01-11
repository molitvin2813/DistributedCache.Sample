using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Commands.ProductCommands.DeleteProduct
{
    public sealed class DeleteProductCommand
        : IRequest<IServiceResponse>
    {
        public int Id { get; set; }
    }
}
