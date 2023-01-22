using AutoMapper;
using Microsoft.AspNetCore.Http;
using App.Core.DTOs;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Core.Entities;

namespace App.Service.Services
{
    public class ProductService : Service<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created, newDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _productRepository.Update(entity);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}