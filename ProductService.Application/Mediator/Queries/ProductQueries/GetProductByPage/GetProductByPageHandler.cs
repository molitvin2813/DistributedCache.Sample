using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Common.DTO.ProductDTO;
using ProductService.Application.Common.Response;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Mediator.Queries.ProductQueries.GetProductByPage
{
    public sealed class GetProductByPageHandler
        : IRequestHandler<GetProductByPageQuery, IServiceResponse>
    {
        public GetProductByPageHandler(
            IMapper mapper,
            IProductServiceContext conetxt)
        {
            _mapper = mapper;
            _conetxt = conetxt;
        }

        private readonly IMapper _mapper;
        private readonly IProductServiceContext _conetxt;

        public async Task<IServiceResponse> Handle(
            GetProductByPageQuery query,
            CancellationToken token)
        {
            var data = await _conetxt.Products
                .AsNoTracking()
                .OrderBy(x => x.CreatedDate)
                .Skip(query.Take * query.Page)
                .Take(query.Take)
                .ProjectTo<GetProductDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(token);

            return new ServiceResponseRead<List<GetProductDTO>>()
            {
                Data = data
            };
        }
    }
}
