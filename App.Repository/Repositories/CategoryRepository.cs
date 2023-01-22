using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int CategoryId)
        {
            return await _context.Categories.Include(x=>x.Products).Where(x=>x.Id==CategoryId).SingleOrDefaultAsync();
        }
    }
}