using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Common.DTO.ProductDTO;
using ProductService.Application.Mediator.Commands.ProductCommands.CreateProduct;
using ProductService.Application.Mediator.Commands.ProductCommands.DeleteProduct;
using ProductService.Application.Mediator.Commands.ProductCommands.UpdateProduct;
using ProductService.Application.Mediator.Queries.ProductQueries.GetProductById;
using ProductService.Application.Mediator.Queries.ProductQueries.GetProductByPage;
using ProductService.Domain.Interfaces;

namespace ProductService.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        public ProductController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Возвращает список всех продуктов
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="500">Ошибка на стороне сервера</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{page}/{take}")]
        public async Task<ActionResult<IServiceResponse>> GetAllProduct([FromRoute] int page, [FromRoute] int take)
        {
            return Ok(await _mediator.Send(new GetProductByPageQuery()
            {
                Page = page,
                Take = take
            }));
        }

        /// <summary>
        /// Возвращает одну запись по указанному идентификатору
        /// </summary>
        /// <param name="productId">Идентификатор записи в таблице Product</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="500">Ошибка на стороне сервера</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IServiceResponse>> GetProductById(int productId)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery()
            {
                Id = productId
            }));
        }

        /// <summary>
        /// Добавляет запись в таблицу Product
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        /// <response code="201"></response>
        /// <response code="500">Ошибка на стороне сервера</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IServiceResponse>> CreateProduct([FromBody] AddProductDTO addProductDto)
        {
            var command = _mapper.Map<CreateProductCommand>(addProductDto);

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Удаляет запись из таблицы Product
        /// </summary>
        /// <param name="ProductId">Идентификатор записи в таблице Product</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="500">Ошибка на стороне сервера</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IServiceResponse>> DeleteProduct(int ProductId)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand()
            {
                Id = ProductId
            }));
        }

        /// <summary>
        /// Обновляет запись в таблице Product
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="500">Ошибка на стороне сервера</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IServiceResponse>> UpdateProduct([FromBody] UpdateProductDTO updateProductDto)
        {
            var command = _mapper.Map<UpdateProductCommand>(updateProductDto);

            return Ok(await _mediator.Send(command));
        }
    }
}
