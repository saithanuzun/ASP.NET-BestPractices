using System.Reflection;
using App.Core.Entities;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Repository;
using App.Repository.Repositories;
using App.Repository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),(option) =>
    {
        option.MigrationsAssembly("App.Repository");
    });

});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
//builder.Services.AddScoped<IService,Service>();

var app = builder.Build();



app.MapGet("/test",async (AppDbContext con) => {


});

app.Run();
