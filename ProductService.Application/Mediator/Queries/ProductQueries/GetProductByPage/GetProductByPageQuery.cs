using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Queries.ProductQueries.GetProductByPage
{
    public class GetProductByPageQuery
        : IRequest<IServiceResponse>
    {
        public int Take { get; set; }

        public int Page { get; set; }
    }
}
