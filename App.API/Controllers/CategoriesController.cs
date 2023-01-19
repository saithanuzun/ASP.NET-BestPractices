using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        [HttpGet("[action]/{CategoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int CategoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProducts(CategoryId));
        }
    }
}