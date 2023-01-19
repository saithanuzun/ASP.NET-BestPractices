using System.Reflection;
using App.Core.Entities;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Repository;
using App.Repository.Repositories;
using App.Repository.UnitOfWorks;
using App.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using App.Service.Validations;
using FluentValidation.AspNetCore;


using Microsoft.Extensions.DependencyInjection;
using App.Service.Services;
using App.API.Filters;
using Microsoft.AspNetCore.Mvc;
using App.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options=>{
    options.SuppressModelStateInvalidFilter=true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),(option) =>
    {
        option.MigrationsAssembly("App.Repository");
    });

});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();

 







var app = builder.Build();

app.UseHttpsRedirection();
app.UserCustomException();
app.UseAuthorization();
app.MapControllers();
app.Run();
