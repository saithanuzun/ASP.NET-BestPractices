using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using App.Core.DTOs;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Service.Exceptions;
using System.Linq.Expressions;
using App.Core.Entities;
using Microsoft.AspNetCore.Http;

// doest work, it has to implement interface
namespace App.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IUnitOfWork unitOfWork, IProductRepository repository, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _memoryCache = memoryCache;
            _mapper = mapper;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }

        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            var newDto = _mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created,newDto);
        }
        

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();         
            var newDto = _mapper.Map<ProductDto>(entity);

            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created,newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductDto> dto)
        {
            List<Product> entities = _mapper.Map<List<Product>>(dto);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            var newDtos = _mapper.Map<List<ProductDto>>(entities);
            return CustomResponseDto<IEnumerable<ProductDto>>.Success(StatusCodes.Status201Created,newDtos);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            var result = await _repository.AnyAsync(expression);

            return CustomResponseDto<bool>.Success(StatusCodes.Status200OK, result);
        }

        public async Task<CustomResponseDto<IEnumerable<ProductDto>>> GetAllAsync()
        {
          
          var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

          var newDtos = _mapper.Map<List<ProductDto>>(products);
          return  CustomResponseDto<IEnumerable<ProductDto>>.Success(StatusCodes.Status200OK,newDtos);
        }

        public async Task<CustomResponseDto<ProductDto>> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if(product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }
            var dto = _mapper.Map<ProductDto>(product);

            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK, dto);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);  
        }

        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<IEnumerable<ProductDto>>> Where(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());

        }

        
    }
}