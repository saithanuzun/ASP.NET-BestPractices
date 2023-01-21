using App.Web.Models;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace App.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categoriesModel = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesModel, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductModel productModel)
        {

            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productModel);             
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();          
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }


        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();
      
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name",product.CategoryId);
            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductModel productModel)
        {
            if(ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productModel); 

                return RedirectToAction(nameof(Index));

            }

            var categoriesModel = await  _categoryApiService.GetAllAsync();
       
            ViewBag.categories = new SelectList(categoriesModel, "Id", "Name", productModel.CategoryId);

            return View(productModel);

        }


        public async Task<IActionResult>  Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}