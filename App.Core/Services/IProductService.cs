using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Entities;

namespace App.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory(); 
    }
}