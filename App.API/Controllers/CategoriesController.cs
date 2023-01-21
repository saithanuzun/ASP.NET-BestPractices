using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService ,IMapper mapper )
        {
            _mapper=mapper;
            _categoryService=categoryService;
        }

        [HttpGet("[action]/{CategoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int CategoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProducts(CategoryId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200,categoriesDto));
        
        }
    }
}