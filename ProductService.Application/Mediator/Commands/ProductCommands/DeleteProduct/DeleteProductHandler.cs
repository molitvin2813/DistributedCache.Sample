using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Response;
using ProductService.Application.Common.Specifications.GeneralSpecifications;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Mediator.Commands.ProductCommands.DeleteProduct
{
    public class DeleteProductHandler
        : IRequestHandler<DeleteProductCommand, IServiceResponse>
    {
        public DeleteProductHandler(
            IProductServiceContext context,
            IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        private readonly IProductServiceContext _context;
        private readonly IDistributedCache _cache;

        public async Task<IServiceResponse> Handle(
        DeleteProductCommand command,
            CancellationToken token)
        {
            var specification = new ByIdSpecification<int, Product>(command.Id);

            var data = await _context.Products
                .Where(specification)
                .SingleOrDefaultAsync(token);

            if (data == null)
            {
                throw new RecordNotFoundException<int>(nameof(Product), command.Id);
            }

            _context.Products.Remove(data);
            await _context.SaveChangesAsync(token);

            await _cache.RemoveAsync($"ProductId:{data.Id}");

            return new ServiceResponseWrite();
        }
    }
}
