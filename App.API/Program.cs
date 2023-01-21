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
using Autofac.Extensions.DependencyInjection;
using Autofac;
using App.API.Modules;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>  options.Filters.Add(new ValidateFilterAttribute()))
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


builder.Services.Configure<ApiBehaviorOptions>(options=>{
    options.SuppressModelStateInvalidFilter=true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),(option) =>
    {
        option.MigrationsAssembly("App.Repository");
    });

});
 



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepoServiceModule());
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomException();
app.UseAuthorization();
app.MapControllers();
app.Run();
