using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Entities;
using AutoMapper;

namespace App.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<ProductFeature,ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto,Product>();
            CreateMap<Product,ProductWithCategoryDto>();
            CreateMap<Category,CategoryWithProductsDto>();
            CreateMap<ProductCreateDto, Product>().ReverseMap();


        }
    }
}