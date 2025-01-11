using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductService.Application.Common.DTO.ProductDTO;
using ProductService.Application.Common.Exceptions;
using ProductService.Application.Common.Response;
using ProductService.Application.Common.Specifications.GeneralSpecifications;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Mediator.Queries.ProductQueries.GetProductById
{
    public sealed class GetProductByIdHandler
        : IRequestHandler<GetProductByIdQuery, IServiceResponse>
    {
        public GetProductByIdHandler(
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
            GetProductByIdQuery query,
            CancellationToken token)
        {
            GetProductDTO? data;

            var cacheValue = await _cache.GetStringAsync($"ProductId:{query.Id}", token);
            if (cacheValue != null)
            {
                data = JsonConvert.DeserializeObject<GetProductDTO>(cacheValue);
            }
            else
            {
                var specification = new ByIdSpecification<int, Product>(query.Id);
                data = await _context.Products
                    .AsNoTracking()
                    .Where(specification)
                    .ProjectTo<GetProductDTO>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(token);

                if (data == null)
                {
                    throw new RecordNotFoundException<int>(nameof(Product), query.Id);
                }

                var serializedValue = JsonConvert.SerializeObject(data);

                await _cache.SetStringAsync($"ProductId:{data.Id}", serializedValue, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                }, token);
            }

            return new ServiceResponseRead<GetProductDTO>()
            {
                Data = data
            };
        }
    }
}
