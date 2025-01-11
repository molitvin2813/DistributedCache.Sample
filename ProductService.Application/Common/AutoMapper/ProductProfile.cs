﻿using AutoMapper;
using ProductService.Application.Common.DTO.ProductDTO;
using ProductService.Application.Mediator.Commands.ProductCommands.CreateProduct;
using ProductService.Application.Mediator.Commands.ProductCommands.UpdateProduct;
using ProductService.Domain.Models;

namespace ProductService.Application.Common.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDTO>();
            CreateProjection<Product, GetProductDTO>();

            CreateMap<AddProductDTO, CreateProductCommand>();
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateProductDTO, UpdateProductCommand>()
                .ForMember(
                    dto => dto.Price,
                    expr => expr.MapFrom((src, dst) => src.Price ?? dst.Price)
                )
                .ForMember(
                    dto => dto.Count,
                    expr => expr.MapFrom((src, dst) => src.Count ?? dst.Count)
                )
                .ForMember(
                    dto => dto.Name,
                    expr => expr.MapFrom((src, dst) => src.Name ?? dst.Name)
                )
                .ForAllMembers(opts => opts.Condition((src, dst, srcMember) => srcMember != null));


            CreateMap<UpdateProductCommand, Product>()
                .ForMember(
                    dto => dto.Price,
                    expr => expr.MapFrom((src, dst) => src.Price ?? dst.Price)
                )
                .ForMember(
                    dto => dto.Count,
                    expr => expr.MapFrom((src, dst) => src.Count ?? dst.Count)
                )
                .ForMember(
                    dto => dto.Name,
                    expr => expr.MapFrom((src, dst) => src.Name ?? dst.Name)
                )
                .ForAllMembers(opts => opts.Condition((src, dst, srcMember) => srcMember != null));
        }
    }
}