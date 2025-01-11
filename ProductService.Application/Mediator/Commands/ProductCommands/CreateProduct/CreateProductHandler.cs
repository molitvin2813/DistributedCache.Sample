using AutoMapper;
using MediatR;
using ProductService.Application.Common.Response;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Mediator.Commands.ProductCommands.CreateProduct
{
    public sealed class CreateProductHandler
        : IRequestHandler<CreateProductCommand, IServiceResponse>
    {
        public CreateProductHandler(
            IProductServiceContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private readonly IProductServiceContext _context;
        private readonly IMapper _mapper;

        public async Task<IServiceResponse> Handle(
            CreateProductCommand command,
            CancellationToken token)
        {
            var data = _mapper.Map<Product>(command);
            data.CreatedDate = DateTime.Now;

            await _context.Products.AddAsync(data, token);
            await _context.SaveChangesAsync(token);

            return new ServiceResponseWrite();
        }
    }
}
