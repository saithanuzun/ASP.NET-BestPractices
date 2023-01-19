using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Entities;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using AutoMapper;

namespace App.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper, IGenericRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _productRepository=productRepository;
            _mapper= mapper;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var Products = await _productRepository.GetProductsWithCategory();
            var ProductDto= _mapper.Map<List<ProductWithCategoryDto>>(Products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,ProductDto);
        }
    }
}