using App.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ProductApiService>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});
builder.Services.AddHttpClient<CategoryApiService>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
      app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();