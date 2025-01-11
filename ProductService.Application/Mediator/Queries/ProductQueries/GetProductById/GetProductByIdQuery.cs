using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Queries.ProductQueries.GetProductById
{
    public class GetProductByIdQuery
        : IRequest<IServiceResponse>
    {
        public int Id { get; set; }
    }
}
