using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class ProductWithCategoryModel : ProductModel
    {
        public CategoryModel Category { get; set; }
    }
}