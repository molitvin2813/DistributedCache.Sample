using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Response;
using ProductService.Application.Common.Specifications.GeneralSpecifications;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Mediator.Commands.ProductCommands.UpdateProduct
{
    public sealed class UpdateProductHandler
        : IRequestHandler<UpdateProductCommand, IServiceResponse>
    {
        public UpdateProductHandler(
            IMapper mapper,
            IProductServiceContext context,
            IDistributedCache cache)
        {
            _mapper = mapper;
            _context = context;
            _cache = cache;
        }

        private readonly IMapper _mapper;
        private readonly IProductServiceContext _context;
        private readonly IDistributedCache _cache;

        public async Task<IServiceResponse> Handle(
        UpdateProductCommand command,
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

            _mapper.Map(command, data);

            await _context.SaveChangesAsync(token);

            var serializedValue = JsonConvert.SerializeObject(data);

            await _cache.SetStringAsync($"ProductId:{data.Id}", serializedValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            }, token);

            return new ServiceResponseWrite();
        }
    }
}
