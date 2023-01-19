using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Core.Entities;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;
        public NotFoundFilter(IService<T> service)      
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValues = context.ActionArguments.Values.FirstOrDefault();

            if(idValues is null)
            {
                await next.Invoke();
                return;
            }

            var id =(int)idValues;

            var anyEntity= await _service.AnyAsync(x=>x.Id==id); 

            if(anyEntity)
            {  
                await next.Invoke();
                return; 

            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404,typeof(T).Name + "Not Found"));

        }
    }
}